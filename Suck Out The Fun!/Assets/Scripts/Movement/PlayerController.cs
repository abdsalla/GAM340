using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pawn pawn;
    public bool canSprint = false;

    private GameManager instance;
    private UIManager actionTracker;

    void Start()
    {
        instance = GameManager.Instance;
        actionTracker = instance.UI;
    }

    void Update()
    {       
        pawn.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))); // wherever our thumbstick/keyboard press is
        Debug.Log("isRunning is: " + pawn.anim.GetBool("isRunning"));
        Sprint();
    }

    void Sprint()
    {
        float currentStamina;
        canSprint = actionTracker.ActionReady(); // does Player have enough stamina to sprint?

        if (Input.GetKey(KeyCode.Space) && canSprint == true) // Use Sprint Blend Tree
        {
            Debug.Log("Using Quick Movement Tree");
            pawn.anim.SetBool("isRunning", true);
            currentStamina = actionTracker.playerEnergy.stamina.fillAmount - actionTracker.actionCost;
            actionTracker.UpdateStamina(currentStamina);
        }
        else if (Input.GetKey(KeyCode.Space) && canSprint == false) // Not enough stamina
        {
            Debug.Log("Using Base Movement Tree");
            pawn.anim.SetBool("isRunning", false);
        }
        else // Recovering stamina by standing still
        {
            pawn.anim.SetBool("isRunning", false);
            Debug.Log("No Input, Regening");
            currentStamina = actionTracker.playerEnergy.stamina.fillAmount + actionTracker.playerEnergy.regenRate;
            actionTracker.UpdateStamina(currentStamina);
        }
    }
}