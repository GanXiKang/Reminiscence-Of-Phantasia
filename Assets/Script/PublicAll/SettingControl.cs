using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    [Header("Musia")]
    public AudioClip onClick;
    public AudioClip flipBook;
    public AudioClip open;
    public AudioClip save;
    public AudioClip leave;
    AudioSource BGM;

    [Header("UI")]
    public Image bookUI;
    public Slider sliderBGM;
    public Toggle fullScreen;
    public GameObject[] settingUI;
    public Button[] labelColor;
    public Sprite[] turnPage;

    public static float volumeBGM = 0.7f;
    public static bool isFullS;
    public static bool isSettingActive = false;
    public static int _page = 1;
    bool isUIInteractable;

    void Start()
    {
        BGM = GetComponent<AudioSource>();

        sliderBGM.value = volumeBGM;
        BGM.volume = volumeBGM;
        fullScreen.isOn = Screen.fullScreen;
        isUIInteractable = true;
    }

    void Update()
    {
        settingUI[0].SetActive(isSettingActive);

        if (isCanUseSetting())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BGM.PlayOneShot(open);
                isSettingActive = !isSettingActive;
            }
        }
             
    }

    bool isCanUseSetting()
    {
        return !CameraControl_House.isLookWorkbench && 
               !UIControl_House.isContentActive && 
               !BagControl_House.isBagActive && 
               !TaskController.isTaskActive &&
               !StoryBagControl.isBagActive &&
               !StoryUIControl_LittleGirl.isContentActive &&
               !StoryGameControl_LittleGirl.isSeeFantasy &&
               !StoryGameControl_LittleGirl.isUseMatchesUI;
    }

    void UIInteractable()
    {
        if (isUIInteractable)
        {
            for (int i = 1; i < settingUI.Length; i++)
            {
                if (i == _page)
                {
                    settingUI[i].SetActive(true);
                }
                else
                {
                    settingUI[i].SetActive(false);
                }
            }
        }
        else 
        {
            for (int i = 1; i < settingUI.Length; i++)
            {
                 settingUI[i].SetActive(false);
            }
        }    
    }
    void LabelInteractable()
    {
        for (int i = 1; i < settingUI.Length; i++)
        {
            if (i == _page)
            {
                labelColor[i].interactable = false;
            }
            else
            {
                labelColor[i].interactable = true;
            }
        }
    }

    //Button
    public void LabelGreen_Button()             //ÔO¶¨
    {
        _page = 1;
        BGM.PlayOneShot(onClick);
        StartCoroutine(TurnPageRight()); 
    }
    public void LabelBlue_Button()              //²Ù×÷
    {
        _page = 2;
        BGM.PlayOneShot(onClick);
        StartCoroutine(TurnPageLeft());
    }
    public void Close_Button()               //êPé]
    {
        BGM.PlayOneShot(onClick);
        isSettingActive = !isSettingActive;
    }
    public void Save_Button() 
    {
        BGM.PlayOneShot(save);
    }
    public void ExitGame_Button() 
    {
        BGM.PlayOneShot(leave);
        Application.Quit();
    }
    public void Volume_BGM()
    {
        volumeBGM = sliderBGM.value;
        BGM.volume = volumeBGM;
    }
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        isFullS = isFullScreen;
    }

    //·­•ø„Ó®‹
    IEnumerator TurnPageLeft()
    {
        isUIInteractable = false;
        UIInteractable();
        bookUI.sprite = turnPage[1];
        yield return new WaitForSeconds(0.2f);
        BGM.PlayOneShot(flipBook);
        bookUI.sprite = turnPage[2];
        yield return new WaitForSeconds(0.2f);
        bookUI.sprite = turnPage[4];
        yield return new WaitForSeconds(0.2f);
        LabelInteractable();
        bookUI.sprite = turnPage[3];
        yield return new WaitForSeconds(0.2f);
        bookUI.sprite = turnPage[0];
        isUIInteractable = true;
        UIInteractable();
    }
    IEnumerator TurnPageRight()
    {
        isUIInteractable = false;
        UIInteractable();
        bookUI.sprite = turnPage[3];
        yield return new WaitForSeconds(0.2f);
        BGM.PlayOneShot(flipBook);
        LabelInteractable();
        bookUI.sprite = turnPage[4];
        yield return new WaitForSeconds(0.2f);
        bookUI.sprite = turnPage[2];
        yield return new WaitForSeconds(0.2f);
        bookUI.sprite = turnPage[1];
        yield return new WaitForSeconds(0.2f);
        bookUI.sprite = turnPage[0];
        isUIInteractable = true;
        UIInteractable();
    }
}
