using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingUI_Gril : MonoBehaviour
{
    [Header("Scene")]
    public GameObject street;
    public GameObject forest;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a,b,c;
    public static bool isLoading = false;
    public static bool isLeft = false;
    public static bool isRight = false;
    float _loadingSpeed = 1.5f;

    void Update()
    {
        loadingUI.SetActive(isLoading);

        LeftSwitch();
        RightSwitch();

        if (Input.GetKeyDown(KeyCode.L))  //úy‘á
        {
            isLoading = true;
            isRight = true;
        }
    }

    void LeftSwitch()
    {
        if (isLeft)
        {
            StartCoroutine(LeftSwitchScene_Open());
        }
    }
    void RightSwitch()
    {
        if (isRight)
        {
            StartCoroutine(RightSwitchScene_Open());
        }
    }
    void BarValue(Image bar, bool isAdd)
    {
        if (isAdd)
        {
            if (bar.fillAmount < 1)
            {
                bar.fillAmount += _loadingSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (bar.fillAmount > 0)
            {
                bar.fillAmount -= _loadingSpeed * Time.deltaTime;
            }
        }
    }

    IEnumerator LeftSwitchScene_Open()
    {
        a.fillOrigin = 0;
        b.fillOrigin = 0;
        c.fillOrigin = 0;

        BarValue(a, true);
        yield return new WaitForSeconds(0.2f);
        BarValue(b, true);
        yield return new WaitForSeconds(0.2f);
        BarValue(c, true);

        if (c.fillAmount == 1)
        {
            isLeft = false;
        }
    }
    IEnumerator LeftSwitchScene_Close()
    {
        a.fillOrigin = 1;
        b.fillOrigin = 1;
        c.fillOrigin = 1;

        BarValue(a, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(b, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(c, false);

        if (c.fillAmount == 0)
        {
            isLeft = false;
        }
    }
    IEnumerator RightSwitchScene_Open()
    {
        a.fillOrigin = 1;
        b.fillOrigin = 1;
        c.fillOrigin = 1;

        BarValue(a, true);
        yield return new WaitForSeconds(0.2f);
        BarValue(b, true);
        yield return new WaitForSeconds(0.2f);
        BarValue(c, true);

        if (c.fillAmount == 1)
        {
            isRight = false;
        }
    }
    IEnumerator RightSwitchScene_Close()
    {
        a.fillOrigin = 1;
        b.fillOrigin = 1;
        c.fillOrigin = 1;

        BarValue(a, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(b, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(c, false);

        if (c.fillAmount == 0)
        {
            isRight = false;
        }
    }
