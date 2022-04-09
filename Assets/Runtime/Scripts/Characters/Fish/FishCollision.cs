using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollision : MonoBehaviour
{
    private FishController fishAI;
    private HungerSystem hungerSystem;
    private void Awake()
    {
        fishAI = GetComponent<FishController>();
        hungerSystem = GetComponent<HungerSystem>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IFishCollisionReact fishCollisionReact = other.GetComponent<IFishCollisionReact>();
        if (fishCollisionReact != null)
        {
            fishCollisionReact.ReactToFishCollision(new FishCollisionInfo()
            {
                FishAI = fishAI,
                HungerSystem = hungerSystem,
                Collider2D = other
            }); ;
        }
    }
   
}
