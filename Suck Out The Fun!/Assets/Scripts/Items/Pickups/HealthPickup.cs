using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public Consummable healthPack;

    public override void OnPickup(GameObject target)
    {
        Energy enToAffect = target.GetComponent<Energy>();
        healthPack.toHeal = enToAffect;

        if (receiverPawn != null)
        {
            if (receiverPawn.agent == null && enToAffect.CurrentHealth < enToAffect.maxHealth)
            {
                receiverPawn.UseConsummable(healthPack);
                healthPack.OnUse();
                receiverPawn.ConsummableEffect(healthPack);
                Destroy(gameObject);
            }
        }
    }
}