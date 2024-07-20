using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableControl_House : MonoBehaviour
{
    [Header("InteractableUI")]
    public GameObject interactableUI;
    public static bool isInteractable = false;
    Vector3 targetPos;
    Vector3 startPos;
    float _speed = 20f;

    void Start()
    {
        targetPos = interactableUI.transform.position + new Vector3(0f, 20f, 0f);
        startPos = interactableUI.transform.position;
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
        interactableUI.transform.position = Vector3.MoveTowards(startPos, targetPos, _speed);
    }
    void DisappearInteractableHint()
    {
        interactableUI.transform.position = startPos;
    }
}
