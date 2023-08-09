using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    private Vector3 lastCheckPointPosition;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lastCheckPointPosition = transform.position;
        }
    }

    public Vector3 GetLastCheckPointPosition()
    {
        return lastCheckPointPosition;
    }
}

