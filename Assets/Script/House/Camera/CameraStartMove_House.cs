using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraStartMove_House : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float _duration = 5f; 
    public static bool isStartMoveCamera = false;
    public static bool isMoving = false;
    float _YSpeed;
    float _XSpeed;

    void Update()
    {
        if (isStartMoveCamera && !isMoving)
        {
            StartCoroutine(MoveCamera());
        }
    }

    IEnumerator MoveCamera()
    {
        isMoving = true;

        _YSpeed = freeLookCamera.m_YAxis.m_MaxSpeed;
        _XSpeed = freeLookCamera.m_XAxis.m_MaxSpeed;
        freeLookCamera.m_YAxis.m_MaxSpeed = 0;
        freeLookCamera.m_XAxis.m_MaxSpeed = 0;

        float elapsedTime = 0;
        float startY = freeLookCamera.m_YAxis.Value;
        float startX = freeLookCamera.m_XAxis.Value;
        float targetY = 0.5f;
        float targetX = 350f;

        while (elapsedTime < _duration)
        {
            freeLookCamera.m_YAxis.Value = Mathf.Lerp(startY, targetY, elapsedTime / _duration);
            freeLookCamera.m_XAxis.Value = Mathf.Lerp(startX, targetX, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        freeLookCamera.m_YAxis.Value = targetY;
        freeLookCamera.m_XAxis.Value = targetX;
        freeLookCamera.m_YAxis.m_MaxSpeed = _YSpeed;
        freeLookCamera.m_XAxis.m_MaxSpeed = _XSpeed;

        isMoving = false;
        isStartMoveCamera = false;
    }
}