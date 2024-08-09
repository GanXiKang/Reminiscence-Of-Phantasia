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
    public static bool isLoading = false;
    public static bool isLeft = false;
    public static bool isRight = false;
    float valueA = 0, valueB = 0, valueC = 0;

    void Update()
    {
        loadingUI.SetActive(isLoading);
        a.fillAmount = valueA;
        b.fillAmount = valueB;
        c.fillAmount = valueC;

        LeftSwitchScene();
        RightSwitchScene();

        if (Input.GetKeyDown(KeyCode.L))  //úy‘á
        {
            isLoading = true;
            isRight = true;
        }
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
            A_BarValue();
            B_BarValue();
            C_BarValue();
        }
    }
    void A_BarValue()
    {
        if (valueA < 1)
        {
            valueA += 1.5f * Time.deltaTime;
        }
        else 
        {
            isRight = false;
        }
        if(valueA > 0)
        {
            valueA -= 1.5f * Time.deltaTime;
        }
    }
    void B_BarValue()
    {
        if (valueB < 1)
        {
            valueB += 1f * Time.deltaTime;

        }
        else
        {
            isRight = false;
        }
        if (valueB >0)
        {
            valueB -= 1f * Time.deltaTime;
        }
    }
    void C_BarValue()
    {
        if (valueC < 1)
        {
            valueC += 0.5f * Time.deltaTime;
        }
        else
        {
            isRight = false;
        }
        if (valueC > 0)
        {
            valueC += 0.5f * Time.deltaTime;
        }
    }
}
