using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private GameManager instance;
    [SerializeField] private GameObject player;

    public Energy playerEnergy;
    public float actionCost = .02f;

    void Awake()
    {
        instance = GameManager.Instance;
    }

    void Update()
    {
        if (!playerEnergy)
        {
            CheckNSet();
        }
    }

    public void UpdateHealth (float enValue) // Make Health visual match the healthbar value
    {
        Debug.Log("Health has been changed to " + enValue);
        playerEnergy.health.fillAmount = enValue;
    }

    public void UpdateStamina (float enValue) // Make Stamina visual match the staminabar value
    {
        playerEnergy.stamina.fillAmount = enValue;
       // Debug.Log("Stamina is at: " + playerEnergy.stamina.fillAmount);
    }

    public void CheckNSet() 
    {
        player = GameObject.FindWithTag("Player"); // Find PLayer

        if (!player) Debug.Log("Player missing");
        else if (player) playerEnergy = player.GetComponent<Energy>(); // Grab Energy
        // Setup events
        playerEnergy.OnHealthChange.AddListener(UpdateHealth);
        playerEnergy.OnStaminaChange.AddListener(UpdateStamina);
    }

    public bool ActionReady() // checks to see if player has enough stamina to sprint
    {
        if (playerEnergy.stamina.fillAmount >= actionCost) return true;
        else return false;
    }
}