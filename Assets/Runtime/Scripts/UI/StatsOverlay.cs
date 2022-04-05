using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsOverlay : MonoBehaviour
{
    [SerializeField] private OxygenSystem oxygen;
    [SerializeField] private Scrollbar oxygenBar;


    private void Update()
    {
        oxygenBar.size = oxygen.OxygenPercent;
    }
}
