using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerpControl : MonoBehaviour
{
    public Color DefaultColor = Color.white;
    public Color HighlightColor = Color.blue;
    public float _lerpSpeed = 1f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + gameObject.name + " or its children.");
            return;
        }

        DefaultColor = spriteRenderer.color;

        StartCoroutine(UpdateColor());
    }

    void OnEnable()
    {
        StartCoroutine(UpdateColor());
    }

    IEnumerator UpdateColor()
    {
        Color lerpedColor = Color.white;
        float currentTime = 0;

        while (this.enabled)
        {
            lerpedColor = Color.Lerp(DefaultColor, HighlightColor, Mathf.PingPong(currentTime += (Time.deltaTime * _lerpSpeed / 1), 1));
            spriteRenderer.color = lerpedColor;

            yield return new WaitForEndOfFrame();
        }

        //spriteRenderer.color = DefaultColor;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = DefaultColor;
        }
    }
}
