using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryOperateControl : MonoBehaviour
{
    CanvasGroup canvasGroup;
    float _duration = 1f; 
    float _delay = 10f;

    public static bool isFadeOut = false;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (isFadeOut)
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        isFadeOut = false;

        yield return new WaitForSeconds(_delay);

        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsed / _duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
