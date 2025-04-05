using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip morning;

    public static int _day = 1;
    public static int _MyCoin = 300;
    public static int _storyNum = 0;

    void Start()
    {
        BGM.PlayOneShot(morning);
        switch (_day)
        {
            case 1:
                UIAboveObject_House.isAboveDoor = true;
                InteractableControl_House.isColliderActive[2] = true;

                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 1;
                break;

            case 2:
                DoorControl_House.isBird = true;
                UIAboveObject_House.isAboveWorkbench = true;
                UIAboveObject_House.isAboveBed = false;
                InteractableControl_House.isColliderActive[1] = true;

                UIControl_House.isDialogue = true;
                DialogueControl_House.isBirdTalk = true;
                DialogueControl_House._textCount = 30;
                break;

            case 3:
                UIAboveObject_House.isAboveWorkbench = true;
                UIAboveObject_House.isAboveBed = false;
                UIAboveObject_House.isStoreHintActive = false;
                InteractableControl_House.isColliderActive[1] = true;
                InteractableControl_House.isColliderActive[4] = true;
                InteractableControl_House.isColliderActive[5] = true;

                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 17;
                DialogueControl_House.isAutoNext = false;
                break;

            case 4:
                UIAboveObject_House.isAboveWorkbench = true;
                UIAboveObject_House.isAboveBed = false;
                UIAboveObject_House.isStoreHintActive = false;
                InteractableControl_House.isColliderActive[1] = true;
                InteractableControl_House.isColliderActive[4] = true;
                InteractableControl_House.isColliderActive[5] = true;

                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 46;
                DialogueControl_House.isAutoNext = false;
                break;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) 
            {
                _storyNum = 1;
                InteractableControl_House.isColliderActive[3] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _storyNum = 2;
                InteractableControl_House.isColliderActive[3] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                DoorControl_House.isStore = true;
                InteractableControl_House.isColliderActive[2] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) 
            {
                DoorControl_House.isEntrust = true;
                InteractableControl_House.isColliderActive[2] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                BookcaseControl_House.isNewBookActive = true;
                BookcaseControl_House.bookActive[0] = true;
                BookcaseControl_House.bookActive[1] = true;
                BookcaseControl_House.bookActive[2] = true;
                BookcaseControl_House.bookActive[3] = true;
                InteractableControl_House.isColliderActive[4] = true;
                InteractableControl_House.isColliderActive[5] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _storyNum = 3;
                InteractableControl_House.isColliderActive[3] = true;
            }
        }

        if (Input.GetKey(KeyCode.P))
        {
            UIControl_House.isDialogue = true;
            DialogueControl_House._textCount = 49;
        }
    }
}
