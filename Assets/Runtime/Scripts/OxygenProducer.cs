using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenProducer : MonoBehaviour
{
    [SerializeField] private float oxygenAmount;

    public static float totalOxygenProductionAmount;

    private void Awake()
    {
        totalOxygenProductionAmount += oxygenAmount;        
    }

    private void OnDestroy()
    {
        totalOxygenProductionAmount -= oxygenAmount;        
    }
}
