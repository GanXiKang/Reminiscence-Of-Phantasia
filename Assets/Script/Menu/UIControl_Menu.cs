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
    public Image background;
    public Button continueButton;
    public Slider sliderBGM;
    public Toggle fullScreen;
    bool isStaff = false;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    void Start()
    {
        if (SaveManagerControl.Instance.SaveFileExists())
            continueButton.interactable = true;
        else
            continueButton.interactable = false;

        sliderBGM.value = SettingControl.volumeBGM;
        BGM.volume = SettingControl.volumeBGM;
        fullScreen.isOn = Screen.fullScreen;
    }

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
        MouseCursor();

        if (Input.GetKeyDown(KeyCode.F11))
            FullScreen(!SettingControl.isFullS);
    }

    public void Button_Start()
    {
        BGM.PlayOneShot(onClick);
        TransitionUIControl.isTransitionUIAnim_In = true;
        GameControl_House._day = 1;
        GameControl_House._MyCoin = 300;
        GameControl_House._storyNum = 0;
        Invoke("GoToStartMovie", 1f);
    }
    public void Button_Continue()
    {
        BGM.PlayOneShot(onClick);
        TransitionUIControl.isTransitionUIAnim_In = true;

        GameData gameData = SaveManagerControl.Instance.LoadGame();
        GameControl_House._day = gameData.gameDay;
        GameControl_House._MyCoin = gameData.playerCoins;
        GameControl_House._storyNum = gameData.gameStoryNum;
        Invoke("GoToLoadGameScene", 1f);
    }
    public void Button_Setting()
    {
        BGM.PlayOneShot(onClick);
        menuUI[0].SetActive(false);
        menuUI[1].SetActive(true);
        BackgroundSprite();
    }
    public void Button_Back()
    {
        BGM.PlayOneShot(onClick);
        if (!isStaff)
        {
            menuUI[0].SetActive(true);
            menuUI[1].SetActive(false);
        }
        else
        {
            isStaff = false;
            menuUI[1].SetActive(true);
            menuUI[2].SetActive(false);
        }
        BackgroundSprite();
    }
    public void Button_Staff()
    {
        BGM.PlayOneShot(onClick);
        isStaff = true;
        menuUI[1].SetActive(false);
        menuUI[2].SetActive(true);
        BackgroundSprite();
    }
    public void Button_Quit()
    {
        BGM.PlayOneShot(onClick);
        Application.Quit();
    }
    public void Volume_BGM()
    {
        SettingControl.volumeBGM = sliderBGM.value;
        BGM.volume = SettingControl.volumeBGM;
    }
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        SettingControl.isFullS = isFullScreen;
    }

    void MouseCursor()
    {
        if (isClick)
            Cursor.SetCursor(mouse2, hotSpot, CursorMode.Auto);
        else
            Cursor.SetCursor(mouse1, hotSpot, CursorMode.Auto);

        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            Invoke("FalseisClick", 0.5f);
        }
    }
    void FalseisClick()
    {
        isClick = false;
    }

    void BackgroundSprite()
    {
        for (int i = 0; i < menuUI.Length; i++)
        {
            if (menuUI[i].activeSelf)
            {
                background.sprite = bgSprite[i];
            }
        }
    }
    void GoToStartMovie()
    {
        SceneManager.LoadScene(5);
    }
    void GoToLoadGameScene()
    {
        SceneManager.LoadScene(SaveManagerControl.Instance.LoadGame().currentSceneName);
    }
}
