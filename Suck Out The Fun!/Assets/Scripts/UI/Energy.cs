using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    // UI Event for health and stamina
    public UnityFloatEvent OnHealthChange = new UnityFloatEvent();
    public UnityFloatEvent OnStaminaChange = new UnityFloatEvent();

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
        }

        //if (currentHealth == 0) gameObject.SetActive(false);       
    }

    public float CurrentStamina
    {
        get { return _currentStamina; }
        set
        {
            _currentStamina = value;
            OnStaminaChange.Invoke(_currentStamina);
        }

        //if (currentHealth == 0) gameObject.SetActive(false);       
    }
}