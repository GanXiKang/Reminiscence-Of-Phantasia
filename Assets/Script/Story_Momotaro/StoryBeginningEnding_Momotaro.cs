using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryBeginningEnding_Momotaro : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip riverSideBGM, EndingBGM;
    public AudioClip page, coroutine;

    [Header("UI")]
    public GameObject interactable;
    public GameObject buttonLeft, buttonRight;
    public GameObject buttonCoroutine;
    public Text content;
    public Image background;
    public Sprite[] pageImages;
    public Image image;
    public Sprite[] storyImages;
    bool isChangePageLeft = false;
    bool isChangePageRight = false;
    bool isAnim = false;

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
        if (StoryUIControl_Momotaro.isStoryStart)
        {
            GetTextFormFile(textStart);
            StartCoroutine(StorySystemUI());
            StartCoroutine(StoryFillImage());
        }
        else if (StoryUIControl_Momotaro.isStoryEnding)
        {
            GetTextFormFile(textEnding);
            StartCoroutine(StorySystemUIAppear(canvasGroup));
            BGM.Stop();
            BGM.clip = EndingBGM;
            BGM.Play();
        }
    }

    void Update()
    {
        TextController();
        ButtonActive();
        ChangePage();
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
                buttonRight.SetActive(true);
                buttonCoroutine.SetActive(false);
                break;

            case 1:
                buttonLeft.SetActive(true);
                buttonRight.SetActive(true);
                buttonCoroutine.SetActive(false);
                break;

            case 2:
                buttonLeft.SetActive(true);
                buttonRight.SetActive(false);
                break;
        }
    }
    void ChangePage()
    {
        interactable.SetActive(!isAnim);

        if (isChangePageLeft && !isAnim)
        {
            isAnim = true;
            StartCoroutine(BackgroundChangePageLeft());
        }
        else if (isChangePageRight && !isAnim)
        {
            isAnim = true;
            StartCoroutine(BackgroundChangePageRight());
        }
    }

    public void Button_Left()
    {
        if (isTextFinish)
        {
            _page--;
            BGM.PlayOneShot(page);
            isChangePageRight = true;
            ResetTextSpeed();
        }
    }
    public void Button_Right()
    {
        if (isTextFinish)
        {
            _page++;
            BGM.PlayOneShot(page);
            isChangePageLeft = true;
            ResetTextSpeed();
            if (_page == 2)
            {
                Invoke("ButtonCoroutineActive", 2.2f);
            }
        }
    }
    public void Button_Coroutine()
    {
        BGM.PlayOneShot(coroutine);
        if (StoryUIControl_Momotaro.isStoryStart)
        {
            BGM.Stop();
            BGM.clip = riverSideBGM;
            BGM.Play();
            StartCoroutine(StorySystemUIDisappear(canvasGroup));
            StoryOperateControl.isFadeOut = true;
        }
        else if (StoryUIControl_Momotaro.isStoryEnding)
        {
            TransitionUIControl.isTransitionUIAnim_In = true;
            Invoke("GoToHouse", 1f);
        }
    }

    void ButtonCoroutineActive()
    {
        if (_page != 2) return;
        {
            if (StoryUIControl_Momotaro.isStoryStart)
            {
                buttonCoroutine.SetActive(true);
            }
            else if (StoryUIControl_Momotaro.isStoryEnding)
            {
                buttonCoroutine.SetActive(true);
            }
        }
    }
    void GoToHouse()
    {
        GameControl_House._day++;
        SceneManager.LoadScene(1);
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
        image.fillAmount = 0;
        StoryUIControl_Momotaro.isStoryStart = false;
        StoryUIControl_Momotaro.isDialogue = true;
        StoryDialogueControl_Momotaro._textCount = 1;
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
        StartCoroutine(StoryFillImage());
    }
    IEnumerator BackgroundChangePageLeft()
    {
        int index = 0;

        while (index < 6)
        {
            index++;
            background.sprite = pageImages[index];
            yield return new WaitForSeconds(0.15f);
        }

        background.sprite = pageImages[0];
        isAnim = false;
        isChangePageLeft = false;
        StartCoroutine(StorySystemUI());
        StartCoroutine(StoryFillImage());
    }
    IEnumerator BackgroundChangePageRight()
    {
        int index = 7;

        while (index < 12)
        {
            index++;
            background.sprite = pageImages[index];
            yield return new WaitForSeconds(0.15f);
        }

        background.sprite = pageImages[0];
        isAnim = false;
        isChangePageRight = false;
        StartCoroutine(StorySystemUI());
        StartCoroutine(StoryFillImage());
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
    IEnumerator StoryFillImage()
    {
        float elapsedTime = 0f;
        float fillDuration = 0.5f;

        image.fillAmount = 0;

        if (StoryUIControl_Momotaro.isStoryStart)
        {
            image.sprite = storyImages[_page];
        }
        else if (StoryUIControl_Momotaro.isStoryEnding)
        {
            image.sprite = storyImages[_page + 3];
        }

        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            image.fillAmount = Mathf.Clamp01(elapsedTime / fillDuration);
            yield return null;
        }

        image.fillAmount = 1;
    }
}
