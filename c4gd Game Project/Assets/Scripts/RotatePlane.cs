using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlane : MonoBehaviour
{
    public float rotationDuration = 1.0f;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationTime = 0.0f;
    private bool isRotating = false;

    void Start()
    {
        // 初始化目标旋转角度
        startRotation = transform.Rotation;
        targetRotation = Quaternion.Euler(rotation.eulerAngles + Vector3.up * 180.0f);
    }

    void Update()
    {
        if (isRotating)
        {
            rotationTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationTime / rotationDuration);

            if (rotationTime >= rotationDuration)
            {
                isRotating = false;
            }
        }

        if (!isRotating)
        {
            StartRotation();
        }
    }

    private void StartRotation()
    {
        rotationTime = 0.0f;
        isRotating = true;
    }
}

