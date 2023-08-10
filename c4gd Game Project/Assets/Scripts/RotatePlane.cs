using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlane : MonoBehaviour
{

    public float waittime = 2.000000f;
    public float timerotating = 1.500000f;
    public float rotationdegree = 180.000000f;
    public float timer = 0.000000f;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        // reset the timer after it goes past total time
        while (timer > timerotating + waittime) {
            timer -= timerotating + waittime;
        }

        // rotate the object
        if (timer < timerotating) {
            transform.Rotate(0.0f, 0.0f, Time.deltaTime * rotationdegree / timerotating);
        }   
    }
}
