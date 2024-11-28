using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrustControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick, receive, open, turn;

    [Header("EntrustUI")]
    public GameObject[] entrustUI;
    public Image background;
    public Sprite normalBG, darkBG;
    public static bool isEntrustActive = false;
    public static int _entrustNum = 0;
    bool isDeliverActive = false;
    bool isReceiveActive = false;
    bool isContentActive = false;

    [Header("LetterDeliver")]
    public GameObject[] deliverButton;
    public GameObject[] alreadyReceived;
    public Text[] letterText;
    public Sprite normalButton, disabledButton;
    bool isAlready = false;

    [Header("LetterReceive")]
    public Image receiveImage;
    public Sprite[] receiveSprite;
    bool isReceive = false;

    [Header("LetterContent")]
    public Image contentImage;
    public Sprite[] contentSprite;

    void Start()
    {
        DeliverButtonInitialState();
    }

    void Update()
    {
        EntrustUI();
        OpenUI();
        LetterDeliverDay();
        LetterReceiveAndContent();
    }

    void DeliverButtonInitialState()
    {
        isReceive = false;
        for (int i = 1; i < deliverButton.Length; i++)
        {
            deliverButton[i].GetComponent<CanvasGroup>().alpha = 0;
            RectTransform rect = deliverButton[i].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(20f, rect.anchoredPosition.y);
        }
    }
    void EntrustUI()
    {
        entrustUI[0].SetActive(isEntrustActive);
        entrustUI[1].SetActive(isDeliverActive);
        entrustUI[2].SetActive(isReceiveActive);
        entrustUI[3].SetActive(isContentActive);

        if (isDeliverActive)
        {
            background.sprite = normalBG;
        }
        else
        {
            background.sprite = darkBG;
        }
    }
    void OpenUI()
    {
        if (!isEntrustActive) return;

        if (entrustUI[0].GetComponent<RectTransform>().localScale.x < 1)
        {
            entrustUI[0].GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
        }
        else
        {
            if (!isDeliverActive && !isReceiveActive && !isContentActive)
            {
                isDeliverActive = true;
                StartCoroutine(AnimateButtonAppear(deliverButton[1].GetComponent<Button>(), 0f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[2].GetComponent<Button>(), 0.4f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[3].GetComponent<Button>(), 0.8f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[0].GetComponent<Button>(), 1f, false));
            }
        }
    }
    void LetterDeliverDay()
    {
        switch (GameControl_House._day)
        {
            case 1:
                deliverButton[1].GetComponent<Image>().sprite = normalButton;
                letterText[1].text = "和奶奶的回憶...";
                deliverButton[2].GetComponent<Image>().sprite = disabledButton;
                deliverButton[2].GetComponent<Button>().interactable = false;
                deliverButton[3].GetComponent<Image>().sprite = disabledButton;
                deliverButton[3].GetComponent<Button>().interactable = false;
                break;

            case 2:
                deliverButton[1].GetComponent<Image>().sprite = normalButton;
                letterText[1].text = "友情的記憶";
                deliverButton[2].GetComponent<Image>().sprite = disabledButton;
                deliverButton[2].GetComponent<Button>().interactable = false;
                deliverButton[3].GetComponent<Image>().sprite = disabledButton;
                deliverButton[3].GetComponent<Button>().interactable = false;
                break;

            case 3:
                deliverButton[1].GetComponent<Image>().sprite = normalButton;
                letterText[1].text = "未來的迷茫";
                deliverButton[2].GetComponent<Image>().sprite = disabledButton;
                deliverButton[2].GetComponent<Button>().interactable = false;
                deliverButton[3].GetComponent<Image>().sprite = disabledButton;
                deliverButton[3].GetComponent<Button>().interactable = false;
                break;
        }
    }
    void LetterReceiveAndContent()
    {
        if (_entrustNum == 0) return;

        receiveImage.sprite = receiveSprite[_entrustNum];
        contentImage.sprite = contentSprite[_entrustNum];
    }
    
    public void Button_Deliver(int _letter)
    {
        BGM.PlayOneShot(turn);
        _entrustNum = _letter;
        BirdControl_House.isDeliver_Close = true;
        StartCoroutine(AnimateButtonDisappear(deliverButton[0].GetComponent<Button>(), 0f, false));
        StartCoroutine(AnimateButtonDisappear(deliverButton[3].GetComponent<Button>(), 0f, true));
        StartCoroutine(AnimateButtonDisappear(deliverButton[2].GetComponent<Button>(), 0.4f, true));
        StartCoroutine(AnimateButtonDisappear(deliverButton[1].GetComponent<Button>(), 0.8f, true));
    }
    public void Button_Receive()
    {
        BGM.PlayOneShot(receive);
        isReceive = true;
        isReceiveActive = false;
        isDeliverActive = true;
        if (!alreadyReceived[_entrustNum].activeSelf)
        {
            isAlready = true;
        }
        BirdControl_House.isHappy = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 3;
        GameControl_House._storyNum = _entrustNum;
        StartCoroutine(AnimateButtonAppear(deliverButton[1].GetComponent<Button>(), 0f, true));
        StartCoroutine(AnimateButtonAppear(deliverButton[2].GetComponent<Button>(), 0.4f, true));
        StartCoroutine(AnimateButtonAppear(deliverButton[3].GetComponent<Button>(), 0.8f, true));
        StartCoroutine(AnimateButtonAppear(deliverButton[0].GetComponent<Button>(), 1f, false));
    }
    public void Button_Back()
    {
        BGM.PlayOneShot(onClick);
        if (isReceiveActive)
        {
            if (isReceive)
            {
                isReceiveActive = false;
                isDeliverActive = true;
                BirdControl_House.isDeliver = true;
                DialogueControl_House.isAutoNext = true;
                DialogueControl_House._paragraph = 4;
                StartCoroutine(AnimateButtonAppear(deliverButton[1].GetComponent<Button>(), 0f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[2].GetComponent<Button>(), 0.4f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[3].GetComponent<Button>(), 0.8f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[0].GetComponent<Button>(), 1f, false));
            }
        }
        else if (isContentActive)
        {
            isReceiveActive = true;
            isContentActive = false;
        }
    }
    public void Button_Letter()
    {
        BGM.PlayOneShot(open);
        isContentActive = true;
        isReceiveActive = false;
    }
    public void Button_Leave()
    {
        BGM.PlayOneShot(onClick);
        BirdControl_House.isBye = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 5;
        entrustUI[1].GetComponent<CanvasGroup>().interactable = false;
        Invoke("LeaveState", 1f);
    }

    void LeaveState()
    {
        DoorControl_House.isLeave = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 6;
        isEntrustActive = false;
        isDeliverActive = false;
        isReceiveActive = false;
        isContentActive = false;
        for (int i = 1; i < alreadyReceived.Length; i++)
        {
            alreadyReceived[i].SetActive(false);
        }
        entrustUI[1].GetComponent<CanvasGroup>().interactable = true;
        entrustUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
        DeliverButtonInitialState();
    }

    IEnumerator AnimateButtonAppear(Button button, float delay, bool isShouldMove)
    {
        yield return new WaitForSeconds(delay);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        RectTransform rect = button.GetComponent<RectTransform>();

        float _duration = 1f;
        float _timeElapsed = 0f;
        Vector2 startPosition = rect.anchoredPosition;
        Vector2 endPosition = new Vector2(9f, startPosition.y);

        while (_timeElapsed < _duration)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / _duration;
            if (isShouldMove)
            {
                rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            }

            yield return null;
        }

        canvasGroup.alpha = 1f;
        button.GetComponent<CanvasGroup>().interactable = true;
        if (isShouldMove)
        {
            rect.anchoredPosition = endPosition;
        }
        if (isAlready)
        {
            alreadyReceived[_entrustNum].SetActive(true);
            StartCoroutine(AnimateAlreadyReceived());
            isAlready = false;
        }
    }
    IEnumerator AnimateButtonDisappear(Button button, float delay, bool isShouldMove)
    {
        yield return new WaitForSeconds(delay);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        RectTransform rect = button.GetComponent<RectTransform>();

        float _duration = 0.5f;
        float _timeElapsed = 0f;
        Vector2 startPosition = rect.anchoredPosition;
        Vector2 endPosition = new Vector2(20f, startPosition.y);
        button.GetComponent<CanvasGroup>().interactable = false;

        while (_timeElapsed < _duration)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / _duration;
            if (isShouldMove)
            {
                rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            }

            yield return null;
        }

        canvasGroup.alpha = 0f;
        if (isShouldMove)
        {
            rect.anchoredPosition = endPosition;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(AnimateReceiveAppear());
            isReceiveActive = true;
            isDeliverActive = false;
        }
    }
    IEnumerator AnimateReceiveAppear()
    {
        CanvasGroup canvasGroup = entrustUI[2].GetComponent<CanvasGroup>();
        RectTransform rect = entrustUI[2].GetComponent<RectTransform>();

        Vector3 startScale = new Vector3(0.7f, 0.7f, 1f);
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        float _duration = 0.6f;
        float _timeElapsed = 0f;
        canvasGroup.alpha = 0;
        rect.localScale = startScale;

        while (_timeElapsed < _duration)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / _duration;
            rect.localScale = Vector3.Lerp(startScale, targetScale, t);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        canvasGroup.alpha = 1;
        rect.localScale = targetScale;
    }
    IEnumerator AnimateAlreadyReceived()
    {
        CanvasGroup canvasGroup = alreadyReceived[_entrustNum].GetComponent<CanvasGroup>();
        RectTransform rect = alreadyReceived[_entrustNum].GetComponent<RectTransform>();

        Vector3 startScale = new Vector3(1.4f, 0.4f, 1f);
        Vector3 targetScale = new Vector3(0.6f, 0.18f, 1f);

        float _duration = 0.3f;
        float _timeElapsed = 0f;
        canvasGroup.alpha = 0;
        rect.localScale = startScale;

        while (_timeElapsed < _duration)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / _duration;
            rect.localScale = Vector3.Lerp(startScale, targetScale, t);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        canvasGroup.alpha = 1;
        rect.localScale = targetScale;
    }
}
