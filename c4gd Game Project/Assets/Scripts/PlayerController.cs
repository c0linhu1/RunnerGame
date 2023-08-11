using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;

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
    public AudioSource pushF;
    public AudioSource cp;
    public AudioSource boun;
    public AudioSource slide;
    public float accelerationForce = 2500f;

    public float bounceForce = 200f;

    public bool isPushingForward = false;

    private bool hasCollided = false;

    public bool hastriggeredobstacle = false;

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

    public GameObject ttrltag;
    public GameObject lvl1tag;
    public GameObject lvl2tag;
    public GameObject lvl3tag;

    public GameObject Ragaintext;

    public float timer;
    

    public float tutorialtimer;
    public float level1timer;
    public float level2timer;
    public float level3timer;

    public TextMeshProUGUI time_text;
    public TextMeshProUGUI high_time_text;
    public TextMeshProUGUI tutorialtime_text;
    public TextMeshProUGUI level1time_text;
    public TextMeshProUGUI level2time_text;
    public TextMeshProUGUI level3time_text;

    public bool timestarted;
    public bool tutorialstarted;
    public bool level1started;
    public bool level2started;
    public bool level3started;

    public bool bouncingup;

    public float highest_time; // this stores the highest time in playerprefs
    public float high_time;

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
        transform.position = new Vector3(-4.622f, 0.31f, -25f);

        initialPosition = new Vector3(-4.622f, 0.31f, -25f);

        lastCheckPointPosition = new Vector3(-4.622f, 0.31f, -25f);

        displanes = GameObject.FindGameObjectsWithTag("DisappearPlane");

        ttrltag.SetActive(true);
        lvl1tag.SetActive(false);
        lvl2tag.SetActive(false);
        lvl3tag.SetActive(false);

        Ragaintext.SetActive(false);

        timer = 0.00f;
        timestarted = false;
        tutorialstarted = false;
        level1started = false;
        level2started = false;
        level3started = false;
        tutorialtimer = 0.00f;
        level1timer = 0.00f;
        level2timer = 0.00f;
        level3timer = 0.00f;
        highest_time = PlayerPrefs.GetFloat("hightime");  
        high_time_text.text = "High Time: " + highest_time.ToString("F2");

        bouncingup = false;
    }

    void Update()
    {
    
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (!timestarted && Input.GetKey(KeyCode.W)) {
            tutorialstarted = true;
            timestarted = true;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && isOnGround && timestarted && !bouncingup)
        {
            anim.SetBool("Running", true);
        }
        else if (!bouncingup)
        {
            anim.SetBool("Running", false);
        }
        if (isOnGround == true && !bouncingup)
        {
            anim.SetBool("Falling", true);
        }
        else if (isOnGround == false && !bouncingup)
        {
            anim.SetBool("Falling", false);
        }

        if (timestarted) {
            timer += Time.deltaTime;
            time_text.text = "Time: " + timer.ToString("F2");
        }

        if (tutorialstarted) {
            tutorialtimer += Time.deltaTime;
            tutorialtime_text.text = "Tutorial: " + tutorialtimer.ToString("F2");
        }
        if (level1started) {
            level1timer += Time.deltaTime;
            level1time_text.text = "Level 1: " + level1timer.ToString("F2");
        }
        if (level2started) {
            level2timer += Time.deltaTime;
            level2time_text.text = "Level 2: " + level2timer.ToString("F2");
        }
        if (level3started) {
            level3timer += Time.deltaTime;
            level3time_text.text = "Level 3: " + level3timer.ToString("F2");
        }

        if (Input.GetKey(KeyCode.R)) {
            Ragaintext.SetActive(true);
            StartCoroutine(RDisappearAfterDelay(Ragaintext));
            if (Input.GetKey(KeyCode.L)) {
                Ragaintext.SetActive(false);
                timestarted = false;
                transform.position = new Vector3(-4.622f, 0.31f, -25f);
                initialPosition = new Vector3(-4.622f, 0.31f, -25f);
                lastCheckPointPosition = new Vector3(-4.622f, 0.31f, -25f);
                level3started = false;
                level1started = false;
                tutorialstarted = false;
                level2started = false;
                timer = 0.00f;
                tutorialtimer = 0.00f;
                level1timer = 0.00f;
                level2timer = 0.00f;
                level3timer = 0.00f;
                ttrltag.SetActive(true);
                lvl1tag.SetActive(false);
                lvl2tag.SetActive(false);
                lvl3tag.SetActive(false);
            }
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
            bouncingup = false;
            // attachedRigidbody.useGravity = true;
            if (doublejump || firstjump) {
                isOnGround = true;
                doublejump = false;
                firstjump = false;
            }
        } 
        else if (other.gameObject.CompareTag("Wall")) {
           // slide.Play();
            isOnGround = false;
            wallslide = true;
            firstjump = false;
            doublejump = false;
            // attachedRigidbody.useGravity = false;
            speed = speedonwall;
            rb.useGravity = false;
            bouncingup = false;
        }
        else if (other.gameObject.CompareTag("BounceUp")) {
            boun.Play();
            rb.useGravity = true;
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            firstjump = false;
            doublejump = false;
            bouncingup = true;
        }
        else if (other.gameObject.CompareTag("PushForward")) {
            pushF.Play();
            isPushingForward = true;
            bouncingup = false;
            isOnGround = true;
            rb.AddForce(transform.forward * accelerationForce, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("RotateObstacle")) {
            isOnGround = true;
            bouncingup = false;
        }
        else if (other.gameObject.CompareTag("DisappearPlane")) {
            hasCollided = true;
            pad_d.SetActive(false);
            StartCoroutine(DisappearAfterDelayA(other.gameObject));
            wallslide = false;
            rb.useGravity = true;
            bouncingup = false;
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("CheckPoint")) {
            cp.Play();
            lastCheckPointPosition = transform.position;
            bouncingup = false;
            isOnGround = true;
        }
        else if (other.gameObject.CompareTag("tptolevelone")) {
            tpAudio.Play();
            transform.position = new Vector3(200f, -70f, 42f);
            lastCheckPointPosition = new Vector3(200f, -70f, 42f);
            isOnGround = true;
            ttrltag.SetActive(false);
            lvl1tag.SetActive(true);
            tutorialstarted = false;
            level1started = true;
            level1timer = 0.00f;
        }
        else if (other.gameObject.CompareTag("tptoleveltwo")) {
            tpAudio.Play();
            transform.position = new Vector3(569.7f, 20f, 50f);
            lastCheckPointPosition = new Vector3(569.7f, 20f, 50f);
            isOnGround = true;
            lvl1tag.SetActive(false);
            lvl2tag.SetActive(true);
            level1started = false;
            level2started = true;
            level2timer = 0.00f;
        }
        else if (other.gameObject.CompareTag("tptolevelthree")) {
            tpAudio.Play();
            transform.position = new Vector3(1249.7f, -5f, 98.1f);
            lastCheckPointPosition = new Vector3(1249.7f, -5f, 98.1f);
            isOnGround = true;
            lvl2tag.SetActive(false);
            lvl3tag.SetActive(true);
            level2started = false;
            level3started = true;
            level3timer = 0.00f;
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
            if (!hastriggeredobstacle) {
                obstacle_text.SetActive(true);
                StartCoroutine(DisappearAfterDelay(obstacle_text));
                hastriggeredobstacle = true;
            }
        }
        else if (other.gameObject.CompareTag("finalline")) {
            timestarted = false;
            if (timer < highest_time || highest_time == 0.00f) {
                high_time = timer;
                highest_time = timer;
                high_time_text.text = "<color=#00FF01FF>High Time: " + high_time.ToString("F2") + "</color>";
                PlayerPrefs.SetFloat("hightime", high_time);
            } else {
                high_time_text.text = "High Time: " + highest_time.ToString("F2");
            }
            timestarted = false;
            transform.position = new Vector3(-4.622f, 0.31f, -25f);
            initialPosition = new Vector3(-4.622f, 0.31f, -25f);
            lastCheckPointPosition = new Vector3(-4.622f, 0.31f, -25f);
            level3started = false;
            level1started = false;
            tutorialstarted = false;
            level2started = false;
            timer = 0.00f;
            tutorialtimer = 0.00f;
            level1timer = 0.00f;
            level2timer = 0.00f;
            level3timer = 0.00f;
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

    private IEnumerator RDisappearAfterDelay(GameObject other)
    {
        yield return new WaitForSeconds(2.0f); 
        other.gameObject.SetActive(false);
    }

    private void RespawnAtLastCheckPoint() {
        Vector3 respawnPosition = lastCheckPointPosition;
        transform.position = respawnPosition;
        foreach (GameObject plane in displanes)
        {
            plane.SetActive(true);
        }
    }
}


