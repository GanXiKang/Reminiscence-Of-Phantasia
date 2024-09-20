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
    public static bool isTeachActive = false;
    int _page = 1;

    void Update()
    {
        teachUI.SetActive(isTeachActive);

        Teach();
    }

    void Teach()
    {
        if (isTemperature)
        {
            
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
    public void Button_Close(int _change)
    {
        isTeachActive = false;
    }
}
