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
    [SerializeField] protected float foodValue = 5f; 
    public virtual void ReactToFishCollision(in FishCollisionInfo collisionInfo)
    {
        for (int i = 0; i < collisionInfo.HungerSystem.foodType.Length; i++)
        {
            if (collisionInfo.FishAI.StateAI == FishAI.State.ChaseFood && foodType == collisionInfo.HungerSystem.foodType[i])
            {
                collisionInfo.HungerSystem.OnFoodCollision(foodValue);
                collisionInfo.FishAI.OnFoodCollision();
                Destroy(gameObject);
            }
        }
        
        
    }

}
