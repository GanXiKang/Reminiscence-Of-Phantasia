using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingControl : MonoBehaviour
{
    GameObject player;

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
    public static float volumeBGM = 0.5f;
    public static bool isFullS;
    public static bool isSettingActive = false;
    bool isOperate = false;

    //Move
    Vector3 pointA = new Vector3(-888, 0, 0); 
    Vector3 pointB = new Vector3(0, 0, 0);
    float _timer = 0f;
    float _duration = 0.8f;
    bool isMoving = false;                  
    bool isAppear = false;                 


    void Start()
    {
        player = GameObject.Find("Player");
        sliderBGM.value = volumeBGM;
        BGM.volume = volumeBGM;
        fullScreen.isOn = Screen.fullScreen;
    }

    void Update()
    {
        SettingUI();
        MoveUI();
    }

    void SettingUI()
    {
        settingsUI.SetActive(isSettingActive);
        setting.SetActive(!isOperate);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close_Button();
        }

        if (!isOperate)
        {
            background.sprite = settingPage;
        }
        else
        {
            background.sprite = operatePage;
        }
    }
    void MoveUI()
    {
        if (!isMoving) return; 

        _timer += Time.deltaTime;
        float t = _timer / _duration;

        if (isAppear)
            settingsUI.GetComponent<RectTransform>().localPosition = Vector3.Lerp(pointA, pointB, t); 
        else
            settingsUI.GetComponent<RectTransform>().localPosition = Vector3.Lerp(pointB, pointA, t);

        if (t >= 1f)
        {
            _timer = 0f; 
            isMoving = false;
            if (!isAppear)
            {
                isSettingActive = false;
            }
        }
    }

    public void Close_Button()
    {
        if (!isOperate)
        {
            BGM.PlayOneShot(open);
            if (!isSettingActive)
            {
                isSettingActive = true;
                isAppear = true;
                isMoving = true;
            }
            else
            {
                isAppear = false;
                isMoving = true;
            }
        }
        else
        {
            BGM.PlayOneShot(onClick);
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

        //GameData gameData = new GameData
        //{
        //    gameDay = GameControl_House._day,
        //    gameStoryNum = GameControl_House._storyNum,
        //    playerCoins = StoreControl_House._MyCoin,
        //    currentSceneName = SceneManager.GetActiveScene().name
        //};

        //SaveManagerControl.Instance.SaveGame(gameData);
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
        //isSettingActive = false;
        SceneManager.LoadScene(0);
    }
}
