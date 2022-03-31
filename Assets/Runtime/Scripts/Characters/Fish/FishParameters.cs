using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Fish")]
public class FishParameters : UnityEngine.ScriptableObject
{
    [field: SerializeField]
    public float oxygenConsumeValue;
}
