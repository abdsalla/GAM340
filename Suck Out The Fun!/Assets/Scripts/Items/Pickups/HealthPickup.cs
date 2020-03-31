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
            if (enToAffect.CurrentHealth < enToAffect.maxHealth) // if the unit isn't at full health
            {
                receiverPawn.UseConsummable(healthPack); 
                healthPack.OnUse();
                receiverPawn.ConsummableEffect(healthPack);
                Destroy(gameObject);
            }
        }
    }
}