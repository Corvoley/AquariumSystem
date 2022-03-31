using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSystem : MonoBehaviour
{
    [SerializeField] private float oxygen = 0;
    [SerializeField] private float oxygenMax = 100;
    
    public float OxygenPercent => oxygen / oxygenMax;

    private void Update()
    {
        OxygenController();
    }
    public void OxygenController()
    {
        if (oxygen < oxygenMax)
        {
            oxygen += (PlantProduction() - FishConsume()) * Time.deltaTime;
        }        
    }

    private float PlantProduction()
    {
        float totalProduction = 0;
        foreach (var plant in EntityNumberController.FoodCountDict)
        {            
            totalProduction += plant.Value * plant.Key.oxygenProdValue;
        }
        return totalProduction;
    }
    private float FishConsume()
    {
        float totalConsume = 0;
        foreach (var fish in EntityNumberController.FishCountDict)
        {
            totalConsume += fish.Value * fish.Key.oxygenConsumeValue;
        }
        return totalConsume;
    }




}
