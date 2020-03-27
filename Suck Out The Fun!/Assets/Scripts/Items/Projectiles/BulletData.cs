using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    public float damageDone;
    public float travelSpeed;
    [SerializeField] private float lifespan = 1.5f;

    private UIManager actionTracker;
    private GameManager instance;

    //public PlayerController shooter; // Multiplayer revamp

    void Awake()
    {
        instance = GameManager.Instance;
        actionTracker = instance.UI;
    }

    void Start() { Destroy(gameObject, lifespan); }

    void Update() { transform.position += transform.forward * travelSpeed * Time.deltaTime; }

    void OnTriggerEnter(Collider other)
    {
        Energy otherEnergy = other.GetComponent<Energy>();
        PlayerController playerCheck = other.GetComponent<PlayerController>();
        if (otherEnergy != null)
        {
            if (playerCheck != null) { actionTracker.RecieveDamage(otherEnergy, damageDone, true); }
            else
            {
                actionTracker.RecieveDamage(otherEnergy, damageDone, false);
                Debug.Log("Collided with Enemy");
            }
        }
        Destroy(gameObject);
    }
}