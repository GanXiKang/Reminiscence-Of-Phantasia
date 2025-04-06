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
                GameDay01();
                break;

            case 2:
                GameDay02();
                break;

            case 3:
                GameDay03();
                break;

            case 4:
                GameDay04();
                break;
        }
        //SaveGame();
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

        AvatarControl_House.isTalk = false;

        DialogueControl_House.isBirdTalk = false;
        DialogueControl_House.isCatTalk = false;
        DialogueControl_House._paragraph = 0;
        DialogueControl_House.isAutoNext = false;
        DialogueControl_House.isAutoPlot = false;

        BirdControl_House.isIdle = false;
        BirdControl_House.isDeliver = false;
        BirdControl_House.isDeliver_Close = false;
        BirdControl_House.isHappy = false;
        BirdControl_House.isBye = false;
        BirdControl_House._goPointNum = 0;

        CatControl_House.isHappy = false;
        CatControl_House.isWave = false;
        CatControl_House.isBye = false;
        CatControl_House.isBag = false;
        CatControl_House.isBag_On = false;
        CatControl_House._goPointNum = 0;

        DoorControl_House.isLoading = false;
        DoorControl_House.isEntrust = false;
        DoorControl_House.isStore = false;
        DoorControl_House.isCat = false;
        DoorControl_House.isLeave = false;
        DoorControl_House.isBird = _day == 2;

        EntrustControl_House.isEntrustActive = false;
        StoreControl_House.isStoreActive = false;
        StoreControl_House.isPlotBut = _day != 1;

        InteractableControl_House.isInteractable = false;
        InteractableControl_House.isCatSeeWorkbench = false;
        InteractableControl_House.isBirdDoorBell = false;
        InteractableControl_House.isBirdLeave = false;
        InteractableControl_House.isReadMomLetter = false;
        InteractableControl_House.isBookcasePlotOnce = false;
        InteractableControl_House.isBirdFirstMeet = false;
        InteractableControl_House.isEnding = false;
        InteractableControl_House.isMomEntrust = _day == 3;
        InteractableControl_House.isCatLeave = _day != 1;
        InteractableControl_House.isBirdEntrust = _day != 1;
        for (int c = 1; c < InteractableControl_House.isColliderActive.Length; c++)
            InteractableControl_House.isColliderActive[c] = false;

        PlayerControl_House.isPlayerEndPoint = false;
        PlayerControl_House.isWave = false;
        PlayerControl_House.isHappy = false;
        PlayerControl_House.isSleep = false;


}
    void GameDay01()
    {
        BookcaseControl_House.bookActive[0] = false;
        BookcaseControl_House.bookActive[1] = false;
        BookcaseControl_House.bookActive[2] = false;
        BookcaseControl_House.bookActive[3] = false;
        BookcaseControl_House._bookActiveNum = 0;

        InteractableControl_House.isColliderActive[2] = true;
        UIAboveObject_House.isAboveDoor = true;

        UIControl_House.isDialogue = true;
        DialogueControl_House._textCount = 1;
    }
    void GameDay02()
    {
        BookcaseControl_House.bookActive[0] = true;
        BookcaseControl_House.bookActive[1] = false;
        BookcaseControl_House.bookActive[2] = false;
        BookcaseControl_House.bookActive[3] = false;
        BookcaseControl_House._bookActiveNum = 1;

        InteractableControl_House.isColliderActive[1] = true;
        UIAboveObject_House.isAboveWorkbench = true;
        UIAboveObject_House.isAboveBed = false;

        UIControl_House.isDialogue = true;
        DialogueControl_House.isBirdTalk = true;
        DialogueControl_House._textCount = 30;
    }
    void GameDay03()
    {
        BookcaseControl_House.bookActive[0] = true;
        BookcaseControl_House.bookActive[1] = true;
        BookcaseControl_House.bookActive[2] = false;
        BookcaseControl_House.bookActive[3] = false;
        BookcaseControl_House._bookActiveNum = 2;

        InteractableControl_House.isColliderActive[1] = true;
        InteractableControl_House.isColliderActive[4] = true;
        InteractableControl_House.isColliderActive[5] = true;
        UIAboveObject_House.isAboveWorkbench = true;
        UIAboveObject_House.isAboveBed = false;
        UIAboveObject_House.isStoreHintActive = false;

        UIControl_House.isDialogue = true;
        DialogueControl_House._textCount = 17;
    }
    void GameDay04()
    {
        BookcaseControl_House.bookActive[0] = true;
        BookcaseControl_House.bookActive[1] = true;
        BookcaseControl_House.bookActive[2] = true;
        BookcaseControl_House.bookActive[3] = false;
        BookcaseControl_House._bookActiveNum = 3;

        InteractableControl_House.isColliderActive[1] = true;
        InteractableControl_House.isColliderActive[4] = true;
        InteractableControl_House.isColliderActive[5] = true;
        UIAboveObject_House.isAboveWorkbench = true;
        UIAboveObject_House.isAboveBed = false;
        UIAboveObject_House.isStoreHintActive = false;

        UIControl_House.isDialogue = true;
        DialogueControl_House._textCount = 46;
    }
    void SaveGame()
    {
        GameData gameData = new GameData
        {
            gameDay = _day,
            gameStoryNum = _storyNum,
            playerCoins = _MyCoin,

            isSpecialEnd = Showcase_House.isSpecialEnd,
            storyBookPaperNum = Showcase_House._storyBookPaperNum
        };

        SaveManagerControl.Instance.SaveGame(gameData);
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
