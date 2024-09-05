using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] storeUI;
    public static bool isStoreActive = false;
    bool isHomePageActive = false;
    bool isContentActive = false;

    void Start()
    {

    }

    void Update()
    {
        storeUI[0].SetActive(isStoreActive);
        storeUI[1].SetActive(isHomePageActive);
        storeUI[2].SetActive(isContentActive);

        OpenUI();
    }

    void OpenUI()
    {
        if (isStoreActive)
        {
            if (storeUI[0].GetComponent<RectTransform>().localScale.x < 1)
            {
                storeUI[0].GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
            }
            else
            {
                isHomePageActive = true;
            }
        }
        else
        {
            storeUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
            isHomePageActive = false;
            isContentActive = false;
        }
    }
}
