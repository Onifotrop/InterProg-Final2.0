using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float forceScale;
    public float jumpHeight;
    public bool canJump;
    public GrapplingHook grapplingHook;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        //Basic Movement
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * forceScale, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * forceScale, ForceMode2D.Force);
        }
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        canJump = true;
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        canJump = false;
    }
}
