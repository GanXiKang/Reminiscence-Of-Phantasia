using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableControl_House : MonoBehaviour
{
    [Header("InteractableUI")]
    public GameObject interactableUI;
    public static bool isInteractable = false;

    void Start()
    {

    }

    void Update()
    {
        interactableUI.SetActive(isInteractable);

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
                DisappearInteractableHint();
                switch (ColliderControl_House._nowNumber)
                {
                    case 1:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookWorkbench = true;
                        break;
                }
            }
        }
    }
    void AppearInteractableHint()
    {

    }
    void DisappearInteractableHint()
    {
        
    }
}
