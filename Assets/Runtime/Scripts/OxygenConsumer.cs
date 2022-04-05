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
        Debug.Log(totalOxygenConsumptionAmount);
    }
    private void OnDestroy()
    {
        totalOxygenConsumptionAmount -= oxygenAmount;
        Debug.Log(totalOxygenConsumptionAmount);
    }


}
