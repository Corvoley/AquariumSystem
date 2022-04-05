using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(HungerSystem),typeof(VisionComponent),typeof(MovementController))]
public class FishAI : MonoBehaviour
{
    public enum State
    {
        Roaming,
        LookForFood,
        ChaseFood,
    }

    [Header("Movement Parameters")]
    [SerializeField] private SpriteRenderer allowedMoveSprite;
    [SerializeField] private Vector2 minDistMove, maxDistMove;
    [SerializeField] private float minTimeToMove = 1f;
    [SerializeField] private float maxTimeToMove = 10f;
    [SerializeField] private float foodSearchRadius = 10;


    #region AI Parameters

    private Vector3 startPosition;
    private Vector3 roamPosition;
    public float startChaseTime;
    private MovementController movement;
    private HungerSystem hunger;
    private VisionComponent vision;
    public State StateAI { get; private set; }

    private float timer;
    private float timeFromLastMove;
    private Collider2D[] foodInRange = new Collider2D[20];
    private List<Food> foodList = new List<Food>();
    #endregion

    [SerializeField] private FishParameters fishParameters;
    private void Awake()
    {        
        hunger = GetComponent<HungerSystem>();
        movement = GetComponent<MovementController>();
        vision = GetComponent<VisionComponent>();
        StateAI = State.Roaming;
        startPosition = transform.position;
    }
    private void Start()
    {

        roamPosition = GetRoamingPos(minDistMove.x, minDistMove.y, maxDistMove.x, maxDistMove.y);

    }
    private void Update()
    {
        hunger.HungerController();
        StateMachine();
    }

    public void OnFoodCollision()
    {
        startChaseTime = Time.time;
        timeFromLastMove = Time.time;        
    }
    #region State Machine Functions
    private void StateMachine()
    {
        switch (StateAI)
        {

            case State.Roaming:
                RoamingStateHandler();
                break;

            case State.LookForFood:
                LookForFoodStateHandler();
                break;

            case State.ChaseFood:
                ChaseFoodStateHandler();

                break;
            default:
                break;
        }
    }
    private void RoamingStateHandler()
    {
        Roaming();
        if (hunger.HungerPercent <= 0.3f)
        {
            StateAI = State.LookForFood;
        }
    }
    private void LookForFoodStateHandler()
    {
        Roaming();
        if (GetClosestFood() != null)
        {
            startChaseTime = Time.time;
            StateAI = State.ChaseFood;
        }
        else
        {
            StateAI = State.Roaming;
        }
    }
    private void ChaseFoodStateHandler()
    {
        if (hunger.HungerPercent >= 0.8f)
        {
            StateAI = State.Roaming;
        }
        else
        {
            ChaseFood();
        }
    }
    #endregion

    private void ChaseFood()
    {
        if (GetClosestFood() == null)
        {
            StateAI = State.Roaming;
        }
        else
        {
            movement.MoveToPosition(GetClosestFood().transform.position, movement.MoveSpeed, startChaseTime, LerpType.SmoothStep);
            roamPosition = GetRoamingPos(minDistMove.x, minDistMove.y, maxDistMove.x, maxDistMove.y);

        }
    }
    private void Roaming()
    {
        if (!allowedMoveSprite.bounds.Contains(roamPosition))
        {
            roamPosition = GetRoamingPos(minDistMove.x, minDistMove.y, maxDistMove.x, maxDistMove.y);
        }
        else
        {
            movement.MoveToPosition(roamPosition, movement.MoveSpeed, timeFromLastMove, LerpType.Fast);
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
    private Food GetClosestFood()
    {
        int foodCount = Physics2D.OverlapCircleNonAlloc(transform.position, foodSearchRadius, foodInRange, hunger.FoodLayer);
        CheckIfIsFoodType(foodCount);
        Food bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        

        foreach (Food potentialTarget in foodList)
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
        foodList.Clear();
        return bestTarget;
    }
    private void CheckIfIsFoodType(int foodCount)
    {
        for (int i = 0; i < foodCount; i++)
        {
            for (int n = 0; n < hunger.foodType.Count; n++)
            {
                Food food = foodInRange[i].GetComponent<Food>();
                if (food != null && !foodList.Contains(food) && hunger.foodType[n] == food.foodType)
                {
                    foodList.Add(food);
                }
            }
        }
    }

    //code to chase food using coroutine instead of update
    /*CoroutineTest
    StartCoroutine(MoveToFood(GetClosestFood().transform.position, movement.MoveSpeed));
    private IEnumerator MoveToFood(Vector3 destination, float duration)
    {
        isChasingFood = true;
            
        float time = 0;
        Vector3 startValue = transform.position;
        while (time < duration)
        {
            float durationPercent = time / duration;
            durationPercent = durationPercent * durationPercent * (3f - 2f * durationPercent);
            movement.rb.MovePosition(Vector3.Lerp(startValue, destination + (transform.position - movement.characterFacing.FishMouthPos), durationPercent));
            time += Time.deltaTime;
            yield return null;
        }
        //movement.transform.position = destination + (transform.position - movement.characterFacing.FishMouthPos);
        StateAI = State.LookForFood;
        isChasingFood = false;

    }*/

}



