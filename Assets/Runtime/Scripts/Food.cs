using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float foodAmount = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            collision.GetComponent<Status>().SetHungry(foodAmount);

            Destroy(gameObject);
        }
    }
}
