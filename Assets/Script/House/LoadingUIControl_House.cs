using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIControl_House : MonoBehaviour
{
    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a, b, c;
    float value = 1;
    public float _loadingSpeed = 1.5f;
    public static bool isCloseLoadingUI = false;
    public static bool isOpenLoadingUI = false;

    [Header("BlackScreen")]
    public GameObject blackScreen;
    public Image panel;
    Color currentColor;
    float _alpha = 0f;
    public float _screenSpeed = 10f;
    public static bool isCloseBlackScreen = false;
    public static bool isOpenBlackScreen = false;

    void Start()
    {
        loadingUI.SetActive(true);
        isCloseLoadingUI = true;

        currentColor = panel.color;
    }

    void Update()
    {
        a.fillAmount = value;
        b.fillAmount = value;
        c.fillAmount = value;
        currentColor.a = _alpha;
        panel.color = currentColor;

        CloseLoadingUI();
        OpenLoadingUI();
        CloseBlackScreen();
        OpenBlackScreen();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOpenBlackScreen = true;
        }
    }

    void CloseLoadingUI()
    {
        if (isCloseLoadingUI)
        {
            if (value > 0)
            {
                value -= _loadingSpeed * Time.deltaTime;
            }
            else
            {
                isCloseLoadingUI = false;
                loadingUI.SetActive(false);
            }
        }
    }
    void OpenLoadingUI()
    {
        if (isOpenLoadingUI)
        {
            if (value < 1)
            {
                value += _loadingSpeed * Time.deltaTime;
            }
            else
            {
                isOpenLoadingUI = false;
            }
        }
    }
    void CloseBlackScreen()
    {
        if (isCloseBlackScreen)
        {
            if (value > 0)
            {
                value -= _screenSpeed * Time.deltaTime;
            }
            else
            {
                isCloseBlackScreen = false;
                blackScreen.SetActive(false);
            }
        }
    }
    void OpenBlackScreen()
    {
        if (isOpenBlackScreen)
        {
            if (value < 255)
            {
                value += _screenSpeed * Time.deltaTime;
            }
            else
            {
                isOpenBlackScreen = false;
            }
        }
    }
    
}
