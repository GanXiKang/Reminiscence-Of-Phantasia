using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraStartMove_House : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float _duration = 2.0f; 
    public static bool isStartMoveCamera = false; 
    
    bool isMoving = false;
    float _YSpeed = 2;
    float _XSpeed = 100;

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

        // **關鍵 1：記錄原本的相機速度，然後禁用控制**
        originalYAxisSpeed = freeLookCamera.m_YAxis.m_MaxSpeed;
        originalXAxisSpeed = freeLookCamera.m_XAxis.m_MaxSpeed;
        freeLookCamera.m_YAxis.m_MaxSpeed = 0; // 鎖定垂直控制
        freeLookCamera.m_XAxis.m_MaxSpeed = 0; // 鎖定水平控制

        float elapsedTime = 0;
        float startY = freeLookCamera.m_YAxis.Value;
        float startX = freeLookCamera.m_XAxis.Value;
        float targetY = 0.5f;
        float targetX = 350f;

        while (elapsedTime < transitionDuration)
        {
            freeLookCamera.m_YAxis.Value = Mathf.Lerp(startY, targetY, elapsedTime / transitionDuration);
            freeLookCamera.m_XAxis.Value = Mathf.Lerp(startX, targetX, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 確保最終值正確
        freeLookCamera.m_YAxis.Value = targetY;
        freeLookCamera.m_XAxis.Value = targetX;

        // **關鍵 2：恢復玩家控制**
        freeLookCamera.m_YAxis.m_MaxSpeed = originalYAxisSpeed;
        freeLookCamera.m_XAxis.m_MaxSpeed = originalXAxisSpeed;

        isMoving = false;
    }
}