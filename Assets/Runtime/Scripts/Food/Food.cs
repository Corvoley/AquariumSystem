using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Algae,
    Coral
}
public class Food : MonoBehaviour, IFishCollisionReact
{
    [SerializeField] public FoodType foodType;
    [SerializeField] private float foodValue = 5f;
    private FoodController foodController;
    private void Start()
    {
        foodController = GetComponentInParent<FoodController>();
    }

    public virtual void ReactToFishCollision(in FishCollisionInfo collisionInfo)
    {       
        if (collisionInfo.FishAI.StateAI == FishAI.State.ChaseFood && collisionInfo.HungerSystem.foodType.Contains(foodType))
        {
            gameObject.SetActive(false);
            foodController.ReduceFoodAmount(foodValue);
            collisionInfo.HungerSystem.OnFoodCollision(foodValue);
            collisionInfo.FishAI.OnFoodCollision();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gameObject.GetComponent<CircleCollider2D>().radius);
    }

}
