using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HungerSystem : MonoBehaviour
{
    [SerializeField] public List<FoodType> foodType;
    
    [SerializeField] private LayerMask foodLayer;
    public LayerMask FoodLayer => foodLayer;

    [SerializeField] private float hunger;
    private float wasteCounter = 0;
    public float HungerPercent => hunger / hungerMax;
    [SerializeField] private float hungerMax = 100;

    private void Start()
    {        
        hunger = hungerMax / 2;
    }  
    public void SetHunger(float amount) 
    {
        hunger += amount;
    }
    public void HungerController()
    {
        if (hunger > 0)
        {
            //Time.deltaTime / 18 takes 1800 seconds(30 minutes) to deplete a 100 units of hungry
            hunger -= Time.deltaTime / 18 ;
            wasteCounter += Time.deltaTime / 18;
        }

        if (wasteCounter >= hungerMax/2)
        {
            Debug.Log("Poop!!");
            wasteCounter = 0;
        }
    }
    public void OnFoodCollision(float foodValue)
    {
        SetHunger(foodValue);
    }
}
