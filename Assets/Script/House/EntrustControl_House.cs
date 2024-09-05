using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrustControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] entrustUI;
    public static bool isEntrustActive = false;
    bool isLetterActive;
    bool isContentActive;

    void Start()
    {
        isLetterActive = false;
        isContentActive = false;
    }

    void Update()
    {
        
    }
}
