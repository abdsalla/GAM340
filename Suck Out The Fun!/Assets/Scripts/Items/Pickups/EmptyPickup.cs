using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPickup : Pickup
{  
    public override void OnPickup(GameObject target)
    {
        if (receiverPawn != null) { Destroy(gameObject);  }
    }
}