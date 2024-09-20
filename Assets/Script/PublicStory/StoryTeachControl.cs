using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTeachControl : MonoBehaviour
{
    [Header("Teach")]
    public bool isTemperature;
    int _teachTemperaturePage = 4;
    public bool isGoddess;
    int _teachGoddessPage = 2;
    public bool isWind;
    int _teachWindPage = 2;
    public bool isTime;
    int _teachTimePage = 3;
    public bool isSupplies;
    int _teachSuppliesPage = 4;
    bool isFinish = false;

    [Header("TextFile")]
    public TextAsset[] teachContent;
    public Sprite[] teachImage;

    [Header("UI")]
    public GameObject teachUI;
    public GameObject window;
    public Image background;
    public Text content;
    public Button[] teachButton;
    public static bool isTeachActive = true;
    int _page = 1;

    void Update()
    {
        teachUI.SetActive(isTeachActive);

        Teach();
        ButtonActive();
    }

    void Teach()
    {
        if (isTemperature)
        {
            for (int i = 1; i <= _teachTemperaturePage; i++)
            {
                if (i == _page)
                {
                    //background.sprite = teachImage[i];
                    content.text = teachContent[i].ToString();
                }
            }
        }

        if (isGoddess && !isFinish)
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

        if (isWind && isFinish)
        {
            for (int i = 1; i <= _teachWindPage; i++)
            {
                if (i == _page)
                {
                    //background.sprite = teachImage[i];
                    content.text = teachContent[i].ToString();
                }
            }
        }

        if (isTime && !isFinish)
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

        if (isSupplies && isFinish)
        {
            for (int i = 1; i <= _teachSuppliesPage; i++)
            {
                if (i == _page)
                {
                    //background.sprite = teachImage[i];
                    content.text = teachContent[i].ToString();
                }
            }
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

                case 4:
                    teachButton[1].interactable = true;
                    break;
            }
        }

        if (isGoddess && !isFinish)
        {
            
        }

        if (isWind && isFinish)
        {
            
        }

        if (isTime && !isFinish)
        {
            
        }

        if (isSupplies && isFinish)
        {
            
        }
    }

    public void Button_ChangePage(int _change)
    {
        _page += _change;
    }
    public void Button_Close()
    {
        isTeachActive = false;
    }
}
