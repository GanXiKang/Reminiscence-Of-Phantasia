using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingScene_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip nowBGM, pastBGM, futureBadBGM, futureGoodBGM;
    public AudioClip switchScene, loading;
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
    public Image ima;
    public Sprite[] time;
    public static bool isLoading = false;
    public static bool isOpen = false;
    bool isClose = false;
    float _loadingSpeed = 1.5f;

    private Coroutine current;

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
                isPlayMusiaOnce = false;
                BGM.PlayOneShot(switchScene);
                current = StartCoroutine(TimeLoading());
            }

            if (ima.fillAmount < 1)
            {
                ima.fillAmount += _loadingSpeed * Time.deltaTime;
            }
            else if (ima.fillAmount == 1)
            {
                isOpen = false;
                BGM.PlayOneShot(loading);
                StorySkillControl_Prince.isClockActice = false;
                Invoke("WaitCloseLoading", 1f);
            }
        }
        if (isClose)
        {
            if (ima.fillAmount > 0)
            {
                ima.fillAmount -= _loadingSpeed * Time.deltaTime;
            }
            else if (ima.fillAmount == 0)
            {
                isPlayMusiaOnce = true;
                isClose = false;
                isLoading = false;
                StopCoroutine(current);
                if (StoryGhostControl_Prince.isWatchSkill)
                    StoryGhostControl_Prince.isDisappear = true;

                if (StorySkillControl_Prince._goPast == 1)
                {
                    StorySkillControl_Prince._goPast++;
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
            if (!StoryGameControl_Prince.isFutureGood)
            {
                BGM.Stop();
                BGM.clip = futureBadBGM;
                BGM.Play();
            }
            else
            {
                BGM.Stop();
                BGM.clip = futureGoodBGM;
                BGM.Play();
            }
        }
    }

    IEnumerator TimeLoading()
    {
        int index = 0;

        while (true)
        {
            index++;
            if (index >= 3)
                index = 0;
            ima.sprite = time[index];
            yield return new WaitForSeconds(0.6f);
        }
    }
}
