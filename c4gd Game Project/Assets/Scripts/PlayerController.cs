using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    public float speed = 40;

    private float forwardInput;
    private float horizontalInput;

    public float wallSlideSpeedMultiplier = 2f;

    private Rigidbody rb; 
    public float jumpForce = 180;
    public float doublejumpForce = 100;

    public bool isOnGround = true;
    public bool firstjump = false;
    public bool doublejump = false;
    private Animator anim;
    public bool wallslide = false;
    private bool isWallSliding = false;

    public bool wallSlideGravityEnabled = false;

    public AudioSource jump;

    public float accelerationForce = 2500f;

    public float bounceForce = 200f;

    public bool isPushingForward = false;
    // public AudioSource jump2;

    // public bool started = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isOnGround = true;
        firstjump = false;
        doublejump = false;
        wallslide = false;
        rb.useGravity = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed); 
        // transform.Translate(forwardInput * Vector3.forward * Time.deltaTime * speed);

        if (!isPushingForward) {
            Vector3 nextV = new Vector3(horizontalInput * speed, rb.velocity.y, forwardInput * speed);
            //print(nextV);
            rb.velocity = nextV;
        }

        if (transform.position.y < 2) {
            Vector3 newPosition = new Vector3(transform.position.x, 2f, transform.position.z);
            transform.position = newPosition; 
        }
        // if(started) {
        //     transform.Translate(Vector3.forward * Time.deltaTime * speed);
        // }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isOnGround) {
            jump = GetComponent<AudioSource>();
            //jump.Play();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // anim.SetTrigger("Jumping");
            isOnGround = false;
            firstjump = true;
  
            } else if (firstjump) {
                rb.AddForce(Vector3.up * doublejumpForce, ForceMode.Impulse);
                // jump.Play();
                firstjump = false;
                doublejump = true;
            }
        }

    //    if (wallslide && !isOnGround)
    //     {
    //         isWallSliding = true;
    //         rb.useGravity = wallSlideGravityEnabled;
    //         if (isWallSliding)
    //         {
    //             rb.velocity = new Vector2(rb.velocity.x, -speed * wallSlideSpeedMultiplier);
    //         }
    //     }
    //     else
    //     {
    //         isWallSliding = false;
    //         rb.useGravity = true;
    //     }

        // if (transform.position.x <= 8.0f || transform.position.x >= -8.0f) {
        //     wallslide = false;
        //     rb.useGravity = true;
        // } else {
        //     wallslide = true;
        //     rb.useGravity = false;
        // }
    }


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            wallslide = false;
            rb.useGravity = true;
            // attachedRigidbody.useGravity = true;
            if (doublejump || firstjump) {
            isOnGround = true;
            doublejump = false;
            firstjump = false;
            }
        } 
        else if (other.gameObject.CompareTag("Wall")) {
            wallslide = true;
            // attachedRigidbody.useGravity = false;
            speed = 80;
            rb.useGravity = false;
        }
        else if (other.gameObject.CompareTag("BounceUp")) {
            rb.useGravity = true;
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            firstjump = false;
            doublejump = false;
        }
        else if (other.gameObject.CompareTag("PushForward")) {
            isPushingForward = true;
            rb.AddForce(transform.forward * accelerationForce, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("RotateObstacle")) {
            print("Game Over");
        }

        // while(!other.gameObject.CompareTag("Wall")) {
        //     wallslide = false;
        // } 
        // Don't use this code! It will make Unity CRASH!
    }

    // private void OnCollisionStay(Collision other)
    // {
    //     // Debug-draw all contact points and normals
    //     if (other.gameObject.CompareTag("Wall"))
    //     {
    //         wallslide = false;
    //     } 
    // }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            wallslide = false;
            rb.useGravity = true;
        } else if (other.gameObject.CompareTag("PushForward")) {
            isPushingForward = false;
        }
    }
}


