using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip leave, back, comfirm, info, buy;

    [Header("UI")]
    public GameObject[] storeUI;
    public GameObject[] contentUI;
    public Button[] homePageButton;
    public Image backgound;
    public Sprite main, content;
    public static bool isStoreActive = false;
    bool isHomePageActive = false;
    bool isContentActive = false;

    [Header("Coin")]
    public Text coinAmount;
    public Button buyButton;
    public static int _MyCoin = 300;
    int _productCoin = 0;
    bool isBuy = false;

    [Header("ProductContent")]
    public GameObject[] scissors;
    public GameObject[] auxiliary;
    public GameObject[] shading;
    public GameObject[] decorate;

    void Update()
    {
        OpenUI();
        StoreUI();
        Coin();
    }

    void OpenUI()
    {
        if (!isStoreActive) return;

        if (storeUI[0].GetComponent<RectTransform>().localScale.x < 1)
        {
            storeUI[0].GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
        }
        else
        {
            if (!isHomePageActive && !isContentActive)
            {
                isHomePageActive = true;
                StartCoroutine(AnimateButtonAppear(homePageButton[1], 0f));
                StartCoroutine(AnimateButtonAppear(homePageButton[2], 0.3f));
                StartCoroutine(AnimateButtonAppear(homePageButton[3], 0.6f));
                StartCoroutine(AnimateButtonAppear(homePageButton[4], 0.9f));
            }
        }

        homePageButton[0].GetComponent<CanvasGroup>().interactable = isBuy;
        if (!isBuy)
            homePageButton[0].GetComponent<CanvasGroup>().alpha = 0;
        else
            homePageButton[0].GetComponent<CanvasGroup>().alpha = 1;
    }
    void StoreUI()
    {
        storeUI[0].SetActive(isStoreActive);
        storeUI[1].SetActive(isHomePageActive);
        storeUI[2].SetActive(isContentActive);

        if (isHomePageActive)
        {
            backgound.sprite = main;
        }
        else
        {
            backgound.sprite = content;
        }
    }
    void Coin()
    {
        coinAmount.text = _MyCoin + " / " + _productCoin;
        buyButton.interactable = _MyCoin >= _productCoin;
        if (_productCoin == 0)
        {
            buyButton.interactable = false;
        }
    }
    void LeaveState()
    {
        DoorControl_House.isLeave = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 7;
        isHomePageActive = false;
        isContentActive = false;
        storeUI[1].GetComponent<CanvasGroup>().interactable = true;
        storeUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    public void Button_Product(int _product)
    {
        BGM.PlayOneShot(comfirm);
        if (isHomePageActive)
        {
            _productCoin = 0;
            CatControl_House.isBag = true;
            StartCoroutine(AnimateButtonDisappear(homePageButton[4], 0f));
            StartCoroutine(AnimateButtonDisappear(homePageButton[3], 0.2f));
            StartCoroutine(AnimateButtonDisappear(homePageButton[2], 0.4f));
            StartCoroutine(AnimateButtonDisappear(homePageButton[1], 0.6f));
            for (int p = 1; p < contentUI.Length; p++)
            {
                if (p == _product)
                {
                    contentUI[p].SetActive(true);
                }
                else
                {
                    contentUI[p].SetActive(false);
                }
            }
        }
        else
        {
            _productCoin = 0;
            for (int p = 1; p < contentUI.Length; p++)
            {
                if (p == _product)
                {
                    contentUI[p].SetActive(true);
                }
                else
                {
                    contentUI[p].SetActive(false);
                }
            }
        }
    }
    public void Button_Content(int _content)
    {
        BGM.PlayOneShot(comfirm);
        if (contentUI[1].activeSelf)
        {
            switch (_content)
            {
                case 1:
                    _productCoin = 250;
                    break;
            }
        }
    }
    public void Button_Info(int _num)
    {
        BGM.PlayOneShot(info);
        if (contentUI[1].activeSelf)
        {
            switch (_num)
            {
                case 1:
                    if (scissors[1].activeSelf)
                    {
                        scissors[1].SetActive(false);
                        scissors[2].SetActive(true);
                    }
                    else
                    {
                        scissors[1].SetActive(true);
                        scissors[2].SetActive(false);
                    }
                    break;
            }
        }
    }
    public void Button_Buy()
    {
        BGM.PlayOneShot(buy);
        isBuy = true;
        _MyCoin -= _productCoin;
        CatControl_House.isHappy = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 4;
    }
    public void Button_Back()
    {
        BGM.PlayOneShot(back);
        isHomePageActive = true;
        isContentActive = false;
        CatControl_House.isBag_On = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 5;
        StartCoroutine(AnimateButtonAppear(homePageButton[1], 0f));
        StartCoroutine(AnimateButtonAppear(homePageButton[2], 0.3f));
        StartCoroutine(AnimateButtonAppear(homePageButton[3], 0.6f));
        StartCoroutine(AnimateButtonAppear(homePageButton[4], 0.9f));
    }
    public void Button_Leave()
    {
        BGM.PlayOneShot(leave);
        if (GameControl_House._storyNum != 0)
        {
            CatControl_House.isBye = true;
            DialogueControl_House.isAutoNext = true;
            DialogueControl_House._paragraph = 6;
            storeUI[1].GetComponent<CanvasGroup>().interactable = false;
            Invoke("LeaveState", 1f);
        }
        else
        {
            CatControl_House.isBye = true;
            DialogueControl_House.isAutoNext = true;
            DialogueControl_House._paragraph = 6;
            storeUI[1].GetComponent<CanvasGroup>().interactable = false;
            Invoke("LeaveState", 1f);
        }
    }

    IEnumerator AnimateButtonAppear(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);

        float duration = 1f;
        float elapsed = 0f;

        float startAlpha = 0f;
        float targetAlpha = 1f;

        Vector3 startScale = new Vector3(2.5f, 13f, 1f);
        Vector3 targetScale = new Vector3(3f, 14f, 1f);

        Quaternion startRotation = Quaternion.Euler(0, 0, 45);
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        canvasGroup.interactable = false;
        canvasGroup.alpha = startAlpha;
        rectTransform.localScale = startScale;
        rectTransform.rotation = startRotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);
            rectTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        canvasGroup.interactable = true;
        canvasGroup.alpha = targetAlpha;
        rectTransform.localScale = targetScale;
        rectTransform.rotation = targetRotation;
    }
    IEnumerator AnimateButtonDisappear(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);

        float duration = 0.8f;
        float elapsed = 0f;

        float startAlpha = 1f;
        float targetAlpha = 0f;

        Vector3 startScale = new Vector3(3f, 14f, 1f);
        Vector3 targetScale = new Vector3(2.5f, 13f, 1f);

        Quaternion startRotation = Quaternion.Euler(0, 0, 0);
        Quaternion targetRotation = Quaternion.Euler(0, 0, 45);

        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        canvasGroup.interactable = false;
        canvasGroup.alpha = startAlpha;
        rectTransform.localScale = startScale;
        rectTransform.rotation = startRotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);
            rectTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        isContentActive = true;
        isHomePageActive = false;
        canvasGroup.interactable = true;
        canvasGroup.alpha = targetAlpha;
        rectTransform.localScale = targetScale;
        rectTransform.rotation = targetRotation;
    }
}
