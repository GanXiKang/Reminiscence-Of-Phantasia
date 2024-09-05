using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (isStoreActive)
        {

        }
        else 
        {
            isHomePageActive = false;
            isContentActive = false;
        }
    }
}
