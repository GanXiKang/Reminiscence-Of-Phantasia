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

    void Start()
    {
        currentColor = hintF.color;
    }

    void Update()
    {
        interactableUI.SetActive(isInteractable);
        currentColor.a = _alpha;
        hintF.color = currentColor;

        Interactable();
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
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookDoor = true;
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
    void AppearInteractableHint()
    { 
        if (_alpha < 1)
        {
            _alpha += _screenSpeed * Time.deltaTime;
        }
    }
}
