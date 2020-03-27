using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private GameObject body;
    [SerializeField] private int amount;
    [SerializeField] private int maxAmount;
    public enum ItemType { Default, Consummable, Weapon, Ammunition}
    protected ItemType type = ItemType.Default;

    public ItemType CurrentType
    {
        get { return type; }
        set { type = value; }
    }

    public abstract void OnUse();
    public abstract void OnExit();
}