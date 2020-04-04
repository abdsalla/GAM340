using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public float ammo = 100;
    public Transform RightHandPoint;
    public Transform LeftHandPoint;
    public Sprite weaponSprite;
    public float shotRange;
    [SerializeField] protected float damageDone;

    public void Start() { type = ItemType.Weapon; }

    public void Update() { if (GameManager.Instance.isPaused) return; }

    public override void OnUse() { }

    public override void OnExit() { }

}