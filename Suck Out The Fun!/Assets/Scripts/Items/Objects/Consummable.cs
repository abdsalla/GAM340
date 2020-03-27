using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consummable : Item
{
    [SerializeField] private float healAmount = 35;
    [SerializeField] private UIManager actionTracker;
    private GameManager instance;

    public Energy toHeal;

    //public PlayerController shooter; // Multiplayer revamp

    void Awake()
    {
        instance = GameManager.Instance;
    }

    void Start() { type = ItemType.Consummable; }

    public override void OnExit() {  }
    public override void OnUse() { actionTracker.HealDamage(toHeal, healAmount, true); }
}