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
    float _loadingSpeed = 1.5f;

    void Update()
    {
        loadingUI.SetActive(isLoading);

        LeftSwitch();
        RightSwitch();

        if (Input.GetKeyDown(KeyCode.L))  //úy‘á
        {
            isLoading = true;
            isRight = true;
        }
    }

    void LeftSwitch()
    {
        //if (isLeft)
        //{
        //    a.fillOrigin = (int)Image.OriginHorizontal.Left;
        //    b.fillOrigin = 0;
        //    c.fillOrigin = 0;
        //}
    }
    void RightSwitch()
    {
        if (isRight)
        {
            print("OK");
            StartCoroutine(RightSwitchScene());
        }
    }
    void BarValue(Image bar, bool isAdd)
    {
        if (isAdd)
        {
            if (bar.fillAmount < 1)
            {
                bar.fillAmount += _loadingSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (bar.fillAmount > 0)
            {
                bar.fillAmount -= _loadingSpeed * Time.deltaTime;
            }
        }
    }

    //IEnumerator LeftSwitchScene()
    //{
        
    //}
    IEnumerator RightSwitchScene()
    {
        BarValue(a, true);
        yield return new WaitForSeconds(0.5f);
        BarValue(b, true);
        yield return new WaitForSeconds(0.5f);
        BarValue(c, true);
        isRight = false;

        //a.fillOrigin = (int)Image.OriginHorizontal.Left;
        //b.fillOrigin = 0;
        //c.fillOrigin = 0;

        //BarValue(a, false);
        //yield return new WaitForSeconds(0.5f);
        //BarValue(b, false);
        //yield return new WaitForSeconds(0.5f);
        //BarValue(c, false);
    }
}
