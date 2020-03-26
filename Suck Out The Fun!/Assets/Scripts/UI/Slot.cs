using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image weaponIcon;
    public Item weaponInSlot;

    void Start()
    {
        weaponIcon.enabled = false;
    }

    public void DisplayItem()
    {

    }
}