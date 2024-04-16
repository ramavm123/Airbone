using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    public float moveSpeed = 5f; 
    public float jumpForce = 10f; 
    
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }    
    void Update()
    {
        
        float moveInputX = Input.GetAxis("Horizontal");
                
        Vector2 moveVelocity = new Vector2(moveInputX * moveSpeed, rb.velocity.y);

        rb.velocity = moveVelocity;

        
        if (moveInputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        
        else if (moveInputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; 
        }
    }








}

