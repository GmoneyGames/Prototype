using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movespeed;
    public bool Grounded = true;
    public bool Spacedown = false;
    public Rigidbody2D RB;
    public float jump;
    public Transform Groundcheck;
    public float Checkradius;
    public LayerMask Whatisground;
    private bool facingright = true;
    private float moveInput;


    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * movespeed;
        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetButton("Jump") && Grounded == true && Spacedown == false)
        {
            RB.velocity = Vector2.up * jump;
            Spacedown = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            Spacedown = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Spacedown = true;
        }

        
        
    }

    void Flip()
    {
        facingright = !facingright;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

    }

    void FixedUpdate()
    {
        Grounded = Physics2D.OverlapCircle(Groundcheck.position, Checkradius, Whatisground);

        if (facingright == false && moveInput > 0)
        {
            Flip();
        }else if (facingright == true && moveInput < 0)
        {
            Flip();
        }
    }


}
