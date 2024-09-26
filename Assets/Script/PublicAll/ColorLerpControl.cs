using UnityEngine;
using System.Collections;

public class ColorLerpControl : MonoBehaviour
{
    public Color DefaultColor = Color.white;   
    public Color HighlightColor = Color.blue;  
    public float LerpSpeed = 1f;               

    private SpriteRenderer spriteRenderer;     // SpriteRenderer �M��
    private Coroutine colorCoroutine;          // ���� Coroutine ������

    void Awake()
    {
        // �� Awake �Ы@ȡ SpriteRenderer �M��
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �z���Ƿ�ɹ�ȡ��
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + gameObject.name);
        }
    }

    void OnEnable()
    {
        // �_ʼ׃ɫ Coroutine
        if (spriteRenderer != null)
        {
            colorCoroutine = StartCoroutine(LerpColor());
        }
    }

    void OnDisable()
    {
        // ����������Õr��ֹͣ׃ɫ Coroutine �K���ɫ�֏͞�ԭ��Ę���
        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = DefaultColor;  // �֏ͳ�ʼ�ɫ
        }
    }

    IEnumerator LerpColor()
    {
        float time = 0;

        // һֱ���У�ֱ�����������
        while (true)
        {
            // ʹ�� Mathf.PingPong ��ز�ֵ�ɫ
            spriteRenderer.color = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(time, 1));

            // ���ӕr�g
            time += Time.deltaTime * LerpSpeed;

            // �ȴ���һ��
            yield return null;
        }
    }
}

