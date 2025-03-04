using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Momotaro : MonoBehaviour
{
    [Header("Exit")]
    public GameObject forest;
    public GameObject mountain;
    public GameObject plaza;
    public static bool isForestActive;
    public static bool isMountainActive;
    public static bool isPlazaActive;
    public static bool isMeetDonkey;
    bool isOnce = true;

    [Header("Statue")]
    public GameObject catLow;
    public GameObject catFinish;
    public GameObject performancesPoint;
    public static bool isPerformancesPointActive;
    public static bool isReadly;

    [Header("SceneActive")]
    public GameObject sceneRiver;
    public GameObject sceneForest;
    public GameObject scenePlaza;

    [Header("Npc")]
    public SpriteRenderer momotaro;
    public SpriteRenderer cat;
    public GameObject donkey;
    public GameObject parrot;
    public static bool isParrotActive;

    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    void Start()
    {
        isForestActive = false;
        isMountainActive = false;
        isPlazaActive = false;
        isMeetDonkey = false;
        isPerformancesPointActive = false;
        isReadly = false;
        isParrotActive = false;

        StoryColliderControl_Momotaro.isStoneSuccess = true;

        StoryExitControl_Momotaro.isExit = false;

        StoryItemIntroduce_Momotaro.isIntroduce = true;

        StoryLoadingScene_Momotaro.isLoading = false;
        StoryLoadingScene_Momotaro.isOpen = false;
        StoryLoadingScene_Momotaro.isFirstGoPlaza = false;
        StoryLoadingScene_Momotaro.isPlotAnimator = false;
        StoryLoadingScene_Momotaro.isHintGoPlaza = false;



        StoryInteractableControl_Momotaro.isInteractableUI = false;
        StoryInteractableControl_Momotaro.isGiveItem = false;
        StoryInteractableControl_Momotaro.isBagGetItem = false;
        StoryInteractableControl_Momotaro.isPlayerMove = true;
        StoryInteractableControl_Momotaro._findPartner = 0;
        StoryInteractableControl_Momotaro.isSpecialEnding = false;
        StoryInteractableControl_Momotaro.isSpecialOver = false;
        StoryInteractableControl_Momotaro.isMeetPartner = false;
        StoryInteractableControl_Momotaro.isGoddessGetSkill = false;
        StoryInteractableControl_Momotaro.isAnswerLie = false;
        StoryInteractableControl_Momotaro.isAnswerGold = false;
        StoryInteractableControl_Momotaro.isMomoFindGoddess = false;
        StoryInteractableControl_Momotaro.isGiveTheRightGift = false;
        StoryInteractableControl_Momotaro.isWrongGift = false;
        StoryInteractableControl_Momotaro.isFinishWork = false;
        StoryInteractableControl_Momotaro.isMeet = false;
        StoryInteractableControl_Momotaro.isSuccessfulPerformance = false;
        StoryInteractableControl_Momotaro.isAnswerCorrect = false;
        StoryInteractableControl_Momotaro.isBackLake = false;
    }

    void Update()
    {
        ExitActive();
        StatueActive();
        NpcActive();
        MouseCursor();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StoryUIControl_Momotaro.isStoryEnding = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                StoryStrongWind_Momotaro._respawnNum = 3;
                StoryStrongWind_Momotaro.isBlownAway = true;
                StoryStrongWind_Momotaro.isFirstBlown = false;
            }
        }
    }

    void ExitActive()
    {
        forest.SetActive(isForestActive);
        mountain.SetActive(isMountainActive);
        plaza.SetActive(isPlazaActive);

        if (isMountainActive && isMeetDonkey && isOnce)
        {
            isOnce = false;
            isPlazaActive = true;
            StoryUIControl_Momotaro.isDialogue = true;
            StoryDialogueControl_Momotaro._textCount = 3;
        }
    }
    void StatueActive()
    {
        catLow.SetActive(!StoryInteractableControl_Momotaro.isFinishWork);
        catFinish.SetActive(StoryInteractableControl_Momotaro.isFinishWork);
        performancesPoint.SetActive(isPerformancesPointActive);
    }
    void NpcActive()
    {
        cat.enabled = scenePlaza.activeSelf;
        parrot.SetActive(isParrotActive);

        if (!StoryInteractableControl_Momotaro.isSpecialEnding)
            momotaro.enabled = sceneRiver.activeSelf;
        else
            momotaro.enabled = true;

        if (StoryNpcAnimator_Momotaro._dating == 0)
            donkey.SetActive(sceneForest.activeSelf);
        else
            donkey.SetActive(true);
    }
    void MouseCursor()
    {
        if (isClick)
        {
            Cursor.SetCursor(mouse2, hotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(mouse1, hotSpot, CursorMode.Auto);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            Invoke("FalseisClick", 0.5f);
        }
    }
    void FalseisClick()
    {
        isClick = false;
    }
}
