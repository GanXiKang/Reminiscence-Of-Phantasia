using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryGameControl_Prince : MonoBehaviour
{
    [Header("Texture")]
    public Texture2D[] mouse;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    [Header("Plank")]
    public GameObject plank;

    [Header("Npc")]
    public GameObject[] npc;
    public GameObject[] npcBC;
    public static bool isPlotNpcActive = false;

    [Header("Scene")]
    public GameObject now;
    public GameObject past;
    public GameObject future;

    [Header("SuppliesGame")]
    public GameObject supplies_Now;
    public GameObject supplies_Past;
    public static bool isSuppliesGameEasy = false;
    public static bool isSuppliesGameHard = false;
    public static bool isPassGameEasy = false;
    public static bool isPassGameHard = false;

    [Header("WoodFence")]
    public GameObject woodFence;
    public GameObject woodFence_Bad;

    [Header("PrinceState_Now")]
    public GameObject princeStatue;
    public GameObject brokenPrinceStatue;
    public GameObject notPrinceStatue;
    public GameObject smokEF;
    public static bool isBroken = false;

    [Header("SceneFuture")]
    public GameObject futureBad;
    public GameObject futureGood;
    public static bool isFutureGood = false;

    [Header("SceneSound")]
    public AudioSource[] fountain;
    public AudioSource[] river;

    void Start()
    {
        PlotNpcActive();
        GameStatic();
    }

    void GameStatic()
    {
        isPlotNpcActive = false;
        isSuppliesGameEasy = false;
        isSuppliesGameHard = false;
        isPassGameEasy = false;
        isPassGameHard = false;
        isBroken = false;
        isFutureGood = false;

        StoryGardenControl_Prince.isPlanting = false;
        StoryGardenControl_Prince.isDigPlant = false;
        StoryGardenControl_Prince.isCherryTree = false;
        StoryGardenControl_Prince.isGrapeTree = false;

        StoryGhostControl_Prince.isWatchSkill = false;
        StoryGhostControl_Prince.isSmile = false;
        StoryGhostControl_Prince.isWarp = false;
        StoryGhostControl_Prince.isNoGem = false;
        StoryGhostControl_Prince.isDisappear = false;
        StoryGhostControl_Prince.isAscend = false;

        StoryInteractableControl_Prince.isInteractableUI = false;
        StoryInteractableControl_Prince.isGiveItem = false;
        StoryInteractableControl_Prince.isBagGetItem = false;
        StoryInteractableControl_Prince.isPlayerMove = true;
        StoryInteractableControl_Prince.isReallyGoFutureGood = false;
        StoryInteractableControl_Prince.isGoFutureGood = false;
        StoryInteractableControl_Prince._askResident = 0;
        StoryInteractableControl_Prince.isPlotBanMove = false;
        StoryInteractableControl_Prince.isCanHelpPrince = false;
        StoryInteractableControl_Prince._helpChildQian = 0;
        StoryInteractableControl_Prince.isPrinceNoDie = false;
        StoryInteractableControl_Prince.isGiveSuppliesBox = false;
        StoryInteractableControl_Prince.isPrinceInNow = false;
        StoryInteractableControl_Prince.isSwallowHunger = false;
        StoryInteractableControl_Prince._swallowHunger = 0;
        StoryInteractableControl_Prince.isCrownGiveQian = false;
        StoryInteractableControl_Prince.isSwallowFindPrince = false;
        StoryInteractableControl_Prince.isKangNeedGem = false;
        StoryInteractableControl_Prince.isTakeGem = false;
        StoryInteractableControl_Prince.isDoubleButter = false;
        StoryInteractableControl_Prince.isNeedSauce = false;
        StoryInteractableControl_Prince.isGetTomato = false;
        StoryInteractableControl_Prince.isGivePlank = false;
        StoryInteractableControl_Prince.isPlantWrong = false;

        StoryLoadingScene_Prince.isNowScene = true;
        StoryLoadingScene_Prince.isPastScene = false;
        StoryLoadingScene_Prince.isFutureScene = false;
        StoryLoadingScene_Prince.isLoading = false;
        StoryLoadingScene_Prince.isOpen = false;

        StoryNpcAnimator_Prince.isWalk_Prince = false;
        StoryNpcAnimator_Prince.isLeaveHelp = false;
        StoryNpcAnimator_Prince.isWet = false;
        StoryNpcAnimator_Prince.isSmiling_Prince = false;
        StoryNpcAnimator_Prince.isDrown = false;
        StoryNpcAnimator_Prince.isPrinceAppear = false;
        StoryNpcAnimator_Prince.isSad_Swallow = false;
        StoryNpcAnimator_Prince.isSurprise_Swallow = false;
        StoryNpcAnimator_Prince.isHungry = false;
        StoryNpcAnimator_Prince.isWalk_Swallow = false;
        StoryNpcAnimator_Prince.isSmiling_Swallow = false;
        StoryNpcAnimator_Prince.isFindFood = false;
        StoryNpcAnimator_Prince.isComeBack = false;
        StoryNpcAnimator_Prince.isLeave_Qian = false;
        StoryNpcAnimator_Prince.isShock_Qian = false;
        StoryNpcAnimator_Prince.isNearPrince = false;
        StoryNpcAnimator_Prince.isFindGem = false;
        StoryNpcAnimator_Prince.isLeave_Kang = false;
        StoryNpcAnimator_Prince.isLeave_Bei = false;

        StorySkillControl_Prince.isClockActice = false;
        StorySkillControl_Prince.isDisabledClock = false;

        StoryUIControl_Prince.isStoryStart = true;
        StoryUIControl_Prince.isStoryEnding = false;
        StoryUIControl_Prince.isSkillActive = false;
        StoryUIControl_Prince.isSuppliesActive = false;
    }

    void Update()
    {
        MouseCursor();

        MainNpcActive();
        SuppliesNpcRotation();
        PlotObjectActive();
        PrinceState_Now();
        SceneFuture();
        SceneSound();

        if (isPlotNpcActive)
            PlotNpcActive();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StoryUIControl_Prince.isStoryEnding = true;
            }
        }
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
    
    void MainNpcActive()
    {
        if (StoryInteractableControl_Prince.isPrinceInNow)
        {
            npcBC[0].SetActive(now.activeSelf);
            npc[2].GetComponent<SpriteRenderer>().enabled = now.activeSelf;
        }
        else if (isFutureGood)
        {
            npcBC[0].SetActive(future.activeSelf);
            npc[2].GetComponent<SpriteRenderer>().enabled = future.activeSelf;
        }
        else
        {
            npcBC[0].SetActive(past.activeSelf);
            npc[2].GetComponent<SpriteRenderer>().enabled = past.activeSelf;
        }

        if (StoryInteractableControl_Prince.isGoFutureGood)
        {
            npcBC[3].SetActive(future.activeSelf);
            npc[6].GetComponent<SpriteRenderer>().enabled = future.activeSelf;
        }
        else
        {
            npcBC[3].SetActive(past.activeSelf);
            npc[6].GetComponent<SpriteRenderer>().enabled = past.activeSelf;
        }

        if (isFutureGood)
        {
            npcBC[1].SetActive(future.activeSelf);
            npc[3].GetComponent<SpriteRenderer>().enabled = future.activeSelf;
        }
        else
        {
            npcBC[1].SetActive(now.activeSelf);
            npc[3].GetComponent<SpriteRenderer>().enabled = now.activeSelf;
        }

        npcBC[2].SetActive(past.activeSelf);
        npc[4].GetComponent<SpriteRenderer>().enabled = past.activeSelf;
        npcBC[4].SetActive(now.activeSelf);
        npc[9].GetComponent<SpriteRenderer>().enabled = now.activeSelf;
        npc[7].SetActive(StoryInteractableControl_Prince.isNeedSauce);
        npc[8].SetActive(StoryInteractableControl_Prince.isNeedSauce);
    }
    void SuppliesNpcRotation()
    {
        if (StoryUIControl_Prince.isSuppliesActive)
        {
            npc[2].transform.rotation = Quaternion.Euler(25f, 0f, 0f);
            npc[3].transform.rotation = Quaternion.Euler(25f, 0f, 0f);
        }
        else
        {
            npc[2].transform.rotation = Quaternion.Euler(30f, -45f, 0f);
            npc[3].transform.rotation = Quaternion.Euler(30f, -45f, 0f);
        }
    }
    void PlotObjectActive()
    {
        supplies_Now.SetActive(isSuppliesGameHard && !isPassGameHard && !StoryUIControl_Prince.isSuppliesActive);
        supplies_Past.SetActive(isSuppliesGameEasy && !isPassGameEasy && !StoryUIControl_Prince.isSuppliesActive);

        switch (StoryInteractableControl_Prince._helpChildQian)
        {
            case 2:
                woodFence.SetActive(false);
                woodFence_Bad.SetActive(true);
                break;

            case 4:
                if (!StoryInteractableControl_Prince.isGivePlank)
                    plank.SetActive(true);
                break;

            case 5:
                if(!StoryInteractableControl_Prince.isGivePlank)
                    plank.SetActive(false);
                break;
        }
    }
    void PlotNpcActive()
    {
        for (int i = 2; i < npc.Length; i++)
        {
            npc[i].SetActive(isPlotNpcActive);
        }

        if (isPlotNpcActive)
            isPlotNpcActive = false;
    }
    void PrinceState_Now()
    {
        if (StoryInteractableControl_Prince.isPrinceNoDie)
        {
            princeStatue.SetActive(false);
            notPrinceStatue.SetActive(true);
        }
        else
        {
            if (isBroken)
            {
                smokEF.SetActive(true);
                Invoke("Broken", 0.5f);
            }
            else
            {
                smokEF.SetActive(false);
                princeStatue.SetActive(true);
                brokenPrinceStatue.SetActive(false);
            }
        }
    }
    void Broken()
    {
        princeStatue.SetActive(false);
        brokenPrinceStatue.SetActive(true);
    }
    void SceneFuture()
    {
        futureBad.SetActive(!isFutureGood);
        futureGood.SetActive(isFutureGood);
    }
    void SceneSound()
    {
        fountain[0].volume = SettingControl.volumeBGM;
        fountain[1].volume = SettingControl.volumeBGM;
        fountain[2].volume = SettingControl.volumeBGM;
        river[0].volume = SettingControl.volumeBGM;
        river[1].volume = SettingControl.volumeBGM;
        river[2].volume = SettingControl.volumeBGM;
    }
}
