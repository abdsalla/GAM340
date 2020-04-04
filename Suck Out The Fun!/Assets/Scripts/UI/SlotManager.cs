using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    private const int MAX_SLOTS = 2;
    
    public Slot activeSlot;
    public Slot[] slots;

    [SerializeField] private Pawn pawn;


    void Start() { pawn = GetComponentInParent<Pawn>(); }

    void Update()
    {
        if (GameManager.Instance.isPaused) return;

        ToggleActiveWeapon();
    }


    void ToggleActiveWeapon()
    {
        Rifle tempWeaponHold;

        if (Input.GetKeyDown(KeyCode.Q) && pawn.weapon != null || Input.GetKeyDown(KeyCode.E) && pawn.weapon != null)
        {
            if (activeSlot.activeWeapon == slots[0].activeWeapon)
            {
                tempWeaponHold = slots[0].activeWeapon;
                activeSlot.activeWeapon = slots[1].activeWeapon;
                slots[1].activeWeapon = tempWeaponHold;
                pawn.SwitchWeapon(activeSlot.activeWeapon);
                activeSlot.GetComponent<Slot>().enabled = !activeSlot.GetComponent<Slot>().enabled;
                StartCoroutine(activeSlot.DisplayItem());
                StopCoroutine(activeSlot.DisplayItem());
            }
            else if (activeSlot.activeWeapon == slots[1].activeWeapon)
            {
                tempWeaponHold = slots[1].activeWeapon;
                activeSlot.activeWeapon = slots[0].activeWeapon;
                slots[0].activeWeapon = tempWeaponHold;
                pawn.SwitchWeapon(activeSlot.activeWeapon);
                activeSlot.GetComponent<Slot>().enabled = !activeSlot.GetComponent<Slot>().enabled;
                StartCoroutine(activeSlot.DisplayItem());
                StopCoroutine(activeSlot.DisplayItem());
            }
        }
    }
}