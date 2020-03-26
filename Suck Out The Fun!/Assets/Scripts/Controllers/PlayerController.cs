using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pawn))]

public class PlayerController : Controller
{
    public bool canSprint = false;
    public Energy energy;

    //[SerializeField] private Pawn playerPawn;
    private GameManager instance;
    private UIManager actionTracker;

    public override void Start()
    {
        instance = GameManager.Instance;
        actionTracker = instance.UI;
        energy = GetComponent<Energy>();
    }

    public override void Update()
    {
        HandleMovement();
        HandleAttacking();
    }

    void HandleMovement()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        pawn.Move(moveVector); // wherever our thumbstick/keyboard press is
        //Debug.Log("isRunning is: " + pawn.anim.GetBool("isRunning"));
        Sprint();
    }

    void HandleAttacking()
    {
        if (Input.GetButtonDown("Fire")) pawn.OnUse.Invoke();
        else if (Input.GetButtonUp("Fire")) pawn.OnExit.Invoke();
    }

    void Sprint()
    {
        canSprint = actionTracker.ActionReady(energy); // does Player have enough stamina to sprint?

        if (Input.GetKey(KeyCode.Space) && canSprint == true) // Use Sprint Blend Tree
        {
            Debug.Log("Using Quick Movement Tree");
            pawn.anim.SetBool("isRunning", true);
            actionTracker.UseStamina(energy, actionTracker.actionCost, true);
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
            actionTracker.RegenStamina(energy, energy.regenRate, true);
        }
    }
}