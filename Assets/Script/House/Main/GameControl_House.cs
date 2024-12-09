using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip morning;

    public static int _day = 3;
    public static int _storyNum = 2;

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
                InteractableControl_House.isColliderActive[1] = true;
                InteractableControl_House.isColliderActive[4] = true;
                InteractableControl_House.isColliderActive[5] = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 17;
                break;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))  //快捷建
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //去故事一
            {
                _storyNum = 1;
                InteractableControl_House.isColliderActive[3] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) //去故事二
            {
                _storyNum = 2;
                InteractableControl_House.isColliderActive[3] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) //去Store
            {
                DoorControl_House.isStore = true;
                InteractableControl_House.isColliderActive[2] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) //去Entrust
            {
                DoorControl_House.isEntrust = true;
                InteractableControl_House.isColliderActive[2] = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5)) //Bookcase All Book
            {
                BookcaseControl_House.isNewBookActive = true;
                BookcaseControl_House.bookActive[0] = true;
                BookcaseControl_House.bookActive[1] = true;
                BookcaseControl_House.bookActive[2] = true;
                BookcaseControl_House.bookActive[3] = true;
                InteractableControl_House.isColliderActive[4] = true;
                InteractableControl_House.isColliderActive[5] = true;
            }
        }
    }
}
