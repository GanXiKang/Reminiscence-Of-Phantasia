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
    public Image a, b, c;
    public static bool isLoading = false;
    public static bool isLeftOpen = false;
    public static bool isLeftClose = false;
    public static bool isRightOpen = false;
    public static bool isRightClose = false;
    float _loadingSpeed = 1.5f;

    void Update()
    {
        loadingUI.SetActive(isLoading);

        LeftSwitch();
        RightSwitch();

        if (Input.GetKeyDown(KeyCode.L))  //úy‘á
        {
            isLoading = true;
            isRightOpen = true;
        }
    }

    void LeftSwitch()
    {
        if (isLeftOpen)
        {
            StartCoroutine(LeftSwitchScene_Open());
        }
        if (isLeftClose)
        {
            StartCoroutine(LeftSwitchScene_Close());
        }
    }
    void RightSwitch()
    {
        if (isRightOpen)
        {
            StartCoroutine(RightSwitchScene_Open());
        }
        if (isRightClose)
        {
            StartCoroutine(RightSwitchScene_Close());
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
            isLeftOpen = false;
            yield return new WaitForSeconds(0.5f);
            isRightOpen = true;
        }
    }
    IEnumerator LeftSwitchScene_Close()
    {
        a.fillOrigin = 0;
        b.fillOrigin = 0;
        c.fillOrigin = 0;

        BarValue(a, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(b, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(c, false);

        if (c.fillAmount == 0)
        {
            isLeftClose = false;
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
            isRightOpen = false;
            yield return new WaitForSeconds(0.5f);
            isLeftClose = true;
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
            isRightClose = false;
        }
    }
}
