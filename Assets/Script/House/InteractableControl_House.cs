using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableControl_House : MonoBehaviour
{
    [Header("InteractableUI")]
    public GameObject interactableUI;
    public Image hintF;
    Color currentColor;
    float _alpha = 0f;
    public float _screenSpeed = 3f;
    public static bool isInteractable = false;

    [Header("Animals")]
    public GameObject bird;
    public GameObject cat;

    void Start()
    {
        currentColor = hintF.color;
    }

    void Update()
    {
        InteractableButton_F();
        Interactable();
    }

    void InteractableButton_F()
    {
        interactableUI.SetActive(isInteractable);
        currentColor.a = _alpha;
        hintF.color = currentColor;
    }
    void AppearInteractableHint()
    {
        if (_alpha < 1)
        {
            _alpha += _screenSpeed * Time.deltaTime;
        }
    }
    void Interactable()
    {
        if (isInteractable)
        {
            AppearInteractableHint();
            if (Input.GetKeyDown(KeyCode.F))
            {
                isInteractable = false;
                switch (ColliderControl_House._nowNumber)
                {
                    case 1:         
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookWorkbench = true;
                        break;  //工作台

                    case 2:
                        DoorControl_House.isLoading = true;
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookDoor = true;
                        //Invoke("DoorLoadingUI", 3f);
                        StartCoroutine(WhoIsVisit());
                        break;  //門

                    case 3:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBed = true;
                        break;  //床

                    case 4:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBookcase = true;
                        break;  //書櫃

                    case 5:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookShowcase = true;
                        break;  //展示櫃
                }
            }
        }
        else 
        {
            _alpha = 0;
        }
    }
    void DoorLoadingUI()
    {
        DoorControl_House.isLoading = false;
        if (DoorControl_House.isBird)
        {
            bird.SetActive(true);
            EntrustControl_House.isEntrustActive = true;
        }
        else if (DoorControl_House.isCat)
        {
            cat.SetActive(true);
            StoreControl_House.isStoreActive = true;
        }
    }
    IEnumerator WhoIsVisit()
    {
        yield return new WaitForSeconds(3f);
        DoorControl_House.isLoading = false;
        if (DoorControl_House.isBird)
        {
            bird.SetActive(true);
            UIAboveObject_House.isDialogBoxActive = true;
            UIAboveObject_House._whichDialog = 1;
            yield return new WaitForSeconds(2f);
            BirdControl_House.isIdle = true;
            BirdControl_House.isDeliver = true;
            EntrustControl_House.isEntrustActive = true;
            UIAboveObject_House.isDialogBoxActive = false;
        }
        else if (DoorControl_House.isCat)
        {
            cat.SetActive(true);
            CatControl_House.isWave = true;
            UIAboveObject_House.isDialogBoxActive = true;
            UIAboveObject_House._whichDialog = 2;
            yield return new WaitForSeconds(2f);
            CatControl_House.isWave = false;
            StoreControl_House.isStoreActive = true;
            UIAboveObject_House.isDialogBoxActive = false;
        }
    }
}
