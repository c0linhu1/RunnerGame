using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    public float speed = 20;
    public float speedonwall = 35;
    public float speedonground = 20;

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

    public AudioSource jump;
    public AudioSource jump2;
    public AudioSource tpAudio;
    public float accelerationForce = 2500f;

    public float bounceForce = 200f;

    public bool isPushingForward = false;

    private bool hasCollided = false;

    // public GameObject Button1;
    // public GameObject Button2;
    // public GameObject Button3;
    // public GameObject titleText;
    // public GameObject optionButton;
    // public GameObject tutorialButton;
    // public GameObject StartButton;
    
    private Vector3 initialPosition;
    private Vector3 lastCheckPointPosition;

    public GameObject[] displanes;

    public bool started = false;

    public GameObject movement_a;
    public GameObject movement_b;
    public GameObject movement_c;
    public GameObject movement_d;
    
    public GameObject pad_a;
    public GameObject pad_b;
    public GameObject pad_c;
    public GameObject pad_d;
    public GameObject pad_e;
    public GameObject obstacle_text;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isOnGround = true;
        firstjump = false;
        doublejump = false;
        wallslide = false;
        rb.useGravity = true;
        anim = GetComponent<Animator>();

        movement_a.SetActive(true);
        movement_b.SetActive(false);
        movement_c.SetActive(false);
        movement_d.SetActive(false);

        pad_a.SetActive(false);
        pad_b.SetActive(false);
        pad_c.SetActive(false);
        pad_d.SetActive(false);
        pad_e.SetActive(false);

        obstacle_text.SetActive(false);

        StartCoroutine(DisappearAfterDelay(movement_a));
        // titleText.SetActive(true);
        // started = false;

        // StartButton.SetActive(true);
        // titleText.SetActive(true);



        initialPosition = transform.position;

        displanes = GameObject.FindGameObjectsWithTag("DisappearPlane");
    }

    void Update()
    {
    
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && isOnGround)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        if (isOnGround == true)
        {
            anim.SetBool("Falling", true);
        }
        else if (isOnGround == false)
        {
            anim.SetBool("Falling", false);
        }

        if (!isPushingForward) {
            Vector3 nextV = new Vector3(horizontalInput * speed, rb.velocity.y, forwardInput * speed);
            rb.velocity = nextV;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isOnGround) {
            firstjump = true;
            isOnGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jumping");
            jump.Play();
        }           
           else if (firstjump) {
                rb.AddForce(Vector3.up * doublejumpForce, ForceMode.Impulse);
                jump.Pause();

                jump2.Play();
                jump.Play();
                firstjump = false;
                doublejump = true;
                isOnGround = false;
            }
        }

        if (transform.position.y < -120)
        {
            RespawnAtLastCheckPoint();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            wallslide = false;
            rb.useGravity = true;
            isOnGround = true;
            // attachedRigidbody.useGravity = true;
            if (doublejump || firstjump) {
                isOnGround = true;
                doublejump = false;
                firstjump = false;
            }
        } 
        else if (other.gameObject.CompareTag("Wall")) {
            isOnGround = false;
            wallslide = true;
            firstjump = false;
            doublejump = false;
            // attachedRigidbody.useGravity = false;
            speed = speedonwall;
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
            isOnGround = true;
            rb.AddForce(transform.forward * accelerationForce, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("RotateObstacle")) {
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("DisappearPlane")) {
            hasCollided = true;
            pad_d.SetActive(false);
            StartCoroutine(DisappearAfterDelayA(other.gameObject));
            wallslide = false;
            rb.useGravity = true;
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("CheckPoint")) {
            lastCheckPointPosition = transform.position;
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("tptolevelone")) {
            tpAudio.Play();
            transform.position = new Vector3(200f, 30f, 42f);
            lastCheckPointPosition = new Vector3(200f, 30f, 42f);
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("tptoleveltwo")) {
            transform.position = new Vector3(569.7f, 42f, 50f);
            lastCheckPointPosition = new Vector3(569.7f, 42f, 50f);
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("platform")) {
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("movementtwo")) {
            isOnGround = true;
            movement_b.SetActive(true);
            StartCoroutine(DisappearAfterDelay(movement_b));
        }
        else if (other.gameObject.CompareTag("movementthree")) {
            isOnGround = true;
            movement_c.SetActive(true);
            StartCoroutine(DisappearAfterDelay(movement_c));
        }
        else if (other.gameObject.CompareTag("movementfour")) {
            isOnGround = true;
            movement_d.SetActive(true);
            StartCoroutine(DisappearAfterDelay(movement_d));
        }
        else if (other.gameObject.CompareTag("padone")) {
            isOnGround = true;
            pad_a.SetActive(true);
            StartCoroutine(DisappearAfterDelay(pad_a));
        }
        else if (other.gameObject.CompareTag("padtwo")) {
            isOnGround = true;
            pad_b.SetActive(true);
            StartCoroutine(DisappearAfterDelay(pad_b));
        }
        else if (other.gameObject.CompareTag("padthree")) {
            isOnGround = true;
            pad_c.SetActive(true);
            StartCoroutine(DisappearAfterDelay(pad_c));
        }
        else if (other.gameObject.CompareTag("padfour")) {
            isOnGround = true;
            pad_d.SetActive(true);
            StartCoroutine(DisappearAfterDelay(pad_d));
        }
        else if (other.gameObject.CompareTag("padfive")) {
            isOnGround = true;
            pad_e.SetActive(true);
            StartCoroutine(DisappearAfterDelay(pad_e));
        }
        else if (other.gameObject.CompareTag("obstacletext")) {
            isOnGround = true;
            obstacle_text.SetActive(true);
            StartCoroutine(DisappearAfterDelay(obstacle_text));
        }
    }

    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            wallslide = false;
            rb.useGravity = true;
            speed = speedonground;
            firstjump = true;
        } 
        else if (other.gameObject.CompareTag("PushForward")) {
            isPushingForward = false;
        } 
    }

    private IEnumerator DisappearAfterDelay(GameObject other)
    {
        yield return new WaitForSeconds(1.0f); 
        other.gameObject.SetActive(false);
    }

    private IEnumerator DisappearAfterDelayA(GameObject other)
    {
        yield return new WaitForSeconds(0.5f); 
        other.gameObject.SetActive(false);
    }

    // public void Options() {
    //     allbuttoninactive();
    // }

    // public void Tutorial() {
    //     allbuttoninactive();
    // }

    // public void levelone() {
    //     allbuttoninactive();
    // }

    // public void leveltwo() {
    //     allbuttoninactive();
    // }

    // public void levelthree() {
    //     allbuttoninactive();
    // }

    // public void leveltutorial() {
    //     titleText.SetActive(false);
    //     StartButton.SetActive(false);
    //     // Time.timeScale = 1;
    // }


    // public void allbuttoninactive() {
    //     optionButton.SetActive(false);
    //     Button1.SetActive(false);
    //     Button2.SetActive(false);
    //     Button3.SetActive(false);
    //     tutorialButton.SetActive(false);
    // }

    // public void allbuttonactive() {
    //     optionButton.SetActive(true);
    //     Button1.SetActive(true);
    //     Button2.SetActive(true);
    //     Button3.SetActive(true);
    //     tutorialButton.SetActive(true);
    // }

    private void RespawnAtLastCheckPoint() {
        Vector3 respawnPosition = lastCheckPointPosition;
        transform.position = respawnPosition;
        foreach (GameObject plane in displanes)
        {
            plane.SetActive(true);
        }
    }
}


