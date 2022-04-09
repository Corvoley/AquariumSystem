using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenConsumer : MonoBehaviour
{
    [SerializeField] private float oxygenAmount;

    public static float totalOxygenConsumptionAmount;

    private void Awake()
    {
        totalOxygenConsumptionAmount += oxygenAmount;        
    }
    private void OnDestroy()
    {
        totalOxygenConsumptionAmount -= oxygenAmount;       
    }
}
