using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    void Update()
    {
        int fps = (int)(1f / Time.deltaTime);
        text.text = fps.ToString();
    }
}
