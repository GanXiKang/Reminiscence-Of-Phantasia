using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTeachControl : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip page, coroutine;

    [Header("Teach_Story1")]
    public bool isTemperature;
    int _teachTemperaturePage = 4;

    [Header("Teach_Story2")]
    public bool isGoddess;
    int _teachGoddessPage = 2;
    public bool isChange;
    int _teachChangePage = 2;
    public bool isWind;
    int _teachWindPage = 2;
    public bool isPerformance;
    int _teachPerformancePage = 2;

    [Header("Teach_Story3")]
    public bool isTime;
    int _teachTimePage = 3;
    public bool isSupplies;
    int _teachSuppliesPage = 4;

    [Header("TextFile")]
    public TextAsset[] teachContent;
    public Sprite[] teachImage;

    [Header("UI")]
    public GameObject teachUI;
    public GameObject window;
    public Image background;
    public Text content;
    public Button[] teachButton;
    public float _openSpeed = 1f;
    public static bool isTeachActive = false;
    int _page = 1;
    int _isFinish = 0;

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
        if (isTemperature)
        {
            for (int i = 1; i <= _teachTemperaturePage; i++)
            {
                if (i == _page)
                {
                    background.sprite = teachImage[i];
                    content.text = teachContent[i].ToString();
                }
            }
        }

        switch (_isFinish)
        {
            case 0:
                if (isGoddess)
                {
                    for (int i = 1; i <= _teachGoddessPage; i++)
                    {
                        if (i == _page)
                        {
                            //background.sprite = teachImage[i];
                            content.text = teachContent[i].ToString();
                        }
                    }
                }
                break;

            case 1:
                if (isChange)
                {
                    for (int i = 1; i <= _teachChangePage; i++)
                    {
                        if (i == _page)
                        {
                            //background.sprite = teachImage[i + _teachGoddessPage];
                            content.text = teachContent[i + _teachGoddessPage].ToString();
                        }
                    }
                }
                break;

            case 2:
                if (isWind)
                {
                    for (int i = 1; i <= _teachWindPage; i++)
                    {
                        if (i == _page)
                        {
                            //background.sprite = teachImage[i + _teachChangePage];
                            content.text = teachContent[i + _teachChangePage].ToString();
                        }
                    }
                }
                break;

            case 3:
                if (isPerformance)
                {
                    for (int i = 1; i <= _teachPerformancePage; i++)
                    {
                        if (i == _page)
                        {
                            //background.sprite = teachImage[i + _teachWindPage];
                            content.text = teachContent[i + _teachWindPage].ToString();
                        }
                    }
                }
                break;
        }

        switch (_isFinish)
        {
            case 0:
                if (isTime)
                {
                    for (int i = 1; i <= _teachTimePage; i++)
                    {
                        if (i == _page)
                        {
                            //background.sprite = teachImage[i];
                            content.text = teachContent[i].ToString();
                        }
                    }
                }
                break;

            case 1:
                if (isSupplies)
                {
                    for (int i = 1; i <= _teachSuppliesPage; i++)
                    {
                        if (i == _page)
                        {
                            //background.sprite = teachImage[i + _teachTimePage];
                            content.text = teachContent[i + _teachTimePage].ToString();
                        }
                    }
                }
                break;
        }
    }
    void ButtonActive()
    {
        if (isTemperature)
        {
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
        }
  
        if (isGoddess && _isFinish == 0)
        {
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
        }
        else if (isChange && _isFinish == 1)
        {
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
        }
        else if (isWind && _isFinish == 2)
        {
            switch (_page)
            {
                case 1:
                    teachButton[0].interactable = true;
                    teachButton[1].interactable = false;
                    teachButton[2].interactable = false;
                    break;
            }
        }
        else if (isPerformance && _isFinish == 3)
        {
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
        }

        if (isTime && _isFinish == 1)
        {
            switch (_page)
            {
                case 1:
                    teachButton[1].interactable = false;
                    break;

                case 2:
                    teachButton[1].interactable = true;
                    teachButton[2].interactable = true;
                    break;

                case 3:
                    teachButton[0].interactable = true;
                    teachButton[2].interactable = false;
                    break;
            }
        }
        else if (isSupplies && _isFinish == 2)
        {
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
        CloseTeach();
    }

    void CloseTeach()
    {
        if (isTemperature)
        {
            StoryThermometerControl_Girl.isThermometer = true;
        }

        if (isGoddess && _isFinish == 0)
        {
            _isFinish++;
        }
        else if (isChange && _isFinish == 1)
        {
            _isFinish++;
        }
        else if (isWind && _isFinish == 2)
        {
            _isFinish++;
        }
        else if (isPerformance && _isFinish == 0)
        {
            _isFinish++;
        }

        if (isTime && _isFinish == 1)
        {
            _isFinish++;
        }
        else if (isSupplies && _isFinish == 2)
        {
            _isFinish++;
        }
    }
}
