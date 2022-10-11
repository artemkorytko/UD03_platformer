using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startHealth;
    private int _currentHealth;
    public event Action OnDie;

    private void Start()
    {
        HealthValue = startHealth;
    }

    public int HealthValue
    {
        get => _currentHealth;

        set
        {
            _currentHealth = value;
            if (_currentHealth<=0)
            {
                OnDie?.Invoke();
            }
        }
    }
    public void SetDamage(int damageValue)
    {
        HealthValue -= damageValue;
    }

    [ContextMenu("Set Dead")]
    public void SetDead()
    {
        HealthValue = 0;
    }
}
