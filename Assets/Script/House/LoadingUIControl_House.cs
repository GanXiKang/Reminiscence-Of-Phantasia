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

    void Start()
    {
        isCloseLoadingUI = true;
    }

    void Update()
    {
        loadingUI.SetActive(isCloseLoadingUI || isOpenLoadingUI);
        a.fillAmount = value;
        b.fillAmount = value;
        c.fillAmount = value;

        CloseLoadingUI();
        OpenLoadingUI();
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
}
