using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryBeginningEnding_Girl : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [Header("UI")]
    public GameObject buttonLeft, buttonRight;
    public GameObject buttonStart, buttonLeave;
    public Text content;

    [Header("TextFile")]
    public TextAsset textStart;
    public TextAsset textEnding;
    float _textSpend = 0.1f;
    bool isTextFinish;
    int _page;

    List<string> textList = new List<string>();

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        if (StoryUIControl_Girl.isStoryStart)
        {
            GetTextFormFile(textStart);
            StartCoroutine(StorySystemUI());
        }
        else if (StoryUIControl_Girl.isStoryEnding)
        {
            GetTextFormFile(textEnding);
            StartCoroutine(StorySystemUIAppear(canvasGroup));
        }
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
                buttonStart.SetActive(false);
                buttonLeave.SetActive(false);
                break;

            case 1:
                buttonLeft.SetActive(true);
                buttonRight.SetActive(true);
                buttonStart.SetActive(false);
                buttonLeave.SetActive(false);
                break;

            case 2:
                buttonRight.SetActive(false);
                if (StoryUIControl_Girl.isStoryStart)
                {
                    buttonStart.SetActive(true);
                }
                else if (StoryUIControl_Girl.isStoryEnding)
                {
                    buttonLeave.SetActive(true);
                }
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
        SceneManager.LoadScene(1);
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
        StoryUIControl_Girl.isStoryStart = false;
        StoryUIControl_Girl.isDialogue = true;
        StoryDialogueControl_Girl._textCount = 1;
    }
    IEnumerator StorySystemUIAppear(CanvasGroup canvasGroup)
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
        StartCoroutine(StorySystemUI());
    }
}
