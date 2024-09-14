using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBeginningEnding_Girl : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [Header("UI")]
    public GameObject storyUI;
    public GameObject buttonLeft, buttonRight;
    public GameObject buttonStart, buttonLeave;
    public Text content;
    public static bool isStory = false;

    [Header("TextFile")]
    public TextAsset textStart;
    public TextAsset textEnding;
    public static bool isStoryEnding = false;
    float _textSpend = 0.1f;
    bool isTextFinish;
    int _page;

    List<string> textList = new List<string>();

    void Start()
    {
        canvasGroup = storyUI.GetComponent<CanvasGroup>();

        GetTextFormFile(textStart);
        StartCoroutine(StorySystemUI());
    }

    
    void Update()
    {
        storyUI.SetActive(isStory);

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
        if (!isTextFinish)
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
            case 0:
                buttonLeft.SetActive(false);
                break;

            case 1:
                buttonLeft.SetActive(true);
                buttonRight.SetActive(true);
                buttonStart.SetActive(false);
                buttonLeave.SetActive(false);
                break;

            case 2:
                buttonRight.SetActive(false);
                break;
        }
    }

    public void Button_Left()
    {
        if (isTextFinish)
        {
            _page--;
            ResetTextSpeed();
            StartCoroutine(StorySystemUI());
        }
    }
    public void Button_Right()
    {
        if (isTextFinish)
        {
            _page++;
            ResetTextSpeed();
            StartCoroutine(StorySystemUI());
        }
    }
    public void Button_StartGame()
    {
        StartCoroutine(StorySystemUIDisappear(canvasGroup));
    }
    public void Button_LeaveStory()
    {
        StartCoroutine(StorySystemUIDisappear(canvasGroup));
    }

    IEnumerator StorySystemUI()
    {
        isTextFinish = false;
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
        isTextFinish = true;
        if (_page == 2)
        {
            buttonStart.SetActive(true);
        }
    }
    IEnumerator StorySystemUIDisappear(CanvasGroup canvasGroup)
    {
        float duration = 1f;
        float elapsed = 0f;

        float startAlpha = 1f;
        float targetAlpha = 0f;

        canvasGroup.alpha = startAlpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        isStory = false;
    }
}
