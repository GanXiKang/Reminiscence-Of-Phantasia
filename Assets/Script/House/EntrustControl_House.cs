using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrustControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] entrustUI;
    public Button[] letterButton;
    public static bool isEntrustActive = false;
    bool isLetterActive = false;
    bool isContentActive = false;

    private void Start()
    {
        LetterButtonInitialState();
    }
    void Update()
    {
        entrustUI[0].SetActive(isEntrustActive);
        entrustUI[1].SetActive(isLetterActive);
        entrustUI[2].SetActive(isContentActive);

        OpenUI();
    }

    void LetterButtonInitialState()
    {
        for (int i = 1; i < letterButton.Length; i++)
        {
            letterButton[i].GetComponent<CanvasGroup>().alpha = 0;
            RectTransform rect = letterButton[i].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(10f, rect.anchoredPosition.y);
        }
    }
    void OpenUI()
    {
        if (isEntrustActive)
        {
            if (entrustUI[0].GetComponent<RectTransform>().localScale.x < 1)
            {
                entrustUI[0].GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
            }
            else
            {
                if (!isLetterActive && !isContentActive)
                {
                    isLetterActive = true;
                    AnimateButton(letterButton[1], 0f);
                    AnimateButton(letterButton[2], 0.2f);
                    AnimateButton(letterButton[3], 0.4f);
                }
            }
        }
        else
        {
            entrustUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
            isLetterActive = false;
            isContentActive = false;
            LetterButtonInitialState();
        }
    }

    public void Button_Letter(int _letter)
    {
        isContentActive = true;
        isLetterActive = false;
    }

    IEnumerator AnimateButton(Button button, float delay)
    {
        print("OK");
        yield return new WaitForSeconds(delay);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        RectTransform rect = button.GetComponent<RectTransform>();

        float _timeElapsed = 0f;
        Vector2 startPosition = rect.anchoredPosition;
        Vector2 endPosition = new Vector2(1f, startPosition.y);

        while (_timeElapsed < 2f)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / 2f;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        canvasGroup.alpha = 1f;
        rect.anchoredPosition = endPosition;
    }
}
