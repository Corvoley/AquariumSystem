using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthAmount = 0;
    [SerializeField] private float healthMax = 10f;
    [SerializeField] private bool canRegen;

    public event Action OnDied;
    private void Awake()
    {       
       healthAmount = healthMax;
    }         
    public void TakeDamage(float amount)
    {
        healthAmount -= amount;
        if (healthAmount <= 0)
        {
            Die();
        }                
    }
    public void HealDamage(float amount)
    {
        healthAmount += amount;
        healthAmount = Math.Clamp(healthAmount, 0, healthMax);
    }
    private void Die()
    {
       OnDied?.Invoke();
    }
}
