using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIControl_House : MonoBehaviour
{
    [Header("LoadingUI")]
    public GameObject UI;
    public Image a, b, c;

    float value = 1;
    float speed = 0.9f;
    bool isClose = false;
    bool isOpen = false;

    void Start()
    {
        Close();
    }

    void Update()
    {
        a.fillAmount = value;
        b.fillAmount = value;
        c.fillAmount = value;

        if (isClose)
        {
            if (value > 0)
            {
                value -= speed * Time.deltaTime;
            }
            else
            {
                isClose = false;
                UI.SetActive(false);
            }
        }
        if (isOpen)
        {
            if (value < 1)
            {
                value += speed * Time.deltaTime;
            }
            else
            {
                isOpen = false;
            }
        }
    }

    void Close()
    {
        isClose = true;
    }
    void Open()
    {
        isOpen = true;
        UI.SetActive(true);
    }
}
