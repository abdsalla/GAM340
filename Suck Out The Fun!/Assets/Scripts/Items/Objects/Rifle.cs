using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{   
    public float ammoCount;

    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool isShooting = false;
    [SerializeField] private float fireRate = .2f;
    [SerializeField] private float roundSpeed = 100f;
    private float shotCooldown;

    void Start()
    {
        shotCooldown = 0;
    }

    void Update()
    {
        if (shotCooldown < fireRate) shotCooldown += Time.deltaTime;

        if (isShooting && shotCooldown >= fireRate && ammoCount > 0)
        {
            ammoCount--;
            ShootBullet();
            shotCooldown = 0;
        }
    }

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(prefabBullet, firePoint.position, firePoint.rotation) as GameObject;
        BulletData bulletData = bullet.GetComponent<BulletData>();
        if (bulletData != null)
        {
            bulletData.damageDone = damageDone;
            bulletData.travelSpeed = roundSpeed;
        }
    }

    public override void OnExit() { isShooting = false; }
    public override void OnUse() { isShooting = true; }
}