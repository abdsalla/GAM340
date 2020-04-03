using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Rifle activeWeapon;
    [SerializeField] private SpriteRenderer weaponRenderer => GetComponentInChildren<SpriteRenderer>();

    void OnEnable()
    {
        StartCoroutine(DisplayItem());
        StopCoroutine(DisplayItem());
    }

    public IEnumerator DisplayItem()
    {
        Debug.Log("Weapon Icon On");
        weaponRenderer.enabled = true;
        yield return new WaitForSeconds(5.0f);
        weaponRenderer.enabled = false;
        Debug.Log("Weapon Icon Off");
    }
}