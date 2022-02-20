using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 roamPosition;
    private MovementController movement;
    [SerializeField] private SpriteRenderer aquariumSprite;
    [SerializeField] private Vector2 minDistMove, maxDistMove;
    [SerializeField] private float minTimeToMove = 1f;
    [SerializeField] private float maxTimeToMove = 10f;
    private float timer;
    private float timeFromLastMove;



    private void Awake()
    {
        movement = GetComponent<MovementController>();
        startPosition = transform.position;
    }
    private void Start()
    {
        roamPosition = GetRoamingPos(minDistMove.x, minDistMove.y, maxDistMove.x, maxDistMove.y);
    }
    private void Update()
    {
        RoamingController();
    }

    private void RoamingController()
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

            //movement.MoveToPosition(roamPosition, movement.MoveSpeed, timeFromLastMove);
            movement.MoveToPositionFast(roamPosition, movement.MoveSpeed);
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
        return new Vector3(GetValuePosOrNeg(),GetValuePosOrNeg());
    }
    private float GetValuePosOrNeg()
    {
        return Random.value < 0.5f ? -1 : 1;
    }



}


