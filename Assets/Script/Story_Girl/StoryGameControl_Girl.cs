using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("TeachWall")]
    public GameObject teachWall;
    public GameObject mistLeft;
    public GameObject mistRight;
    public static bool isWallActive;
    bool isMistEffects = true;

    [Header("GarbageCan")]
    public GameObject trashcanLid;
    public static bool isTrashcanLidActice;

    [Header("Resurrection")]
    public Transform streetPoint;
    public Transform forestPoint;
    public static bool isInStreet;
    public static bool isResurrection;
    public static bool isRenewTemperature;
    StoryPlayerControl playerControl;

    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<StoryPlayerControl>();

        GameStatic();
    }

    void GameStatic()
    {
        isWallActive = true;
        isTrashcanLidActice = true;
        isInStreet = true;
        isResurrection = false;
        isRenewTemperature = false;

        StoryPileWood_Girl.isFireActice = false;

        StoryExitControl_Girl.isExit = false;

        StoryItemIntroduce_Girl.isIntroduce = true;

        StoryUIControl_Girl.isDialogue = false;
        StoryUIControl_Girl.isStoryStart = true;
        StoryUIControl_Girl.isStoryEnding = false;

        StoryPlayerAnimator_Girl.isCold = false;
        StoryPlayerAnimator_Girl.isIronRod = false;
        StoryPlayerAnimator_Girl.isMatch = false;

        StoryLoadingScene_Girl.isLoading = false;
        StoryLoadingScene_Girl.isLeftOpen = false;
        StoryLoadingScene_Girl.isLeftClose = false;
        StoryLoadingScene_Girl.isRightOpen = false;
        StoryLoadingScene_Girl.isRightClose = false;

        StoryThermometerControl_Girl.isThermometer = false;
        StoryThermometerControl_Girl.isStepOnSnow = false;
        StoryThermometerControl_Girl.isFireBeside = false;
        StoryThermometerControl_Girl.isDead = false;
        StoryThermometerControl_Girl.isSkillActive = false;
        StoryThermometerControl_Girl._matchQuantity = 0;

        StoryInteractableControl_Girl.isInteractableUI = false;
        StoryInteractableControl_Girl.isGiveItem = false;
        StoryInteractableControl_Girl.isBagGetItem = false;
        StoryInteractableControl_Girl.isPlayerMove = true;
        StoryInteractableControl_Girl.isFinallyMatch = false;
        StoryInteractableControl_Girl.isWearingLittleRedHood = false;
        StoryInteractableControl_Girl.isTrashCanLid = false;
        StoryInteractableControl_Girl.isNeedHelp = false;
        StoryInteractableControl_Girl.isGetGift = false;
        StoryInteractableControl_Girl.isCanKillWolf = false;
        StoryInteractableControl_Girl.isFirstAskFind = false;
        StoryInteractableControl_Girl.isAgreeFind = false;

        StoryNpcAnimator_Girl._direction = 1;
        StoryNpcAnimator_Girl.isHappy_Irls = false;
        StoryNpcAnimator_Girl.isSurprise = false;
        StoryNpcAnimator_Girl.isLeaveStreet = false;
        StoryNpcAnimator_Girl.isGoGrandmom = false;
        StoryNpcAnimator_Girl.isHide = false;
        StoryNpcAnimator_Girl.isFind = false;
        StoryNpcAnimator_Girl.isAttractWolf = false;
        StoryNpcAnimator_Girl.isOpen = false;
        StoryNpcAnimator_Girl.isAngry = false;
        StoryNpcAnimator_Girl.isLeave = false;
        StoryNpcAnimator_Girl.isHappy_Cri = false;
        StoryNpcAnimator_Girl.isMoveSeeWolf = false;
        StoryNpcAnimator_Girl.isFinishLeave = false;
        StoryNpcAnimator_Girl.isNormal = false;
        StoryNpcAnimator_Girl.isScared = false;
        StoryNpcAnimator_Girl.isShotRunAway = false;
        StoryNpcAnimator_Girl.isFightRunAway = false;
    }

    void Update()
    {
        trashcanLid.SetActive(isTrashcanLidActice);

        TeachWall();
        PlayerResurrection();
        MouseCursor();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StoryUIControl_Girl.isStoryEnding = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6)) 
            {
                isInStreet = true;
                StoryLoadingScene_Girl.isLeftOpen = true;
                StartCoroutine(ResurrectionState());
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                isInStreet = false;
                StoryLoadingScene_Girl.isRightOpen = true;
                StartCoroutine(ResurrectionState());
            }
        }
    }

    void TeachWall()
    {
        teachWall.SetActive(isWallActive);

        if (isWallActive) return;
        if (!isMistEffects) return;

        Invoke("Mist", 5f);
        mistLeft.transform.Translate(0f, 0.05f, 0f);
        mistRight.transform.Translate(0f, -0.05f, 0f);
    }
    void Mist()
    {
        isMistEffects = false;
        Destroy(mistLeft);
        Destroy(mistRight);
    }
    void PlayerResurrection()
    {
        if (!isResurrection) return;

        isResurrection = false;
        StartCoroutine(ResurrectionState());
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

    IEnumerator ResurrectionState()
    {
        playerControl.enabled = false;
        yield return new WaitForSeconds(0.7f);
        if (isInStreet)
        {
            player.transform.position = streetPoint.position;
        }
        else
        {
            player.transform.position = forestPoint.position;
        }
        isRenewTemperature = true;
        StoryThermometerControl_Girl.isDead = false;
        if (StoryThermometerControl_Girl._matchQuantity < 10)
        {
            StoryThermometerControl_Girl._matchQuantity = 10;
        }
        yield return new WaitForSeconds(0.5f);
        playerControl.enabled = true;
    }
}
