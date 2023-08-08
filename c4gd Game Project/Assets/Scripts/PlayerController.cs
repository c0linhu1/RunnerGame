using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    private float speed = 15;
    

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
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed); 
        transform.Translate(forwardInput * Vector3.forward * Time.deltaTime * speed);

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
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            speed = 15;
            wallslide = false;
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
            speed = 20;
        }
    }
}


