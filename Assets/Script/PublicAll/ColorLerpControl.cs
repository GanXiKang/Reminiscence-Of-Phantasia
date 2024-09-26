using UnityEngine;
using System.Collections;

public class ColorLerpControl : MonoBehaviour
{
    public Color DefaultColor = Color.white;   
    public Color HighlightColor = Color.blue;  
    public float LerpSpeed = 1f;               

    private SpriteRenderer spriteRenderer;     // SpriteRenderer 組件
    private Coroutine colorCoroutine;          // 保存 Coroutine 的引用

    void Awake()
    {
        // 在 Awake 中獲取 SpriteRenderer 組件
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 檢查是否成功取得
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + gameObject.name);
        }
    }

    void OnEnable()
    {
        // 開始變色 Coroutine
        if (spriteRenderer != null)
        {
            colorCoroutine = StartCoroutine(LerpColor());
        }
    }

    void OnDisable()
    {
        // 當物件被禁用時，停止變色 Coroutine 並將顏色恢復為原來的樣子
        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = DefaultColor;  // 恢復初始顏色
        }
    }

    IEnumerator LerpColor()
    {
        float time = 0;

        // 一直執行，直到物件被禁用
        while (true)
        {
            // 使用 Mathf.PingPong 來回插值顏色
            spriteRenderer.color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(time, 1));

            // 增加時間
            time += Time.deltaTime * LerpSpeed;

            // 等待下一幀
            yield return null;
        }
    }
}

