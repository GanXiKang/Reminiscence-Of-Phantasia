using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTeachControl : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip page, coroutine;

    [Header("TextFile")]
    public int isTeachStory;
    public TextAsset[] teachContent;
    public Sprite[] teachImage;
    int _totalPage = 0;

    int _teachTemperaturePage = 4;
    int _teachAllPage_Story2 = 2;
    int _teachAllPage_Story3 = 3;

    [Header("UI")]
    public GameObject teachUI;
    public GameObject window;
    public Image background;
    public Text content;
    public Button[] teachButton;
    public float _openSpeed = 1f;
    public static bool isTeachActive;
    int _page = 1;
    int _isFinish = 0;

    void Start()
    {
        isTeachActive = false;
    }

    void Update()
    {
        OpenUI();
        Teach();
        ButtonActive();
    }

    void OpenUI()
    {
        teachUI.SetActive(isTeachActive);

        if (isTeachActive)
        {
            if (window.GetComponent<RectTransform>().localScale.x < 1)
            {
                window.GetComponent<RectTransform>().localScale += new Vector3(_openSpeed, _openSpeed, 0f) * Time.deltaTime;
            }
        }
        else
        {
            window.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
        }
    }
    void Teach()
    {
        switch (isTeachStory)
        {
            case 1:
                for (int i = 1; i <= _teachTemperaturePage; i++)
                {
                    if (i == _page)
                    {
                        background.sprite = teachImage[i];
                        content.text = teachContent[i].ToString();
                    }
                }
                break;

            case 2:
                for (int i = 1; i <= _teachAllPage_Story2; i++)
                {
                    if (i == _page)
                    {
                        background.sprite = teachImage[i + _totalPage];
                        content.text = teachContent[i + _totalPage].ToString();
                    }
                }
                break;

            case 3:
                for (int i = 1; i <= _teachAllPage_Story3; i++)
                {
                    if (i == _page)
                    {
                        background.sprite = teachImage[i + _totalPage];
                        content.text = teachContent[i + _totalPage].ToString();
                    }
                }
                break;
        }
    }
    void ButtonActive()
    {
        switch (isTeachStory)
        {
            case 1:
                switch (_page)
                {
                    case 1:
                        teachButton[1].interactable = false;
                        break;

                    case 2:
                        teachButton[1].interactable = true;
                        break;

                    case 3:
                        teachButton[2].interactable = true;
                        break;

                    case 4:
                        teachButton[0].interactable = true;
                        teachButton[2].interactable = false;
                        break;
                }
                break;

            case 2:
                switch (_page)
                {
                    case 1:
                        teachButton[1].interactable = false;
                        teachButton[2].interactable = true;
                        break;

                    case 2:
                        teachButton[0].interactable = true;
                        teachButton[1].interactable = true;
                        teachButton[2].interactable = false;
                        break;
                }
                break;

            case 3:
                switch (_page)
                {
                    case 1:
                        teachButton[1].interactable = false;
                        teachButton[2].interactable = true;
                        break;

                    case 2:
                        teachButton[1].interactable = true;
                        teachButton[2].interactable = true;
                        break;

                    case 3:
                        teachButton[0].interactable = true;
                        teachButton[1].interactable = true;
                        teachButton[2].interactable = false;
                        break;
                }
                break;
        }
    }

    public void Button_ChangePage(int _change)
    {
        BGM.PlayOneShot(page);
        _page += _change;
    }
    public void Button_Close()
    {
        BGM.PlayOneShot(coroutine);
        isTeachActive = false;
        teachButton[0].interactable = false;
        _page = 1;
        CloseTeach();
    }

    void CloseTeach()
    {
        switch (isTeachStory)
        {
            case 1:
                StoryThermometerControl_Girl.isThermometer = true;
                break;

            case 2:
                _isFinish++;
                if (_isFinish != 4)
                {
                    _totalPage += _teachAllPage_Story2;
                }
                break;

            case 3:
                _isFinish++;
                if (_isFinish != 2)
                {
                    _totalPage += _teachAllPage_Story3;
                }
                break;
        }
    }
}
