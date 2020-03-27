using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public Consummable healthPack; // health Item

    public override void OnPickup(GameObject target)
    {
        Energy enToAffect = target.GetComponent<Energy>(); 
        healthPack.toHeal = enToAffect; 

        if (receiverPawn != null)
        {
            if (receiverPawn.agent == null && enToAffect.CurrentHealth < enToAffect.maxHealth) // if the this pawn belongs to the player and the player isn't at full health
            {
                receiverPawn.UseConsummable(healthPack); 
                healthPack.OnUse();
                receiverPawn.ConsummableEffect(healthPack);
                Destroy(gameObject);
            }
        }
    }
}