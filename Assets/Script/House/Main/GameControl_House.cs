using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_House : MonoBehaviour
{
    public static int _day = 1;
    public static int _storyNum = 2;

    void Start()
    {
        switch (_day)
        {
            case 1:
                UIAboveObject_House.isAboveDoor = true;
                InteractableControl_House.isColliderActive[2] = true;
                InteractableControl_House.isColliderActive[3] = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 1;
                break;
        }
    }
}
