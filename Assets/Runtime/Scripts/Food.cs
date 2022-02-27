using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IFishCollisionReact
{
    [SerializeField] protected float foodValue = 5f; 
    public virtual void ReactToFishCollision(in FishCollisionInfo collisionInfo)
    {
        if (collisionInfo.FishAI.StateAI == FishAI.State.ChaseFood)
        {
            collisionInfo.HungerSystem.OnFoodCollision(foodValue);
            collisionInfo.FishAI.OnFoodCollision();
            Destroy(gameObject);
        }
        
    }

}
