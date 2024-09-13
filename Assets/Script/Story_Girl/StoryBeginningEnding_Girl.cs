using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBeginningEnding_Girl : MonoBehaviour
{
    [Header("UI")]
    public GameObject storyUI;
    public GameObject buttonLeft, buttonRight;
    public Text content;
    public static bool isStory;

    [Header("TextFile")]
    public TextAsset textStart;
    public TextAsset textEnding;
    public static bool isStoryEnding = false;
    float _textSpend = 0.1f;
    bool textFinish;
    int _page;

    List<string> textList = new List<string>();

    void Start()
    {
        GetTextFormFile(textStart);
        StartCoroutine(StorySystemUI());
    }

    
    void Update()
    {
        TextController();
        ButtonActive();
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        _page = 0;

        var lineDate = file.text.Split("\n");

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    void TextController()
    {
        if (!textFinish)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _textSpend = 0f;
            }
        }
    }
    void ResetTextSpeed()
    {
        if (_textSpend != 0.1f)
            _textSpend = 0.1f;
    }
    void ButtonActive()
    {
        switch (_page)
        {
            case 1:
                buttonLeft.SetActive(false);
                break;

            case 2:
                buttonLeft.SetActive(true);
                buttonRight.SetActive(true);
                break;

            case 3:
                buttonRight.SetActive(false);
                break;
        }
    }

    public void Button_Left()
    {
        _page--;
        ResetTextSpeed();
        StartCoroutine(StorySystemUI());
    }
    public void Button_Right()
    {
        _page++;
        ResetTextSpeed();
        StartCoroutine(StorySystemUI());
    }

    IEnumerator StorySystemUI()
    {
        textFinish = false;
        content.text = "";
        for (int i = 0; i < textList[_page].Length; i++)
        {
            if (textList[_page][i] == '\\' && i + 1 < textList[_page].Length && textList[_page][i + 1] == 'n')
            {
                content.text += "\n";
                i++;
            }
            else
            {
                content.text += textList[_page][i]; 
            }

            yield return new WaitForSeconds(_textSpend);
        }
        textFinish = true;
    }
    IEnumerator StorySystemUIDisappear(CanvasGroup canvasGroup)
    {
        float duration = 1f;
        float elapsed = 0f;

        float startAlpha = 0f;
        float targetAlpha = 1f;

        canvasGroup.alpha = startAlpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
