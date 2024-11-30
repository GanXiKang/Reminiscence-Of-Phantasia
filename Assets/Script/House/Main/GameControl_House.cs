using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_House : MonoBehaviour
{
    public static int _day = 0;
    public static int _storyNum = 0;

    void Start()
    {
        _day++;
        switch (_day)
        {
            case 1:
                UIAboveObject_House.isAboveDoor = true;
                InteractableControl_House.isColliderActive[1] = true;
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
                InteractableControl_House.isColliderActive[1] = true;
                InteractableControl_House.isColliderActive[4] = true;
                InteractableControl_House.isColliderActive[5] = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 17;
                break;
        }
    }
}
