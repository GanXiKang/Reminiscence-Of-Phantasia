using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableControl_House : MonoBehaviour
{
    [Header("InteractableUI")]
    public GameObject interactableUI;
    public GameObject interactableHint;
    public static bool isInteractable = false;
    Vector3 targetPos;
    Vector3 startPos;
    float _speed = 2f;

    void Start()
    {
        targetPos = Vector3.zero;
        startPos = interactableHint.transform.position;
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
        interactableHint.transform.position = Vector3.MoveTowards(startPos, targetPos, _speed * Time.deltaTime);
    }
    void DisappearInteractableHint()
    {
        interactableHint.transform.position = Vector3.MoveTowards(targetPos, startPos, _speed * Time.deltaTime);
    }
}
