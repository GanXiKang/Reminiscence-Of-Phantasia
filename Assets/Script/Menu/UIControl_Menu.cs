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
    public Slider sliderBGM;
    public Toggle fullScreen;
    public static float volumeBGM = 0.7f;
    public static bool isFullS;
    bool isstaff = false;

    void Start()
    {
        sliderBGM.value = volumeBGM;
        BGM.volume = volumeBGM;
        fullScreen.isOn = Screen.fullScreen;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            FullScreen(!isFullS);
        }
    }

    public void Button_Start()
    {
        BGM.PlayOneShot(onClick);
        TransitionUIControl.isTransitionUIAnim_In = true;
        Invoke("GoToMenu", 1f);
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
        if (!isstaff)
        {
            menuUI[1].SetActive(true);
            menuUI[2].SetActive(false);
        }
        else
        {
            isstaff = false;
            menuUI[2].SetActive(true);
            menuUI[3].SetActive(false);
        }
    }
    public void Button_Staff()
    {
        isstaff = true;
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
