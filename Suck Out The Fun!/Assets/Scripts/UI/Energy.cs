using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class UnityFloatEvent : UnityEvent<float> { }

public class Energy : MonoBehaviour
{
    private GameManager instance;

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

    void Awake()
    {
        instance = GameManager.Instance;
    }

    void Start()
    {
        _currentHealth = maxHealth;
        CalculateHealth();
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
            CalculateHealth();
            if (_currentHealth <= 0)
            {
                OnDeath.Invoke();
            }
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

    public void CalculateHealth() // Determines health color range off of unit's health percentage
    {
        float healthPercent = (100f / maxHealth) * _currentHealth;
        
        if (healthPercent <= 30f) { health.color = Color.red; }
        else if (healthPercent <= 70f && healthPercent > 30f) { health.color = new Color(255, 165, 0); }
        else if (healthPercent > 70f) { health.color = Color.green; }
    }

    public void DestroySelf() // Handles death and increments/decrements of player counters
    {
        Pawn unitCheck = GetComponent<Pawn>();

        if (unitCheck != null)
        {
            if (unitCheck.agent == null)
            {
                instance.lives -= 1;
                instance.score -= 5;
                Destroy(gameObject);
                StartCoroutine(instance.PlayerRespawn());
                StopCoroutine(instance.PlayerRespawn());
            }
            else if (unitCheck.agent != null)
            {
                instance.score += 10;
                Destroy(gameObject, 2.0f);
                for (int i = instance.activeEnemies; i >= instance.allowedEnemies; i--)
                {
                    Destroy(gameObject, 2.0f);
                    instance.activeEnemies -= 1;
                    if (instance.activeEnemies < instance.allowedEnemies)
                    {
                        instance.EnemyRespawn();
                        instance.activeEnemies += 1;
                    }
                }
                
            }
        }
    }
}