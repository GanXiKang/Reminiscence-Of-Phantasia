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
        GameStatic();
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

    void GameStatic()
    {
        BedControl_House.isMovingToBed = false;

        CameraControl_House.isFreeLook = false;
        CameraControl_House.isLookWorkbench = false;
        CameraControl_House.isLookStorkBook = false;
        CameraControl_House.isLookPlayer = false;
        CameraControl_House.isLookDoor = false;
        CameraControl_House.isLookBed = false;
        CameraControl_House.isLookBedcase = false;
        CameraControl_House.isLookBookcase = false;
        CameraControl_House.isLookShowcase = false;
        CameraControl_House.isLookDoorPlot = false;
        CameraControl_House.isLookWorkPlot = false;
        CameraControl_House.isLookEndingPlot = false;

        CameraStartMove_House.isStartMoveCamera = false;
        CameraStartMove_House.isMoving = false;

        BookcaseControl_House._bookNum = 0;
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
            if (Input.GetKeyDown(KeyCode.P))
            {
                CameraControl_House.isFreeLook = false;
                CameraControl_House.isLookEndingPlot = true;
                DoorControl_House.isCat = true;
                DoorControl_House.isBird = true;
                BirdControl_House.isIdle = true;
                BirdControl_House._goPointNum = 4;
                CatControl_House._goPointNum = 3;
                PlayerControl_House.isPlayerEndPoint = true;

                UIControl_House.isDialogue = true;
                DialogueControl_House.isBirdTalk = true;
                DialogueControl_House._textCount = 49;
            }
        }
    }
}
