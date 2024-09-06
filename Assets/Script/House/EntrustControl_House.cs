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
    bool isContentActive = false;

    private void Start()
    {
        DeliverButtonInitialState();
    }
    void Update()
    {
        entrustUI[0].SetActive(isEntrustActive);
        entrustUI[1].SetActive(isDeliverActive);
        entrustUI[2].SetActive(isContentActive);

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
        if (isEntrustActive)
        {
            if (entrustUI[0].GetComponent<RectTransform>().localScale.x < 1)
            {
                entrustUI[0].GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
            }
            else
            {
                if (!isDeliverActive && !isContentActive)
                {
                    isDeliverActive = true;
                    StartCoroutine(AnimateButton(deliverButton[1], 0f));
                    StartCoroutine(AnimateButton(deliverButton[2], 0.4f));
                    StartCoroutine(AnimateButton(deliverButton[3], 0.8f));
                }
            }
        }
        else
        {
            entrustUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
            isDeliverActive = false;
            isContentActive = false;
            DeliverButtonInitialState();
        }
    }

    public void Button_Deliver(int _letter)
    {
        isContentActive = true;
        isDeliverActive = false;
    }
    public void Button_Receive()
    {

    }
    public void Button_Back()
    {
        
    }
    public void Button_Letter()
    {
        
    }

    IEnumerator AnimateButton(Button button, float delay)
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
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        canvasGroup.alpha = 1f;
        rect.anchoredPosition = endPosition;
    }
}
