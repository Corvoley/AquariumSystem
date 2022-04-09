using System;
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
        oxygen += (OxygenProducer.totalOxygenProductionAmount - OxygenConsumer.totalOxygenConsumptionAmount) * Time.deltaTime;        
        oxygen = Mathf.Clamp(oxygen, 0, oxygenMax);
    }    

    public float GetOxygenPercent()
    {
        return oxygen / oxygenMax;
    }

}
