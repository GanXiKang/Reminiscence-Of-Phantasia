using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionUIControl : MonoBehaviour
{
    public Vector3 startScale = new Vector3(350f, 200f, 1f);
    public Vector3 targetScale = new Vector3(19.5f, 11f, 1f);
    public float animationDuration = 1f;
    private bool isAnimating = false;
    private float animationTime = 0f;

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        // 檢查是否應該啟動動畫
        if (isAnimating)
        {
            // 增加時間，並確保時間不會超過動畫持續時間
            animationTime += Time.deltaTime;
            float t = animationTime / animationDuration;

            // 使用 Lerp 將物件的縮放從 startScale 漸變到 targetScale
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            // 當動畫結束時，停止動畫
            if (animationTime >= animationDuration)
            {
                isAnimating = false;
            }
        }
    }

    // 調用此方法以啟動動畫
    public void StartScalingAnimation()
    {
        isAnimating = true;
        animationTime = 0f; // 重置動畫時間
    }
}
