using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingScene_Momotaro : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip riverSideBGM, forestBGM, mountainBGM, plazaBGM;
    public AudioClip switchScene;
    bool isPlayOnce = true;

    [Header("Scene")]
    public GameObject riverSide;
    public GameObject forest;
    public GameObject mountain;
    public GameObject plaza;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a, b, c;
    public static bool isLoading = false;
    public static bool isLeftOpen = false;     //ªÿ»•
    public static bool isLeftClose = false;
    public static bool isRightOpen = false;    //«∞ﬂM
    public static bool isRightClose = false;
    float _loadingSpeed = 1.5f;
    bool isOnce = true;

    void Update()
    {
        loadingUI.SetActive(isLoading);

        SwitchSceneMusia();
        LeftRightSwitch();
    }

    void SwitchSceneMusia()
    {
        if (!isLeftOpen && !isRightOpen) return;
        if (!isPlayOnce) return;

        BGM.PlayOneShot(switchScene);
        isPlayOnce = false;
    }
    void LeftRightSwitch()
    {
        if (isLeftOpen)
        {
            StartCoroutine(LeftSwitchScene_Open());
        }
        if (isLeftClose)
        {
            StartCoroutine(LeftSwitchScene_Close());
        }
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
            ChangeScene();
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
            isPlayOnce = true;
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
            ChangeScene();
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
            isPlayOnce = true;
            isRightClose = false;
            isLoading = false;
        }
    }

    void ChangeScene()
    {
        riverSide.SetActive(false);
        forest.SetActive(false);
        mountain.SetActive(false);
        plaza.SetActive(false);
        switch (StoryExitControl_Momotaro._changeSceneNum)
        {
            case 1:
                riverSide.SetActive(true);
                BGM.Stop();
                BGM.clip = riverSideBGM;
                BGM.Play();
                break;

            case 2:
                forest.SetActive(true);
                BGM.Stop();
                BGM.clip = forestBGM;
                BGM.Play();
                break;

            case 3:
                mountain.SetActive(true);
                BGM.Stop();
                BGM.clip = mountainBGM;
                BGM.Play();
                break;

            case 4:
                plaza.SetActive(true);
                BGM.Stop();
                BGM.clip = plazaBGM;
                BGM.Play();
                break;
        }
    }
}
