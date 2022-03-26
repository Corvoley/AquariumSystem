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
    private float foodAmount;

    public virtual void ReactToFishCollision(in FishCollisionInfo collisionInfo)
    {       
        if (collisionInfo.FishAI.StateAI == FishAI.State.ChaseFood && collisionInfo.HungerSystem.foodType.Contains(foodType))
        {
            Destroy(gameObject);
            collisionInfo.HungerSystem.OnFoodCollision(foodValue);
            collisionInfo.FishAI.OnFoodCollision();
        }
    }

}
