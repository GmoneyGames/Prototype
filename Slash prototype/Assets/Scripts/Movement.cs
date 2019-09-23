using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movespeed;
    public bool Grounded = true;
    public bool Spacedown = false;
    public bool Attacking = false;
    public Rigidbody2D RB;
    public float jump_height;
    public int fallMultiplier;
    public Transform Groundcheck;
    public float Checkradius;
    public LayerMask Whatisground;
    private bool facingright = true;
    private float moveInput;

    private Animator anim;

    public AudioClip attackSound;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(Input.GetAxis("Horizontal") * movespeed, RB.velocity.y);

        if (RB.velocity.y < 0 && !Grounded)
            RB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        moveInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && Grounded == true)
        {
            RB.velocity = Vector2.up * jump_height;
        }



        if (Input.GetButtonUp("Fire1"))
        {
            Attacking = false;
            if (null != anim)
            {
                anim.Play("New Animation", 0, 0.25f);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            source.PlayOneShot(attackSound, 1F);
            Attacking = true;
            if (null != anim)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("AttackAnimation", 0, 0.25f);
            }
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
        }
        else if (facingright == true && moveInput < 0)
        {
            Flip();
        }
    }
}
