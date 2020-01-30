using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UnityFloatEvent : UnityEvent<float>{}

public class Health : MonoBehaviour
{
    [Header("UI Values")]
    public float maxHealth;
    private float _currentHealth;

    public UnityFloatEvent OnHealthChange = new UnityFloatEvent();

    void Start()
    {
        maxHealth = 100f;
        _currentHealth = maxHealth;
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

    public float RecieveDamage(float damageValue)
    {
        _currentHealth -= damageValue;

        //If the current health is less than 0, set it to zero so it doesn't go under
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            Die();
        }
        return _currentHealth;
    }

    //Player damage replenish
    public float HealDamage(float healValue)
    {
        _currentHealth += healValue;
        //If current health is more than the max health set it to max so it doesn't go over
        if (_currentHealth > maxHealth) _currentHealth = maxHealth;
        return _currentHealth;
    }

    //Health percentage is currenthealth divided by maxhealth
    
    public void Die()  // Game Over
    {
        Destroy(this.gameObject);
    }  
}