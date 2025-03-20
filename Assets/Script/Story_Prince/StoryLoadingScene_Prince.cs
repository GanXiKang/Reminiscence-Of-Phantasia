using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingScene_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip nowBGM, pastBGM, futureBGM;
    public AudioClip switchScene;
    bool isPlayMusiaOnce = true;

    [Header("Scene")]
    public GameObject now;
    public GameObject past;
    public GameObject future;
    public static bool isNowScene = true;
    public static bool isPastScene = false;
    public static bool isFutureScene = false;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a;
    public static bool isLoading = false;
    public static bool isOpen = false;
    bool isClose = false;
    float _loadingSpeed = 1.5f;

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

            if (a.fillAmount < 1)
            {
                a.fillAmount += _loadingSpeed * Time.deltaTime;
            }
            else if (a.fillAmount == 1)
            {
                isOpen = false;
                StorySkillControl_Prince.isClockActice = false;
                Invoke("WaitCloseLoading", 1f);
            }
        }
        if (isClose)
        {
            if (a.fillAmount > 0)
            {
                a.fillAmount -= _loadingSpeed * Time.deltaTime;
            }
            else if (a.fillAmount == 0)
            {
                isPlayMusiaOnce = true;
                isClose = false;
                isLoading = false;
                StoryGhostControl_Prince.isDisappear = true;
                if (StorySkillControl_Prince._goPast == 1)
                {
                    StoryUIControl_Prince.isDialogue = true;
                    StoryDialogueControl_Prince._textCount = 8;
                }
            }
        }
    }
    void WaitCloseLoading()
    {
        ChangeScene();
        isClose = true;
        if (StorySkillControl_Prince.isFirstBackNow)
        {
            StoryGameControl_Prince.isPlotNpcActive = true;
            StorySkillControl_Prince.isFirstBackNow = false;
        }
    }
    void ChangeScene() 
    {
        now.SetActive(isNowScene);
        past.SetActive(isPastScene);
        future.SetActive(isFutureScene);

        if (isNowScene)
        {
            BGM.Stop();
            BGM.clip = nowBGM;
            BGM.Play();
        }
        else if (isPastScene)
        {
            BGM.Stop();
            BGM.clip = pastBGM;
            BGM.Play();
        }
        else if (isFutureScene)
        {
            BGM.Stop();
            BGM.clip = futureBGM;
            BGM.Play();
        }
    }
}
