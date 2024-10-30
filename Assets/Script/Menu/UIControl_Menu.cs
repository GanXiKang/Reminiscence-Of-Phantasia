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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Button_Start()
    {
        
    }
    public void Button_Continue()
    {

    }
    public void Button_Setting()
    {

    }
    public void Button_Quit()
    {
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
}
