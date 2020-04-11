using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Rifle weaponToGet;
    public AudioClip reload;

    public override void OnPickup(GameObject target)
    {
        if (receiverPawn != null && receiverPawn.agent == null) // if is PLayer
        {
            
            SlotManager inventory = target.GetComponentInChildren<SlotManager>(); // grab inventory

            receiverPawn.weapon.ammo += 50;
            StartCoroutine(Reload());       
            //receiverPawn.EquipWeapon(weaponToGet);   
        }
    }

    public IEnumerator Reload()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(reload);
        yield return new WaitForSeconds(reload.length);
        Destroy(gameObject);
    }
}