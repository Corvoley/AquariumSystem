using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public enum State
    {
        Roaming,
        LookForFood,
        ChaseFood,
    }

    [Header("Movement Parameters")]
    [SerializeField] private SpriteRenderer aquariumSprite;
    [SerializeField] private Vector2 minDistMove, maxDistMove;
    [SerializeField] private float minTimeToMove = 1f;
    [SerializeField] private float maxTimeToMove = 10f;
    [SerializeField] private float foodSearchRadius = 10;


    #region AI Parameters
    private Vector3 startPosition;
    private Vector3 roamPosition;
    private float startChaseTime;
    Vector3 target;
    private MovementController movement;
    private HungerSystem status;
    private VisionComponent vision;
    [SerializeField] private Food foodType;
    public State StateAI { get; private set; }

    private float timer;
    private float timeFromLastMove;
    public Collider2D[] FoodInRange { get; private set; }
    #endregion
    private void Awake()
    {
        foodType.FoodPickupEvent += OnFoodPickup;
        vision = GetComponent<VisionComponent>();
        status = GetComponent<HungerSystem>();
        movement = GetComponent<MovementController>();
        StateAI = State.Roaming;
        startPosition = transform.position;
    }
    private void Start()
    {
        roamPosition = GetRoamingPos(minDistMove.x, minDistMove.y, maxDistMove.x, maxDistMove.y);

    }
    private void Update()
    {
        StateMachine();
    }

    private void OnFoodPickup()
    {
        startChaseTime = Time.time;
        timeFromLastMove = Time.time;
    }
    private void StateMachine()
    {
        switch (StateAI)
        {
            case State.Roaming:
                Roaming();
                if (status.HungerPercent <= 0.3f)
                {
                    StateAI = State.LookForFood;
                }
                break;

            case State.LookForFood:

                Roaming();
                if (GetClosestFood() != null)
                {
                     
                    startChaseTime = Time.time;
                    target = GetClosestFood().transform.position;
                    StateAI = State.ChaseFood;

                }
                else
                {
                    StateAI = State.Roaming;
                }
                break;

            case State.ChaseFood:
                if (status.HungerPercent >= 0.8f)
                {
                    StateAI = State.Roaming;
                }
                else
                {
                    ChaseFood();
                }

                break;
            default:
                break;
        }
    }
    private void ChaseFood()
    {
        if (GetClosestFood() == null)
        {
            StateAI = State.LookForFood;
            
        }
        else
        {           
            movement.MoveToFood(GetClosestFood().transform.position, movement.MoveSpeed, startChaseTime);
            roamPosition = GetClosestFood().transform.position;


        }


    }
    private void Roaming()
    {
        if (roamPosition.x < aquariumSprite.transform.position.x - aquariumSprite.bounds.extents.x
            || roamPosition.x > aquariumSprite.transform.position.x + aquariumSprite.bounds.extents.x
            || roamPosition.y < aquariumSprite.transform.position.y - aquariumSprite.bounds.extents.y
            || roamPosition.y > aquariumSprite.transform.position.y + aquariumSprite.bounds.extents.y)
        {
            roamPosition = GetRoamingPos(minDistMove.x, minDistMove.y, maxDistMove.x, maxDistMove.y);
        }
        else
        {

            movement.MoveToPosition(roamPosition, movement.MoveSpeed, timeFromLastMove);
            //movement.MoveToPositionFast(roamPosition, movement.MoveSpeed);
            //movement.MoveToPosSmoothStep(roamPosition, movement.MoveSpeed, timeFromLastMove);
            //movement.MoveToPositionSlowing(roamPosition, movement.MoveSpeed, timeFromLastMove);
            startPosition = transform.position;
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            roamPosition = GetRoamingPos(minDistMove.x, minDistMove.y, maxDistMove.x, maxDistMove.y);
            timer = Random.Range(minTimeToMove, maxTimeToMove);
            timeFromLastMove = Time.time;
        }
        // change roamPosition when reached destination
        /*float reachedPosDistance = 1f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPosDistance)
        {
            roamPosition = GetRoamingPos(minMove.x, minMove.y, maxMove.x, maxMove.y);
        }
        */
        Debug.DrawLine(transform.position, roamPosition);
    }

    private Vector3 GetRoamingPos(float minX, float minY, float maxX, float maxY)
    {
        float posX = GetRandomDir().x * Random.Range(minX, maxX);
        float posY = GetRandomDir().y * Random.Range(minY, maxY);
        return startPosition + new Vector3(posX, posY);
    }
    private Vector3 GetRandomDir()
    {
        return new Vector3(GetValuePosOrNeg(), GetValuePosOrNeg());
    }
    private float GetValuePosOrNeg()
    {
        return Random.value < 0.5f ? -1 : 1;
    }
    public Collider2D GetClosestFood()
    {
        FoodInRange = Physics2D.OverlapCircleAll(transform.position, foodSearchRadius, status.FoodLayer);
        Collider2D bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in FoodInRange)
        {
            if (vision.IsVisible(potentialTarget.gameObject))
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

        }

        return bestTarget;
    }


}


