﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn pawn;

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if (GameManager.Instance.isPaused) return;
    }
}
