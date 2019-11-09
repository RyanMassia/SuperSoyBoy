﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Animator))]

public class SoyBoyController : MonoBehaviour
{
    public float speed = 14f; // speed of soyboy
    public float accel = 6f; // accerlation of soyboy
    private Vector2 input; //holds position
    private SpriteRenderer sr; // sprite renderer
    private Rigidbody2D rb; //rigidbody
    private Animator animator; // finds animator componet

    void Awake() // starts up these componets when object is created!
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1  
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");
        // 2   
        if (input.x > 0f)
        {
            sr.flipX = false;
        }
        else if (input.x < 0f)
        {
            sr.flipX = true;
        }
    }

    void FixedUpdate()
    {  
        // 1  assigns acceleration
        var acceleration = accel;
        var xVelocity = 0f;
        // 2   if not moving sets movement to 0  
        if (input.x == 0)
        {
            xVelocity = 0f;
        }
        else
        {
            xVelocity = rb.velocity.x;
        }
        // 3    adds firce which is x times speed - velocity 
        rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
        // 4   when not moving velocity is reset to 0
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    } 

    }
