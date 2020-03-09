using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Animator anim;
    public float speed;

    void Awake()
    {
        anim = GetComponent<Animator>();        
    }

    public void Move(Vector3 direction) // Receives controller input
    {
        direction = transform.InverseTransformDirection(direction); // World to local for animator
        direction = Vector3.ClampMagnitude(direction, 1);

        // passing animator the speed and direction
        anim.SetFloat("Horizontal", direction.x * speed); 
        anim.SetFloat("Vertical", direction.z * speed);
    }
}