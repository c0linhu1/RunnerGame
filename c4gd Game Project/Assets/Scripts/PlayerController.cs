using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private float speed = 15;

    private float forwardInput;
    private float horizontalInput;

    private Rigidbody rb; 
    public float jumpForce = 150;

    public bool isOnGround = true;

    // public bool started = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isOnGround = true;
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

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        }
    }
}


