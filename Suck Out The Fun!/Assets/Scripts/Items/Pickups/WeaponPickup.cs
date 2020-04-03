using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Rifle weaponToGet; 

    public override void OnPickup(GameObject target)
    {
        if (receiverPawn != null && receiverPawn.agent == null) // if is PLayer
        {
            SlotManager inventory = target.GetComponentInChildren<SlotManager>(); // grab inventory

            receiverPawn.weapon.ammo += 50;
            Destroy(gameObject);
            //receiverPawn.EquipWeapon(weaponToGet);   
        }
    }
}