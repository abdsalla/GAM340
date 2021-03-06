﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    public float damageDone;
    public float travelSpeed;
    public AudioClip shot;
    public AudioSource audio;

    [SerializeField] private float lifespan = 1.5f;

    private UIManager actionTracker;
    private GameManager instance;

    //public PlayerController shooter; // Multiplayer revamp

    void Awake()
    {
        instance = GameManager.Instance;
        actionTracker = instance.UI;
        audio.PlayOneShot(shot);
    }

    void Start() { Destroy(gameObject, lifespan); }

    void Update()
    {
        if (GameManager.Instance.isPaused) return;

        transform.position += transform.forward * travelSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Energy otherEnergy = other.GetComponent<Energy>();
        PlayerController playerCheck = other.GetComponent<PlayerController>();
        if (otherEnergy != null)
        {
            if (playerCheck != null && other != null) { actionTracker.RecieveDamage(otherEnergy, damageDone, true); }
            else actionTracker.RecieveDamage(otherEnergy, damageDone, false);
        }
        Destroy(gameObject);
    }
}