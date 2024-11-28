using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl_House : MonoBehaviour
{
    public int _serialNumber;
    public static int _nowNumber;
    float _forwardValue = 0.1f;

    bool IsFacingObject(Transform playerTransform)
    {
        Vector3 playerForward = playerTransform.forward;
        Vector3 directionToObject = (transform.position - playerTransform.position).normalized;

        float dotProduct = Vector3.Dot(playerForward, directionToObject);

        return dotProduct > _forwardValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (IsFacingObject(other.transform))
            {
                if (InteractableControl_House.isCatSeeWorkbench)
                {
                    InteractableControl_House.isCatSeeWorkbench = false;
                    UIControl_House.isDialogue = true;
                    DialogueControl_House.isCatTalk = true;
                    DialogueControl_House._textCount = 21;
                }
                else if (InteractableControl_House.isBirdSeeBed)
                {
                    InteractableControl_House.isBirdSeeBed = false;
                    UIControl_House.isDialogue = true;
                    DialogueControl_House.isBirdTalk = true;
                    DialogueControl_House._textCount = 29;
                }
                else if (InteractableControl_House.isBirdSeeBookcase)
                {
                    InteractableControl_House.isBirdSeeBookcase = false;
                    UIControl_House.isDialogue = true;
                    DialogueControl_House.isBirdTalk = true;
                    DialogueControl_House._textCount = 32;
                }
                else
                {
                    InteractableControl_House.isInteractable = true;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (UIControl_House.isDialogue) return;

        if (CameraControl_House.isFreeLook)
        {
            if (other.tag == "Player")
            {
                if (IsFacingObject(other.transform))
                {
                    _nowNumber = _serialNumber;
                    InteractableControl_House.isInteractable = true;
                }
                else
                {
                    InteractableControl_House.isInteractable = false;
                }
            }
        }
        else
        {
            InteractableControl_House.isInteractable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InteractableControl_House.isInteractable = false;
        }
    }
}
