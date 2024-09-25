using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenControl : MonoBehaviour
{
    [Header("BlackScreen")]
    public GameObject blackScreen;
    public Image panel;
    Color currentColor;
    float _alpha = 0f;
    public float _screenSpeed = 50f;
    public static bool isCloseBlackScreen = false;
    public static bool isOpenBlackScreen = false;

    void Start()
    {
        currentColor = panel.color;
    }

    void Update()
    {
        currentColor.a = _alpha;
        panel.color = currentColor;

        OpenBlackScreen();
        CloseBlackScreen();
    }

    void CloseBlackScreen()
    {
        if (isCloseBlackScreen)
        {
            if (_alpha > 0)
            {
                _alpha -= _screenSpeed * Time.deltaTime;
            }
            else
            {
                isCloseBlackScreen = false;
                blackScreen.SetActive(false);
            }
        }
    }
    void OpenBlackScreen()
    {
        if (isOpenBlackScreen)
        {
            blackScreen.SetActive(true);
            if (_alpha < 1)
            {
                _alpha += _screenSpeed * Time.deltaTime;
            }
            else
            {
                isOpenBlackScreen = false;
                Invoke("WaitCloseBlackSreen", 1f);
            }
        }
    }
}
