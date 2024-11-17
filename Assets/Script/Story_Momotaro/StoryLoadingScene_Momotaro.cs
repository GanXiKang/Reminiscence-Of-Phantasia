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
    bool isPlayMusiaOnce = true;

    [Header("Scene")]
    public GameObject riverSide;
    public GameObject forest;
    public GameObject mountain;
    public GameObject plaza;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a, b;
    public static bool isLoading = false;
    public static bool isOpen = false; 
    bool isClose = false;
    float _loadingSpeed = 1.5f;

    //value
    bool isFirstGoForest = true;

    void Update()
    {
        loadingUI.SetActive(isLoading);

        SwitchScene();
    }

    void SwitchScene()
    {
        if (isOpen)
        {
            isLoading = true;
            if (isPlayMusiaOnce)
            {
                BGM.PlayOneShot(switchScene);
                isPlayMusiaOnce = false;
            }

            BarValue(a, true);
            BarValue(b, true);

            if (a.fillAmount == 1)
            {
                isOpen = false;
                Invoke("WaitCloseLoading", 0.5f);
            }
        }
        if (isClose)
        {
            BarValue(a, false);
            BarValue(b, false);

            if (a.fillAmount == 0)
            {
                isPlayMusiaOnce = true;
                isClose = false;
                isLoading = false;
                if (isFirstGoForest)
                {
                    isFirstGoForest = false;
                    StoryPlayerAnimator_Momotaro.isFall = true;
                    Invoke("PlayerFall", 0.5f);
                }
            }
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
    void WaitCloseLoading()
    {
        ChangeScene();
        isClose = true;
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

    void PlayerFall()
    {
        StoryUIControl_Momotaro.isDialogue = true;
        StoryDialogueControl_Momotaro._textCount = 2;
    }
}
