using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Animator anim;
    public float speed;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Move(Vector2 direction)
    {
        anim.SetFloat("Horizontal", direction.x * speed);
        anim.SetFloat("Vertical", direction.y * speed);
    }
}