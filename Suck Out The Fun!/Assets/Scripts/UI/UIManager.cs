using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private GameManager instance;

    public Text fpsCounter;
    public GameObject settings;
    public float actionCost = .02f;


    void Awake() { instance = GameManager.Instance; }

    void Update()
    {
        int fps;
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsCounter.text = "FPS: " + fps;
    }

    public void RecieveDamage (Energy enToAffect, float damageValue, bool isPlayer) // Make Health visual match the healthbar value
    {
        if (isPlayer)
        {
            enToAffect.CurrentHealth -= damageValue;
            enToAffect.health.fillAmount -= damageValue;
        }
        else if (!isPlayer)
        {
            enToAffect.CurrentHealth -= damageValue;
        }
    }

    public void HealDamage (Energy enToAffect, float healValue, bool isPlayer)
    {
        if (isPlayer)
        {
            enToAffect.CurrentHealth += healValue;
            enToAffect.health.fillAmount += healValue;
        }
        else if (!isPlayer) enToAffect.CurrentHealth += healValue;
    }

    public void UseStamina (Energy enToAffect, float actionStamCost, bool isPlayer) // Make Stamina visual match the staminabar value
    {
        if (isPlayer)
        {
            enToAffect.CurrentStamina -= actionStamCost;
            enToAffect.stamina.fillAmount -= actionStamCost;
        }
        else if (!isPlayer) enToAffect.CurrentStamina -= actionStamCost;
    }

    public void RegenStamina (Energy enToAffect, float regenValue, bool isPlayer)
    {
        if (isPlayer)
        {
            enToAffect.CurrentStamina += regenValue;
            enToAffect.stamina.fillAmount += regenValue;
        }
        else if (!isPlayer) enToAffect.CurrentStamina += regenValue;
    }

    public bool ActionReady(Energy stamAmount) // checks to see if player has enough stamina to sprint
    {
        if (stamAmount == null) return false;
        else
        {
            if (stamAmount.stamina.fillAmount >= actionCost) return true;
            else return false; // cannot Sprint
        }  
    }
}