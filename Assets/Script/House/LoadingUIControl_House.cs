using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIControl_House : MonoBehaviour
{
    [Header("LoadingUI")]
    public GameObject UI;
    public Image a, b, c;
    public float speed = 1.5f;
    float value = 1;
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
        UI.SetActive(true);
        isClose = true;
    }
    void Open()
    {
        UI.SetActive(true);
        isOpen = true;
    }
}
