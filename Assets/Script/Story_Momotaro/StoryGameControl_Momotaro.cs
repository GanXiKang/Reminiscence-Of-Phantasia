using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public SpriteRenderer donkey;
    public GameObject parrot;
    public static bool isParrotActive;

    [Header("Texture")]
    public Texture2D[] mouse;
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

        StoryStrongWind_Momotaro._respawnNum = 0;
        StoryStrongWind_Momotaro.isBlownAway = false;
        StoryStrongWind_Momotaro.isFirstBlown = true;

        StoryUIControl_Momotaro.isDialogue = false;
        StoryUIControl_Momotaro.isPerformances = false;
        StoryUIControl_Momotaro.isStoryStart = true;
        StoryUIControl_Momotaro.isStoryEnding = false;

        StoryRiceDumpling_Momotaro.isSkillActive = false;
        StoryRiceDumpling_Momotaro.isChangeRoles = false;
        StoryRiceDumpling_Momotaro.isChangeRolePlot = false;
        StoryRiceDumpling_Momotaro.isEat = false;
        StoryRiceDumpling_Momotaro._whoEatGoldRice = 0;

        StoryLoadingScene_Momotaro.isLoading = false;
        StoryLoadingScene_Momotaro.isOpen = false;
        StoryLoadingScene_Momotaro.isFirstGoPlaza = false;
        StoryLoadingScene_Momotaro.isPlotAnimator = false;
        StoryLoadingScene_Momotaro.isHintGoPlaza = false;

        StoryPlayerAnimator_Momotaro.isHuman = false;
        StoryPlayerAnimator_Momotaro.isDonkey = false;
        StoryPlayerAnimator_Momotaro.isRaccoon = false;
        StoryPlayerAnimator_Momotaro.isStone = false;
        StoryPlayerAnimator_Momotaro.isParrot = false;
        StoryPlayerAnimator_Momotaro.isFall = false;
        StoryPlayerAnimator_Momotaro.isSmokeEF = false;
        StoryPlayerAnimator_Momotaro.isMistEF = false;
        StoryPlayerAnimator_Momotaro.isNervousEF = false;

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

        StoryNpcAnimator_Momotaro.isHappy_Momo = false;
        StoryNpcAnimator_Momotaro.isSad_Momo = true;
        StoryNpcAnimator_Momotaro.isWalk_Momo = false;
        StoryNpcAnimator_Momotaro._movePlot = 0;
        StoryNpcAnimator_Momotaro.isAngry = false;
        StoryNpcAnimator_Momotaro.isOutLake = false;
        StoryNpcAnimator_Momotaro.isBackLake = false;
        StoryNpcAnimator_Momotaro.isShy = false;
        StoryNpcAnimator_Momotaro.isGift = false;
        StoryNpcAnimator_Momotaro.isWalk_Monkey = false;
        StoryNpcAnimator_Momotaro.isWalkGold_Monkey = false;
        StoryNpcAnimator_Momotaro.isGold_Monkey = false;
        StoryNpcAnimator_Momotaro.isControlled_Monkey = false;
        StoryNpcAnimator_Momotaro.isGoMountain = false;
        StoryNpcAnimator_Momotaro.isLeave_Monkey = false;
        StoryNpcAnimator_Momotaro.isFindPlayer = false;
        StoryNpcAnimator_Momotaro.isFindMomotaro_Monkey = false;
        StoryNpcAnimator_Momotaro.isMeet = false;
        StoryNpcAnimator_Momotaro.isCloseEyes = false;
        StoryNpcAnimator_Momotaro._dating = 0;
        StoryNpcAnimator_Momotaro.isStone = false;
        StoryNpcAnimator_Momotaro._performancesNum = 0;
        StoryNpcAnimator_Momotaro.isFindMomotaro = false;
        StoryNpcAnimator_Momotaro.isSliver_Dog = false;
        StoryNpcAnimator_Momotaro.isControlled_Dog = false;
        StoryNpcAnimator_Momotaro.isGold_Chicken = false;
        StoryNpcAnimator_Momotaro.isControlled_Chicken = false;
        StoryNpcAnimator_Momotaro.isWalk_Parrot = false;
        StoryNpcAnimator_Momotaro.isPerformance = false;
        StoryNpcAnimator_Momotaro.isExcited = false;
        StoryNpcAnimator_Momotaro.isLeave_Parrot = false;
        StoryNpcAnimator_Momotaro.isOutLake_GSMomo = false;
        StoryNpcAnimator_Momotaro.isGoTarget_GSMomo = false;
        StoryNpcAnimator_Momotaro.isBackLake_GSMomo = false;
        StoryNpcAnimator_Momotaro.isWalk_GoldMomo = false;
        StoryNpcAnimator_Momotaro.isWalk_SliverMomo = false;
        StoryNpcAnimator_Momotaro.isFinishPerformances_SliverMomo = false;
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
                Showcase_House.isSpecialEnd[2] = true;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                GameControl_House._day = 3;
                GameControl_House._storyNum = 3;
                SceneManager.LoadScene(4);
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
            donkey.enabled = sceneForest.activeSelf;
        else
            donkey.enabled = scenePlaza.activeSelf;
    }
    void MouseCursor()
    {
        if (StoryBagControl.isItemFollow)
        {
            Cursor.SetCursor(mouse[2], hotSpot, CursorMode.Auto);
        }
        else
        {
            if (isClick)
                Cursor.SetCursor(mouse[1], hotSpot, CursorMode.Auto);
            else
                Cursor.SetCursor(mouse[0], hotSpot, CursorMode.Auto);
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
