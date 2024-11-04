using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl_Menu : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick;

    [Header("UI")]
    public GameObject[] menuUI;
    public Sprite[] bgSprite; 
    public Slider sliderBGM;
    public Toggle fullScreen;
    public static float volumeBGM = 0.7f;
    public static bool isFullS;
    bool isStaff = false;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Start()
    {
        sliderBGM.value = volumeBGM;
        BGM.volume = volumeBGM;
        fullScreen.isOn = Screen.fullScreen;
    }

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);

        if (Input.GetKeyDown(KeyCode.F11))
        {
            FullScreen(!isFullS);
        }
    }

    public void Button_Start()
    {
        BGM.PlayOneShot(onClick);
        TransitionUIControl.isTransitionUIAnim_In = true;
        Invoke("GoToHouse", 1f);
    }
    public void Button_Continue()
    {
        BGM.PlayOneShot(onClick);
    }
    public void Button_Setting()
    {
        BGM.PlayOneShot(onClick);
        menuUI[1].SetActive(false);
        menuUI[2].SetActive(true);
    }
    public void Button_Back()
    {
        if (!isStaff)
        {
            menuUI[1].SetActive(true);
            menuUI[2].SetActive(false);
        }
        else
        {
            isStaff = false;
            menuUI[2].SetActive(true);
            menuUI[3].SetActive(false);
        }
    }
    public void Button_Staff()
    {
        isStaff = true;
        menuUI[2].SetActive(false);
        menuUI[3].SetActive(true);
    }
    public void Button_Quit()
    {
        BGM.PlayOneShot(onClick);
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

    void GoToHouse()
    {
        SceneManager.LoadScene(1);
    }
}
