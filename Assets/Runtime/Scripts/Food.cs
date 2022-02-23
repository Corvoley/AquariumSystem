using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public event Action FoodPickupEvent;  
    [SerializeField] private float foodAmount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            if (collision.GetComponentInParent<FishAI>().StateAI == FishAI.State.ChaseFood)
            {
                FoodPickupEvent?.Invoke();
                collision.GetComponentInParent<HungerSystem>().SetHunger(foodAmount);

                Destroy(gameObject);
            }
            
        }
    }
}
