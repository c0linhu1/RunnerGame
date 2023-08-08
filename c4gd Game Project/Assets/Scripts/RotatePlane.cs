using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlane : MonoBehaviour
{

    public float timerforrotate = 1.5f;
    public float timerotating = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timerforrotate > 0) {
            timerforrotate -= Time.deltaTime;
            timerotating = 1f;
        } else if (timerforrotate <= 0) {
            if (timerotating > 0) {
                timerotating -= Time.deltaTime;
                transform.Rotate(0f, 0f, 2f);
            } else if (timerotating <= 0) {
                timerforrotate = 1.5f;
            }
        }
    }
}
