using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HungerSystem : MonoBehaviour
{
    [SerializeField] public FoodType[] foodType;
    
    [SerializeField] private LayerMask foodLayer;
    public LayerMask FoodLayer => foodLayer;

    [SerializeField] private float hunger;   
    public float HungerPercent => hunger / hungerMax;
    [SerializeField] private float hungerMax = 100;

    private void Start()
    {
        
        hunger = 0;
    }
    private void Update()
    {
        HungerController();        
    }
    public void SetHunger(float amount) 
    {
        hunger += amount;
    }
    private void HungerController()
    {
        if (hunger > 0)
        {
            //Time.deltaTime / 18 takes 1800 seconds(30 minutes) to deplete a 100 units of hungry
            hunger -= Time.deltaTime / 18 ;           
        }
    }
    public void OnFoodCollision(float foodValue)
    {
        SetHunger(foodValue);   
    }


}
