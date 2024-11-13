using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingControl : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick, flipBook, open, save, leave;
 
    [Header("UI")]
    public Image bookUI;
    public Slider sliderBGM;
    public Toggle fullScreen;
    public GameObject[] settingUI;
    public Button[] labelColor;
    public Sprite[] turnPage;

    public static float volumeBGM = 0.2f;
    public static bool isFullS;
    public static bool isSettingActive = false;
    public static int _page = 1;
    public float _openSpeed = 1f;
    bool isUIInteractable;

    void Start()
    {
        sliderBGM.value = volumeBGM;
        BGM.volume = volumeBGM;
        fullScreen.isOn = Screen.fullScreen;
        isUIInteractable = true;
    }

    void Update()
    {
        OpenUI();

        if (isCanUseSetting())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BGM.PlayOneShot(open);
                isSettingActive = !isSettingActive;
            }
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            FullScreen(!isFullS);
        }
    }

    bool isCanUseSetting()
    {
        return true;
    }

    void OpenUI()
    {
        settingUI[0].SetActive(isSettingActive);

        if (isSettingActive)
        {
            if (settingUI[0].GetComponent<RectTransform>().localScale.x < 1)
            {
                settingUI[0].GetComponent<RectTransform>().localScale += new Vector3(_openSpeed, _openSpeed, 0f) * Time.deltaTime;
            }
        }
        else 
        {
            settingUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
        }
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

    public void LabelGreen_Button()
    {
        _page = 1;
        BGM.PlayOneShot(onClick);
        StartCoroutine(TurnPageRight());
    }
    public void LabelBlue_Button()
    {
        _page = 2;
        BGM.PlayOneShot(onClick);
        StartCoroutine(TurnPageLeft());
    }
    public void Close_Button()
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
