using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl_House : MonoBehaviour
{
    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a, b, c;
    float value = 1;
    public float _loadingSpeed = 1.5f;
    public static bool isCloseLoadingUI = false;
    public static bool isOpenLoadingUI = false;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Start()
    {
        isCloseLoadingUI = true;
    }

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);

        LoadingUI();
        CloseLoadingUI();
        OpenLoadingUI();
    }

    void LoadingUI()
    {
        loadingUI.SetActive(isCloseLoadingUI || isOpenLoadingUI);
        a.fillAmount = value;
        b.fillAmount = value;
        c.fillAmount = value;
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
