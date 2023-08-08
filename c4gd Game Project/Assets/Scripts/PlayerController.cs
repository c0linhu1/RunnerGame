using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    public float speed = 40;
    

    private float forwardInput;
    private float horizontalInput;

    private Rigidbody rb; 
    public float jumpForce = 180;

    public bool isOnGround = true;
    public bool firstjump = false;
    public bool doublejump = false;
    
    public bool wallslide = false;

    public AudioClip crashSFX;
    public AudioClip jumpSFX;
    public AudioSource audioSource;

    // public bool started = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isOnGround = true;
        firstjump = false;
        doublejump = false;
        wallslide = false;
        rb.useGravity = true;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed); 
        // transform.Translate(forwardInput * Vector3.forward * Time.deltaTime * speed);

        Vector3 nextV = new Vector3(horizontalInput * speed, rb.velocity.y, forwardInput * speed);
        //print(nextV);
        rb.velocity = nextV;

        // if(started) {
        //     transform.Translate(Vector3.forward * Time.deltaTime * speed);
        // }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isOnGround) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            firstjump = true;
            audioSource.PlayOneShot(jumpSFX, 1.5f);
            } else if (firstjump) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                firstjump = false;
                doublejump = true;
            }
        }

        if (!wallslide) {
            rb.useGravity = true;
        }

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
            speed = 40;
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
            rb.AddForce(Vector3.up * 300, ForceMode.Impulse);
            firstjump = false;
            doublejump = false;
        }
        // else if (other.gameObject.CompareTag("PushForward")) {
        //     transform.Translate(horizontalInput * 10 * Vector3.forward);
        // }
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

}


