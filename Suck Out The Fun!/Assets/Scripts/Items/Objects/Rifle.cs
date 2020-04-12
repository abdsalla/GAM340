using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifle : Weapon
{   
    public float ammoCount;

    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject prefabFlash;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool isShooting = false;
    [SerializeField] private float fireRate = .2f;
    [SerializeField] private float roundSpeed = 100f;
    private float shotCooldown;

    void Start()
    {
        shotCooldown = 0;
        ammoCount = ammo;
    }

    void Update()
    {
        if (GameManager.Instance.isPaused) return;

        if (shotCooldown < fireRate) shotCooldown += Time.deltaTime;

        ammoCount = ammo;
        ShootBullet(); 
    }

    public void ShootBullet()
    {
        if (isShooting && shotCooldown >= fireRate && ammoCount > 0)
        {
            GameObject mFlash = Instantiate(prefabFlash, firePoint.position, firePoint.rotation) as GameObject;
            Destroy(mFlash, 2.0f);
            GameObject bullet = Instantiate(prefabBullet, firePoint.position, firePoint.rotation) as GameObject;
            BulletData bulletData = bullet.GetComponent<BulletData>();
            ammoCount--;
            ammo = ammoCount;
            if (bulletData != null)
            {
                bulletData.damageDone = damageDone;
                bulletData.travelSpeed = roundSpeed;
            }
            shotCooldown = 0;
        }
    }

    public override void OnExit() { isShooting = false; }
    public override void OnUse() { isShooting = true; }
}