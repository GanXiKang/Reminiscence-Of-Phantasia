using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] storeUI;
    public static bool isStoreActive = false;
    bool isHomePageActive;
    bool isContentActive;

    void Start()
    {
        isHomePageActive = false;
        isContentActive = false;
    }

    void Update()
    {
        
    }
}
