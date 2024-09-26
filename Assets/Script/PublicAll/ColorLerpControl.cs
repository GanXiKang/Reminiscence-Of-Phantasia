using UnityEngine;
using System.Collections;

public class ColorLerpControl : MonoBehaviour
{
    public Color defaultColor = Color.white;   
    public Color highlightColor = Color.blue;  
    public float _lerpSpeed = 1f;               

    private SpriteRenderer spriteRenderer;
    private Coroutine colorCoroutine;          

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        if (spriteRenderer != null)
        {
            colorCoroutine = StartCoroutine(LerpColor());
        }
    }
    void OnDisable()
    {
        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = defaultColor;
        }
    }

    IEnumerator LerpColor()
    {
        float time = 0;

        while (true)
        {
            spriteRenderer.color = Color.Lerp(defaultColor, highlightColor, Mathf.PingPong(time, 1));
            time += Time.deltaTime * _lerpSpeed;
            yield return null;
        }
    }
}

