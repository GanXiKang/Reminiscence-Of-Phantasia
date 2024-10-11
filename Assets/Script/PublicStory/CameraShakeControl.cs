using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeControl : MonoBehaviour
{
    public Transform cameraTransform;
    //public float shakeDuration = 0.5f;
    public float shakeAmount = 10f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;
    private float currentShakeDuration = 0f;

    void Start()
    {
        originalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            currentShakeDuration = 0f;
            cameraTransform.localPosition = originalPos;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TriggerShake(2f);
        }
    }

    void TriggerShake(float duration)
    {
        currentShakeDuration = duration;
    }
}
