using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
  //variables for character movement
  public float movespeed;
  public bool Grounded = true;
  public bool Spacedown = false;
  public bool Attacking = false;
  public Rigidbody2D RB;
  public float jump_height;
  public Transform Groundcheck;
  public float Checkradius;
  public LayerMask Whatisground;
  private bool facingright = true;
  private float moveInput;
  //variables for animation
  private Animator anim;
  //variables for sound assets
  public AudioClip attackSound;
  private AudioSource source;

  void Awake(){
    source = GetComponent<AudioSource>();
  }

  void Start(){
      anim = GetComponent<Animator>();
  }

  //update is called once per frame
  void Update(){
    Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    transform.position += movement * Time.deltaTime * movespeed;
    moveInput = Input.GetAxis("Horizontal");

    if(Input.GetButtonDown("Jump") && Grounded == true){
      RB.velocity = Vector2.up * jump_height;
    }

    if(Input.GetButtonUp("Fire1")){
      Attacking = false;

      if(null != anim){
      anim.Play("idleAnimation", 0, 0.25f);
      }
    }

    if(Input.GetButtonDown("Fire1")){
      source.PlayOneShot(attackSound, 1F);
      Attacking = true;

      if(null != anim){
        anim.Play("attackAnimation", 0, 0.25f);
      }
    }
  }
  //function to flip x-axis of the character
  void Flip(){
    facingright = !facingright;
    Vector3 scaler = transform.localScale;
    scaler.x *= -1;
    transform.localScale = scaler;
  }

  void FixedUpdate(){
    Grounded = Physics2D.OverlapCircle(Groundcheck.position, Checkradius, Whatisground);

    if(facingright == false && moveInput > 0){
      Flip();
    }else if(facingright == true && moveInput < 0){
      Flip();
    }
  }
}
