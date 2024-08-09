using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingUI_Gril : MonoBehaviour
{
    [Header("Scene")]
    public GameObject street;
    public GameObject forest;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a,b,c;
    public static bool isLeft, isRight;
    float valueA, valueB, valueC;

    void Start()
    {
        isLeft = false;
        isRight = false;
        valueA = 0;
        valueB = 0;
        valueC = 0;
    }

    void Update()
    {
        LeftSwitchScene();
        RightSwitchScene();
    }

    void LeftSwitchScene()
    {
        if (isLeft)
        {
            a.fillOrigin = (int)Image.OriginHorizontal.Left;
            b.fillOrigin = 0;
            c.fillOrigin = 0;
        }
    }
    void RightSwitchScene()
    {
        if (isRight)
        {
            a.fillOrigin = (int)Image.OriginHorizontal.Right;
            b.fillOrigin = 1;
            c.fillOrigin = 1;
        }
    }
    void A_BarValue()
    {
        
    }
    void B_BarValue()
    {

    }
    void C_BarValue()
    {

    }
}
