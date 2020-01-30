using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public Health playerHealthComponent;

    [Header("UI Elements")]
    public Image healthBar;

    private void Start()
    {
        playerHealthComponent.OnHealthChange.AddListener(UpdateHealthUI);
    }

    public void UpdateHealthUI (float newhealthvalue)
    {
        Debug.Log("Health has been changed to " + newhealthvalue);
        healthBar.fillAmount = newhealthvalue;
    }

    public float CalculateHealth()
    {
        return playerHealthComponent.CurrentHealth / playerHealthComponent.maxHealth;
    }
}