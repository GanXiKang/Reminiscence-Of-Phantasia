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
    public float _screenSpeed = 50f;
    public static bool isCloseBlackScreen = false;
    public static bool isOpenBlackScreen = false;

    void Start()
    {
        isCloseLoadingUI = true;

        currentColor = panel.color;
    }

    void Update()
    {
        loadingUI.SetActive(isCloseLoadingUI || isOpenLoadingUI);
        a.fillAmount = value;
        b.fillAmount = value;
        c.fillAmount = value;
        CloseLoadingUI();
        OpenLoadingUI();

        currentColor.a = _alpha;
        panel.color = currentColor;
        OpenBlackScreen();
        CloseBlackScreen();
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
            if (_alpha > 0)
            {
                _alpha -= _screenSpeed * Time.deltaTime;
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
            blackScreen.SetActive(true);
            if (_alpha < 1)
            {
                _alpha += _screenSpeed * Time.deltaTime;
            }
            else
            {
                isOpenBlackScreen = false;
                Invoke("WaitCloseBlackSreen", 1f);
            }
        }
    }
    void WaitCloseBlackSreen()
    {
        isCloseBlackScreen = true;
    }
}
