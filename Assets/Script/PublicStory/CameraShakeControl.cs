using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeControl : MonoBehaviour
{
    public Transform cameraTransform; // 鏡頭的Transform
    public float shakeDuration = 0.5f; // 震動持續時間
    public float shakeAmount = 0.1f; // 震動強度
    public float decreaseFactor = 1.0f; // 震動衰減速度

    private Vector3 originalPos;
    private float currentShakeDuration = 0f;

    void Start()
    {
        originalPos = cameraTransform.localPosition; // 記錄原始位置
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            // 隨機生成一個偏移量來抖動攝影機
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            // 逐漸減少震動時間
            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            // 恢復到原始位置
            currentShakeDuration = 0f;
            cameraTransform.localPosition = originalPos;
        }
    }

    public void TriggerShake(float duration)
    {
        // 啟動震動
        currentShakeDuration = duration;
    }
}
