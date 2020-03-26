using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public Transform RightHandPoint;
    public Transform LeftHandPoint;
    [SerializeField] protected float damageDone;

    public void Start() { type = ItemType.Weapon; }

    public void Update() {    }

    public override void OnUse() { }

    public override void OnExit() { }

}