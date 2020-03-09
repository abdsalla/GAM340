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

    public void Move(Vector3 direction)
    {
        direction = transform.InverseTransformDirection(direction); // Using InverseTransformDirection here to make the character move based on World Space 
        direction = Vector3.ClampMagnitude(direction, 1);

        // animator requires local space data
        anim.SetFloat("Horizontal", direction.x * speed); 
        anim.SetFloat("Vertical", direction.z * speed);
    }
}