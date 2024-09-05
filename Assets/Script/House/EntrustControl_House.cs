using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrustControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] entrustUI;
    public Button[] letterButton;
    public static bool isEntrustActive = false;
    bool isLetterActive = false;
    bool isContentActive = false;

    void Update()
    {
        entrustUI[0].SetActive(isEntrustActive);
        entrustUI[1].SetActive(isLetterActive);
        entrustUI[2].SetActive(isContentActive);

        OpenUI();
    }

    void OpenUI()
    {
        if (isEntrustActive)
        {
            if (entrustUI[0].GetComponent<RectTransform>().localScale.x < 1)
            {
                entrustUI[0].GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
            }
            else
            {
                if (!isLetterActive && !isContentActive)
                {
                    isLetterActive = true;
                }
            }
        }
        else
        {
            entrustUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
            isLetterActive = false;
            isContentActive = false;
        }
    }

    public void Button_Letter(int _letter)
    {
        isContentActive = true;
        isLetterActive = false;
    }
}
