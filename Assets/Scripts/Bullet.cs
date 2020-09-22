﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity = transform.right * speed * -1;
        }
        else
        {
            rb.velocity = transform.right * speed;
        }
    }
}
