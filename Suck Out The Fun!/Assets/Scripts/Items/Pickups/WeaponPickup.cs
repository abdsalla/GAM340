using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weaponToGet; 

    public override void OnPickup(GameObject target)
    {
        if (receiverPawn != null && receiverPawn.agent == null)
        {
            receiverPawn.EquipWeapon(weaponToGet);
            Destroy(gameObject);
        }
    }
}