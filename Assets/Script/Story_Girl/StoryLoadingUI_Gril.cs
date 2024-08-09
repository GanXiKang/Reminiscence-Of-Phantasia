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
    public static bool isLoading;
    public static bool isLeft, isRight;
    float valueA, valueB, valueC;
    float _loadingSpeed = 1.5f;

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
        loadingUI.SetActive(isLoading);

        LeftSwitchScene();
        RightSwitchScene();

        if (Input.GetKeyDown(KeyCode.L))
        {
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

            Invoke("A_BarValue", 0.2f);
            Invoke("B_BarValue", 0.4f);
            Invoke("C_BarValue", 0.6f);
        }
    }
    void A_BarValue()
    {
        if (valueA < 1)
        {
            valueA += _loadingSpeed * Time.deltaTime;
            a.fillAmount = valueA;
        }
        else 
        {
            isRight = false;
        }
        if(valueA > 0)
        {
            valueA -= _loadingSpeed * Time.deltaTime;
            a.fillAmount = valueA;
        }
    }
    void B_BarValue()
    {
        if (valueB < 1)
        {
            valueB += _loadingSpeed * Time.deltaTime;
            b.fillAmount = valueB;
        }
        else
        {
            isRight = false;
        }
        if (valueB >0)
        {
            valueB -= _loadingSpeed * Time.deltaTime;
            b.fillAmount = valueB;
        }
    }
    void C_BarValue()
    {
        if (valueC < 1)
        {
            valueC += _loadingSpeed * Time.deltaTime;
            c.fillAmount = valueC;
        }
        else
        {
            isRight = false;
        }
        if (valueC > 0)
        {
            valueC += _loadingSpeed * Time.deltaTime;
            c.fillAmount = valueC;
        }
    }
}
