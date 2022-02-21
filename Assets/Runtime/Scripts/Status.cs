using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private Food foodType;
    [SerializeField] private float hungry;
    [SerializeField] private LayerMask foodLayer;
    public float HungryPercent => hungry / hungryMax;
    [SerializeField] private float hungryMax = 100;
    [SerializeField] private float foodSearchRadius = 10;
    public Collider2D[] FoodInRange { get; private set; }

    private void Start()
    {
        hungry = hungryMax;
    }
    private void Update()
    {
        HungryController();
        
    }                  
      
    public Collider2D GetClosestFood()
    {
        FoodInRange = Physics2D.OverlapCircleAll(transform.position, foodSearchRadius, foodLayer);
        Collider2D bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Collider2D potentialTarget in FoodInRange)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public void SetHungry(float amount) 
    {
        hungry += amount;
    }

    private void HungryController()
    {
        if (hungry > 0)
        {
            //Time.deltaTime / 18 takes 1800 seconds(30 minutes) to deplete a 100 units of hungry
            hungry -= Time.deltaTime / 18 ;           
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, foodSearchRadius);
    }


}
