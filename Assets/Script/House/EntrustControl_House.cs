using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrustControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] entrustUI;
    public Button[] deliverButton;
    public static bool isEntrustActive = false;
    bool isDeliverActive = false;
    bool isReceiveActive = false;
    bool isContentActive = false;

    private void Start()
    {
        DeliverButtonInitialState();
    }
    void Update()
    {
        entrustUI[0].SetActive(isEntrustActive);
        entrustUI[1].SetActive(isDeliverActive);
        entrustUI[2].SetActive(isReceiveActive);
        entrustUI[3].SetActive(isContentActive);

        OpenUI();
    }

    void DeliverButtonInitialState()
    {
        for (int i = 1; i < deliverButton.Length; i++)
        {
            deliverButton[i].GetComponent<CanvasGroup>().alpha = 0;
            RectTransform rect = deliverButton[i].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(6f, rect.anchoredPosition.y);
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
                StartCoroutine(AnimateButtonAppear(deliverButton[1], 0f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[2], 0.4f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[3], 0.8f, true));
                StartCoroutine(AnimateButtonAppear(deliverButton[0], 1f, false));
            }
        }
    }

    public void Button_Deliver(int _letter)
    {
        StartCoroutine(AnimateButtonDisappear(deliverButton[0], 0f, false));
        StartCoroutine(AnimateButtonDisappear(deliverButton[3], 0f, true));
        StartCoroutine(AnimateButtonDisappear(deliverButton[2], 0.4f, true));
        StartCoroutine(AnimateButtonDisappear(deliverButton[1], 0.8f, true));
    }
    public void Button_Receive()
    {
        isDeliverActive = true;
        isReceiveActive = false;
    }
    public void Button_Back()
    {
        if (isReceiveActive)
        {
            isDeliverActive = true;
            isReceiveActive = false;
        }
        else if (isContentActive)
        {
            isReceiveActive = true;
            isContentActive = false;
        }
    }
    public void Button_Letter()
    {
        isContentActive = true;
        isReceiveActive = false;
    }
    public void Button_Content()
    {
        
    }
    public void Button_Leave()
    {
        DoorControl_House.isLeave = true;
        isEntrustActive = false;
        isDeliverActive = false;
        isReceiveActive = false;
        isContentActive = false;
        entrustUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
        DeliverButtonInitialState();
    }

    IEnumerator AnimateButtonAppear(Button button, float delay, bool isShouldMove)
    {
        yield return new WaitForSeconds(delay);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        RectTransform rect = button.GetComponent<RectTransform>();

        float _timeElapsed = 0f;
        Vector2 startPosition = rect.anchoredPosition;
        Vector2 endPosition = new Vector2(0f, startPosition.y);

        while (_timeElapsed < 2f)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / 2f;
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
    }
    IEnumerator AnimateButtonDisappear(Button button, float delay, bool isShouldMove)
    {
        yield return new WaitForSeconds(delay);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        RectTransform rect = button.GetComponent<RectTransform>();

        float _timeElapsed = 0f;
        Vector2 startPosition = rect.anchoredPosition;
        Vector2 endPosition = new Vector2(6f, startPosition.y);
        button.GetComponent<CanvasGroup>().interactable = false;

        while (_timeElapsed < 2f)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / 2f;
            if (isShouldMove)
            {
                rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            }

            yield return null;
        }

        isReceiveActive = true;
        isDeliverActive = false;
        canvasGroup.alpha = 0f;
        if (isShouldMove)
        {
            rect.anchoredPosition = endPosition;
        }
    }
}
