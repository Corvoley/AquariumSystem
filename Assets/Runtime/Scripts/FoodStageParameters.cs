using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FoodStage
{
    [field: SerializeField]
    public Sprite foodStageSprite { get; set; }
    [field: SerializeField]
    public Vector3[] foodPositions { get; set; }
    [field: SerializeField]
    public float minFoodAmount { get; set; }

}

[CreateAssetMenu(menuName ="Data/FoodStageSettings")]
public class FoodStageParameters : ScriptableObject
{
    [field: SerializeField]
    public float foodInitialAmount { get; set; }
    [field: SerializeField]
    public float foodMaxAmount { get; set; }
    [field: SerializeField]
    public GameObject foodPrefab { get; set; }

    [field: SerializeField]
    public FoodStage[] foodStages { get; set; }
}
