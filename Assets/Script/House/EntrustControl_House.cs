using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrustControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] entrustUI;
    public static bool isEntrustActive = false;
    bool isLetterActive = false;
    bool isContentActive = false;

    void Start()
    {

    }

    void Update()
    {
        entrustUI[0].SetActive(isEntrustActive);
        entrustUI[1].SetActive(isLetterActive);
        entrustUI[2].SetActive(isContentActive);

        if (isEntrustActive)
        {

        }
        else
        {
            isLetterActive = false;
            isContentActive = false;
        }
    }
}
