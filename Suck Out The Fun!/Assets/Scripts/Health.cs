using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UnityFloatEvent : UnityEvent<float> { }

public class Health : MonoBehaviour
{
    [Header("UI Elements")]
    public Image healthBar;
    public float currentHealth { get; set; }
    public float maxHealth;

    public UnityEvent OnHealthChange = new UnityEvent();

    void Start()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
    }

    public float ReceiveDamage(float damageValue)
    {
        currentHealth -= damageValue;
        healthBar.fillAmount = CalculateHealth();
        //If the current health is less than 0, set it to zero so it doesn't go under
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Die();
        }

        //if (currentHealth == 0) gameObject.SetActive(false);
        return currentHealth;
    }

    //Player damage replenish
    public float HealDamage(float healValue)
    {
        currentHealth += healValue;
        healthBar.fillAmount = CalculateHealth();
        //If current health is more than the max health set it to max so it doesn't go over
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        return currentHealth;
    }

    //Health percentage is currenthealth divided by maxhealth
    public float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    public void Die()  // Game Over
    {
        Destroy(this.gameObject);
    }

}
