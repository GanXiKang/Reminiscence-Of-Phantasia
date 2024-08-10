using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingScene_Girl : MonoBehaviour
{
    [Header("Scene")]
    public GameObject street;
    public GameObject forest;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a, b, c;
    public static bool isLoading = false;
    public static bool isLeftOpen = false;     //��ȥ
    public static bool isLeftClose = false;
    public static bool isRightOpen = false;    //ǰ�M
    public static bool isRightClose = false;
    float _loadingSpeed = 1.5f;

    void Update()
    {
        loadingUI.SetActive(isLoading);

        LeftSwitch();
        RightSwitch();

        if (Input.GetKeyDown(KeyCode.L))  //�yԇ
        {
            isRightOpen = true;
        }
        if (Input.GetKeyDown(KeyCode.K))  //�yԇ
        {
            isLeftOpen = true;
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
        isLoading = true;
        a.fillOrigin = 0; //(int)Image.OriginHorizontal.Left
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
            street.SetActive(true);
            forest.SetActive(false);
            isRightClose = true;
        }
    }
    IEnumerator LeftSwitchScene_Close()
    {
        a.fillOrigin = 0;
        b.fillOrigin = 0;
        c.fillOrigin = 0;

        BarValue(c, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(b, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(a, false);

        if (a.fillAmount == 0)
        {
            isLeftClose = false;
            isLoading = false;
        }
    }
    IEnumerator RightSwitchScene_Open()
    {
        isLoading = true;
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
            forest.SetActive(true);
            street.SetActive(false);
            isLeftClose = true;
        }
    }
    IEnumerator RightSwitchScene_Close()
    {
        a.fillOrigin = 1;
        b.fillOrigin = 1;
        c.fillOrigin = 1;

        BarValue(c, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(b, false);
        yield return new WaitForSeconds(0.2f);
        BarValue(a, false);

        if (a.fillAmount == 0)
        {
            isRightClose = false;
            isLoading = false;
        }
    }
}
}
