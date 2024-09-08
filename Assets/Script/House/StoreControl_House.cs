using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] storeUI;
    public GameObject[] homePageButton;
    public static bool isStoreActive = false;
    bool isHomePageActive = false;
    bool isContentActive = false;

    void Update()
    {
        storeUI[0].SetActive(isStoreActive);
        storeUI[1].SetActive(isHomePageActive);
        storeUI[2].SetActive(isContentActive);

        OpenUI();
    }

    void OpenUI()
    {
        if (!isStoreActive) return;

        if (storeUI[0].GetComponent<RectTransform>().localScale.x < 1)
        {
            storeUI[0].GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
        }
        else
        {
            if (!isHomePageActive && !isContentActive)
                isHomePageActive = true;
        }
    }
    void LeaveState()
    {
        UIAboveObject_House.isDialogBoxActive = false;
        DoorControl_House.isLeave = true;
        isHomePageActive = false;
        isContentActive = false;
        storeUI[1].GetComponent<CanvasGroup>().interactable = true;
        storeUI[0].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    public void Button_Product(int _product)
    {
        if (isHomePageActive)
        {
            isContentActive = true;
            isHomePageActive = false;
            CatControl_House.isBag = true;
        }
        else
        { 

        }
    }
    public void Button_Buy()
    {
        CatControl_House.isHappy = true;
    }
    public void Button_Back()
    {
        isHomePageActive = false;
        isContentActive = true;
        CatControl_House.isBag_Out = true;
    }
    public void Button_Leave()
    {
        CatControl_House.isBye = true;
        UIAboveObject_House.isDialogBoxActive = true;
        UIAboveObject_House._whichDialog = 4;
        storeUI[1].GetComponent<CanvasGroup>().interactable = false;
        Invoke("LeaveState", 1f);
    }
}
