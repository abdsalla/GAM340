using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pawn pawn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pawn.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
