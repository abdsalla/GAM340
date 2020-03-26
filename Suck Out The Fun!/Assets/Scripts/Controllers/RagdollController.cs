using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Energy energy;
    private Rigidbody mainRigidbody;
    private Rigidbody[] childRigidbodies;
    private Collider mainCollider;
    private Collider[] childColliders;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        energy = GetComponent<Energy>();
        // Grab Rigidbody and Collider on this Pawn
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
        // Grab Rigidbodies and Colliders of the joints
        childRigidbodies = GetComponentsInChildren<Rigidbody>();
        childColliders = GetComponentsInChildren<Collider>();
    }

    void Start()
    {
        energy.OnDeath.AddListener(energy.DestroySelf);
        DisableRagdoll();
    }

    public void EnableRagdoll() // Controlled by Ragdoll physics and not the animator
    {
        foreach (Collider col in childColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rig in childRigidbodies)
        {
            rig.isKinematic = false;
        }
        mainRigidbody.isKinematic = true;
        mainCollider.enabled = false;
        anim.enabled = false;
    }

    public void DisableRagdoll() // Not controlled by Ragdoll physics but by the animator
    {
        foreach (Collider col in childColliders)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rig in childRigidbodies)
        {
            rig.isKinematic = true;
        }
        mainRigidbody.isKinematic = false;
        mainCollider.enabled = true;
        anim.enabled = true;
    }
}