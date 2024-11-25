using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingControl : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick, open, save, leave;

    [Header("UI")]
    public GameObject settingsUI;
    public GameObject setting;
    public Image background;
    public Sprite settingPage, operatePage;
    public Slider sliderBGM;
    public Toggle fullScreen;
    
    public static float volumeBGM = 0.2f;
    public static bool isFullS;
    public static bool isSettingActive = false;
    bool isOperate = false;

    void Start()
    {
        sliderBGM.value = volumeBGM;
        BGM.volume = volumeBGM;
        fullScreen.isOn = Screen.fullScreen;
    }

    void Update()
    {
        SettingUI();
        UIBackground();
    }

    void SettingUI()
    {
        settingsUI.SetActive(isSettingActive);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BGM.PlayOneShot(open);
            isSettingActive = !isSettingActive;
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            FullScreen(!isFullS);
        }
    }
    void UIBackground()
    {
        if (!isOperate)
        {
            background.sprite = settingPage;
        }
        else
        {
            background.sprite = operatePage;
        }
    }

    public void Close_Button()
    {
        BGM.PlayOneShot(onClick);
        if (!isOperate)
        {
            isSettingActive = !isSettingActive;
        }
        else
        {
            isOperate = false;
        }
    }
    public void Operate_Button()
    {
        BGM.PlayOneShot(onClick);
        isOperate = true;
    }
    public void Save_Button()
    {
        BGM.PlayOneShot(save);
    }
    public void ExitGame_Button()
    {
        BGM.PlayOneShot(leave);
        TransitionUIControl.isTransitionUIAnim_In = true;
        Invoke("GoToMenu", 1f);
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

    void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
