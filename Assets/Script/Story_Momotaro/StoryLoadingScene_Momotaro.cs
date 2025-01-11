using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingScene_Momotaro : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip riverSideBGM, forestBGM, mountainBGM, plazaBGM;
    public AudioClip switchScene, hintGoPlaza;
    bool isPlayMusiaOnce = true;

    [Header("Scene")]
    public GameObject riverSide;
    public GameObject forest;
    public GameObject mountain;
    public GameObject plaza;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a, b;
    public Sprite riverSideA, riverSideB;
    public Sprite forestA, forestB;
    public Sprite mountainA, mountainB;
    public Sprite plazaA, plazaB;
    public static bool isLoading = false;
    public static bool isOpen = false; 
    bool isClose = false;
    float _loadingSpeed = 1.5f;

    //Plot
    bool isFirstGoForest = true;
    bool isSpecialEndingOnce = true;
    public static bool isFirstGoPlaza = false;
    public static bool isPlotAnimator = false;
    public static bool isHintGoPlaza = false;

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
            ChangeLoadingSprite();
            if (isPlayMusiaOnce)
            {
                isPlayMusiaOnce = false;
                BGM.PlayOneShot(switchScene);
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
                    isPlotAnimator = true;
                    StoryPlayerControl._direction = 0;
                    StoryPlayerAnimator_Momotaro.isFall = true;
                    StoryBagControl.isOpenBag = false;
                    StoryBagControl.isItemNumber[1] = false;
                    StoryBagControl._howManyGrids--;
                    StoryBagControl.isRenewBag = true;
                    Invoke("PlayerFall", 0.75f);
                }
                if (isFirstGoPlaza)
                {
                    isFirstGoPlaza = false;
                    StoryUIControl_Momotaro.isDialogue = true;
                    StoryDialogueControl_Momotaro._textCount = 4;
                }
                if (isHintGoPlaza)
                {
                    isHintGoPlaza = false;
                    BGM.PlayOneShot(hintGoPlaza);
                    StoryUIControl_Momotaro.isDialogue = true;
                    StoryDialogueControl_Momotaro._textCount = 76;
                }
                if (StoryInteractableControl_Momotaro.isSpecialEnding && isSpecialEndingOnce)
                {
                    isSpecialEndingOnce = false;
                    StoryNpcAnimator_Momotaro.isWalk_Momo = false;
                    StoryUIControl_Momotaro.isDialogue = true;
                    StoryDialogueControl_Momotaro._isAboveWho1 = 1;
                    StoryDialogueControl_Momotaro._textCount = 13;
                }
                if (StoryInteractableControl_Momotaro.isSpecialEnding && StoryInteractableControl_Momotaro.isMeetPartner)
                {
                    StoryUIControl_Momotaro.isDialogue = true;
                    StoryDialogueControl_Momotaro._isAboveWho1 = 1;
                    StoryDialogueControl_Momotaro._textCount = 14;
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
    void ChangeLoadingSprite()
    {
        switch (StoryExitControl_Momotaro._changeSceneNum)
        {
            case 1:
                a.sprite = riverSideA;
                b.sprite = riverSideB;
                break;

            case 2:
                a.sprite = forestA;
                b.sprite = forestB;
                break;

            case 3:
                a.sprite = mountainA;
                b.sprite = mountainB;
                break;

            case 4:
                a.sprite = plazaA;
                b.sprite = plazaB;
                break;
        }
    }

    void PlayerFall()
    {
        StoryUIControl_Momotaro.isDialogue = true;
        StoryDialogueControl_Momotaro._textCount = 2;
    }
}
