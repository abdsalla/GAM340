using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class UnityFloatEvent : UnityEvent<float> { }

public class Energy : MonoBehaviour
{
    [Header("Energy Stats")]
    public float regenRate;

    [Header("UI Values")]
    public float maxHealth;
    public float maxStamina;
    private float _currentHealth;
    private float _currentStamina;

    [Header("UI Visuals")]
    public Image health;
    public Image stamina;

    // UI Event for health, stamina and death
    public UnityFloatEvent OnHealthChange = new UnityFloatEvent();
    public UnityFloatEvent OnStaminaChange = new UnityFloatEvent();
    public UnityEvent OnDeath = new UnityEvent();

    void Start()
    {
        _currentHealth = maxHealth;
        _currentStamina = maxStamina;
    }

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            OnHealthChange.Invoke(_currentHealth);
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            if (_currentHealth <= 0) OnDeath.Invoke();
        }
    }

    public float CurrentStamina
    {
        get { return _currentStamina; }
        set
        {
            _currentStamina = value;
            OnStaminaChange.Invoke(_currentStamina);
            _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);
        }  
    }

    public void DestroySelf() { Destroy(gameObject, 1.0f); }
}