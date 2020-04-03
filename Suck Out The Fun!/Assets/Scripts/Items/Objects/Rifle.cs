﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        ammoCount = ammo;
    }

    void Update()
    {
        if (shotCooldown < fireRate) shotCooldown += Time.deltaTime;

        ShootBullet(); 
    }

    public void ShootBullet()
    {
        if (isShooting && shotCooldown >= fireRate && ammoCount > 0)
        {
            GameObject bullet = Instantiate(prefabBullet, firePoint.position, firePoint.rotation) as GameObject;
            BulletData bulletData = bullet.GetComponent<BulletData>();
            ammoCount--;
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