﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteractableControl_Prince : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip pickUp, get;

    [Header("InteractableDistance")]
    public float _snapDistance = 12f;
    public float _scaleSpeed = 20f;
    private Coroutine currentCoroutine;
    private Vector3 originalScale;
    Vector3 scaledSize = Vector3.one;

    [Header("InteractableUI")]
    public GameObject interactableUI;
    public Text interactableName;
    public int _who;
    public static int _aboveWho = 0;
    public static bool isInteractableUI = false;
    bool isInteractable = false;
    int _countMouseDown;

    [Header("Item_Give")]
    public bool isGive;
    public int[] _giveItemNumber;
    public static bool isGiveItem = false;
    public static int _whoGive;
    public static int _whoGiveNumber;

    [Header("Item_Get")]
    public bool isGet;
    public int[] _getItemNumber;
    public static bool isBagGetItem = false;
    bool isGetItem = false;

    [Header("Item_Exchange")]
    public bool isExchange;
    public int[] _exchangeItemNumber;
    int _exchangeDifferentItemRecord;
    bool isExchangeItem = false;

    [Header("ItemPickUp")]
    public GameObject moveItemUI;
    public Transform bagUIPosition;
    public static bool isPlayerMove = true;
    bool isPickedUp = false;
    bool isAnim = false;

    [Header("SkillPickUp")]
    public Transform skillUIPosition;
    bool isSkill = false;

    [Header("Effects")]
    public GameObject effects;

    //01Ghost
    public static bool isReallyGoFutureGood = false;
    public static bool isGoFutureGood = false;
    public static int _askResident = 0;
    bool isReallyGoOnce = true;
    bool isGoOnce = true;
    //02Prince
    public static bool isPlotBanMove = false;
    public static bool isCanHelpPrince = false;
    public static int _helpChildQian = 0;
    public static bool isPrinceNoDie = false;
    public static bool isGiveSuppliesBox = false;
    public static bool isPrinceInNow = false;
    public static bool isSwallowHunger = false;
    public static int _swallowHunger = 0;
    public static bool isCrownGiveQian = false;
    //03PrinceStatue
    public static bool isSwallowFindPrince = false;
    public static bool isKangNeedGem = false;
    public static bool isTakeGem = false;
    //06Qian_Adult
    bool isHalfGemMerge = false;
    bool isGiveRope = false;
    bool isGoodFutureFirstMeet = true;
    //09Jun & 11Bei
    bool isHelpJun = false;
    bool isExchangeTomato = false;
    public static bool isDoubleButter = false;
    //12Bei
    public static bool isNeedSauce = false;
    public static bool isGetTomato = false;
    //14Plank
    public static bool isGivePlank = false;
    //15Garden
    public static bool isPlantWrong = false;
    // Resident
    bool isResidentOnce = true;
    bool isResidentTwo = true;


    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;

        if (_who == 15)
        {
            isInteractable = true;
        }
    }

    void Update()
    {
        PickUpItem();
        InteractableUI();
        GivePlayerObject();
        ExchangeItem();
    }

    void PickUpItem()
    {
        if (!isPickedUp || isAnim) return;

        isPlayerMove = false;
        player.GetComponent<CharacterController>().Move(Vector3.zero);

        moveItemUI.SetActive(true);
        Vector3 startPosition = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0f, 120f, 0f);
        moveItemUI.transform.position = startPosition;
        if (!isSkill)
        {
            StartCoroutine(MoveItemUI(moveItemUI, startPosition, bagUIPosition.position));
        }
        else
        {
            StartCoroutine(MoveItemUI(moveItemUI, startPosition, skillUIPosition.position));
        }
    }
    void InteractableUI()
    {
        interactableUI.SetActive(isInteractableUI);

        if (StoryUIControl_Prince.isDialogue)
            isInteractableUI = false;

        if (!isInteractableUI) return;

        switch (_who)
        {
            case 1:
                if (_aboveWho == _who)
                {
                    interactableName.text = "幽靈";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 2:
                if (_aboveWho == _who)
                {
                    interactableName.text = "善願王子";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 3:
                if (_aboveWho == _who)
                {
                    interactableName.text = "王子雕像";
                    Vector3 worldPos = transform.position + new Vector3(0f, 12f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 4:
                if (_aboveWho == _who)
                {
                    if (isSwallowFindPrince)
                        interactableName.text = "？";
                    else
                        interactableName.text = "姿裴絲";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 5:
                if (_aboveWho == _who)
                {
                    interactableName.text = "千千";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 6:
                if (_aboveWho == _who)
                {
                    interactableName.text = "千千";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 7:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小康";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 8:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小彥";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 9:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小俊";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 10:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小欣";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 11:
                if (_aboveWho == _who)
                {
                    interactableName.text = "薇普";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 12:
                if (_aboveWho == _who)
                {
                    interactableName.text = "貝蘿";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 14:
                if (_aboveWho == _who)
                {
                    interactableName.text = "木板";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 15:
                if (_aboveWho == _who)
                {
                    interactableName.text = "樹圃";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 16:
                if (_aboveWho == _who)
                {
                    interactableName.text = "損壞雕像";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 17:
                if (_aboveWho == _who)
                {
                    interactableName.text = "吉恩";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 18:
                if (_aboveWho == _who)
                {
                    interactableName.text = "阿久";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 19:
                if (_aboveWho == _who)
                {
                    interactableName.text = "米菈";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 20:
                if (_aboveWho == _who)
                {
                    interactableName.text = "凱伊";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 21:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小鈴";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 22:
                if (_aboveWho == _who)
                {
                    interactableName.text = "陸歐";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 23:
                if (_aboveWho == _who)
                {
                    interactableName.text = "妮可";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 24:
                if (_aboveWho == _who)
                {
                    interactableName.text = "姿裴絲墓";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;
        }
    }
    void GivePlayerObject()
    {
        if (isGiveItem)
        {
            if (_whoGive == _who)
            {
                isGiveItem = false;
                switch (_who)
                {
                    case 1:
                        switch (_whoGiveNumber)
                        {
                            case 0:
                            case 1:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_giveItemNumber[_whoGiveNumber]] = true;
                                StoryBagControl._whichItem = _giveItemNumber[_whoGiveNumber];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;

                            case 2:
                                isPickedUp = true;
                                isSkill = true;
                                StoryBagControl._whichItem = _giveItemNumber[_whoGiveNumber];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;
                        }
                        break;

                    case 2:
                        switch (_whoGiveNumber)
                        {
                            case 0:
                            case 1:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_giveItemNumber[_whoGiveNumber]] = true;
                                StoryBagControl._whichItem = _giveItemNumber[_whoGiveNumber];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;
                        }
                        break;

                    case 6:
                        switch (_whoGiveNumber)
                        {
                            case 0:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_exchangeItemNumber[_whoGiveNumber]] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[_whoGiveNumber];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;

                            case 1:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                                StoryBagControl._whichItem = _giveItemNumber[0];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;
                        }
                        break;

                    case 7:
                        switch (_whoGiveNumber)
                        {
                            case 0:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_exchangeItemNumber[_whoGiveNumber]] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[_whoGiveNumber];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;

                            case 1:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_exchangeItemNumber[_whoGiveNumber]] = true;
                                StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                                StoryBagControl.isItemNumber[_giveItemNumber[1]] = true;
                                StoryBagControl._whichItem = _giveItemNumber[2];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;
                        }
                        break;

                    case 9:
                    case 11:
                        isPickedUp = true;
                        StoryBagControl.isGet = true;
                        StoryBagControl.isItemNumber[_exchangeItemNumber[1]] = true;
                        StoryBagControl._whichItem = _exchangeItemNumber[1];
                        if (isDoubleButter)
                        {
                            isDoubleButter = false;
                            StoryBagControl.isItemNumber[_exchangeItemNumber[0]] = true;
                        }
                        if (StoryBagControl.isOpenBag)
                        {
                            StoryBagControl.isOpenBag = false;
                        }
                        break;

                    case 4:
                    case 18:
                    case 19:
                    case 20:
                        switch (_whoGiveNumber)
                        {
                            case 0:
                                isPickedUp = true;
                                isSkill = true;
                                StoryBagControl._whichItem = _giveItemNumber[_whoGiveNumber];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
    void ExchangeItem()
    {
        if (!isExchange) return;
        if (!isExchangeItem) return;

        isExchangeItem = false;
        isPickedUp = true;
        StoryBagControl.isGet = true;
        StoryBagControl.isItemNumber[_exchangeItemNumber[_exchangeDifferentItemRecord]] = true;
        StoryBagControl._whichItem = _exchangeItemNumber[_exchangeDifferentItemRecord];
        if (StoryBagControl.isOpenBag)
        {
            StoryBagControl.isOpenBag = false;
        }
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;
        if (StoryUIControl_Prince.isDialogue) return;

        if (isGive)
        {
            switch (_who)
            {
                case 1:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 4;
                            break;

                        default:
                            if (isReallyGoFutureGood)
                            {
                                if (isReallyGoOnce)
                                {
                                    isReallyGoOnce = false;
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 57;
                                }
                                else
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 58;
                                }
                            }
                            if (isGoFutureGood)
                            {
                                if (isGoOnce)
                                {
                                    isGoOnce = false;
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 59;
                                }
                                else
                                {
                                    if (_askResident < 4)
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._textCount = 60;
                                    }
                                    else
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._textCount = 66;
                                    }
                                }
                            }
                            break;
                    }
                    break;

                case 2:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 10;
                            break;

                        default:
                            if (!isGoFutureGood)
                            {
                                if (!isCanHelpPrince)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 11;
                                }
                                switch (_helpChildQian)
                                {
                                    case 1:
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._isAboveWho2 = 13;
                                        StoryDialogueControl_Prince._textCount = 23;
                                        break;

                                    case 2:
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._textCount = 24;
                                        break;

                                    case 7:
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._textCount = 31;
                                        break;
                                }
                                if (isSwallowHunger)
                                {
                                    if (!StoryGardenControl_Prince.isCherryTree && !StoryGardenControl_Prince.isGrapeTree)
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._isAboveWho2 = 4;
                                        StoryDialogueControl_Prince._textCount = 33;
                                    }
                                    else if (StoryGardenControl_Prince.isCherryTree)
                                    {
                                        isPlantWrong = true;
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._isAboveWho2 = 4;
                                        StoryDialogueControl_Prince._textCount = 36;
                                    }
                                    else if (StoryGardenControl_Prince.isGrapeTree)
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._isAboveWho2 = 4;
                                        StoryDialogueControl_Prince._textCount = 38;
                                    }
                                }
                                if (isGiveSuppliesBox)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 39;
                                }
                            }
                            else
                            {
                                if (!isCrownGiveQian)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._isAboveWho2 = 4;
                                    StoryDialogueControl_Prince._textCount = 68;
                                }
                                else
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._isAboveWho2 = 6;
                                    StoryDialogueControl_Prince._textCount = 71;
                                }
                            }
                            break;
                    }
                    break;

                case 6:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 19;
                            break;

                        default:
                            if (!StoryGameControl_Prince.isFutureGood)
                            {
                                if (!isHalfGemMerge)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 20;
                                }
                                else if (_helpChildQian == 4 && !isGiveRope)
                                {
                                    isGiveRope = true;
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 27;
                                }
                                else
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 88;
                                }
                            }
                            else
                            {
                                if (isGoodFutureFirstMeet)
                                {
                                    isGoodFutureFirstMeet = false;
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 64;
                                }
                                else
                                {
                                    if (!isCrownGiveQian)
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                                        StoryDialogueControl_Prince._textCount = 65;
                                    }
                                    else
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._isAboveWho1 = 2;
                                        StoryDialogueControl_Prince._isAboveWho2 = _who;
                                        StoryDialogueControl_Prince._textCount = 71;
                                    }
                                }
                            }
                            break;
                    }
                    break;

                case 7:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            isKangNeedGem = true;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 12;
                            break;

                        default:
                            if (!StoryGameControl_Prince.isFutureGood)
                            {
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 14;
                            }
                            else
                            {
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 91;
                            }
                            break;
                    }
                    break;

                case 14:
                    isGivePlank = true;
                    StoryUIControl_Prince.isDialogue = true;
                    StoryDialogueControl_Prince._textCount = 26;

                    isPickedUp = true;
                    BGM.PlayOneShot(pickUp);
                    StoryBagControl.isGet = true;
                    StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                    StoryBagControl._whichItem = _giveItemNumber[0];
                    if (StoryBagControl.isOpenBag)
                    {
                        StoryBagControl.isOpenBag = false;
                    }

                    isInteractableUI = false;
                    effects.SetActive(false);
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(this.gameObject, 2f);
                    break;

                case 18:
                    if (!isPrinceNoDie && isResidentOnce)
                    {
                        isResidentOnce = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 81;
                    }
                    else if (isPrinceNoDie && isResidentTwo)
                    {
                        isResidentTwo = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 84;
                    }
                    break;

                case 19:
                    if (!isPrinceNoDie && isResidentOnce)
                    {
                        isResidentOnce = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 82;
                    }
                    else if (isPrinceNoDie && isResidentTwo)
                    {
                        isResidentTwo = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 85;
                    }
                    break;

                case 20:
                    if (!isPrinceNoDie && isResidentOnce)
                    {
                        isResidentOnce = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 83;
                    }
                    else if (isPrinceNoDie && isResidentTwo)
                    {
                        isResidentTwo = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 86;
                    }
                    break;
            }
        }
        else
        {
            switch (_who)
            {
                case 3:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 2;
                            break;

                        case 2:
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = 1;
                            StoryDialogueControl_Prince._textCount = 3;
                            break;

                        default:
                            if (isSwallowFindPrince)
                            {
                                isSwallowFindPrince = false;
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._isAboveWho2 = 4;
                                StoryDialogueControl_Prince._textCount = 7;
                            }
                            if (isKangNeedGem)
                            {
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._isAboveWho2 = 1;
                                StoryDialogueControl_Prince._textCount = 15;
                            }
                            break;
                    }
                    break;

                case 5:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = 2;
                            StoryDialogueControl_Prince._textCount = 25;
                            break;
                    }
                    break;

                case 8:
                    if (!StoryGameControl_Prince.isFutureGood)
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 75;
                    }
                    else
                    {
                        if (isResidentOnce)
                        {
                            isResidentOnce = false;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 62;
                        }
                    }
                    break;

                case 9:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 47;
                            break;

                        default:
                            if (!StoryGameControl_Prince.isFutureGood)
                            {
                                if (!isHelpJun)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 48;
                                }
                                else
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 50;
                                }
                            }
                            else
                            {
                                if (isResidentOnce)
                                {
                                    isResidentOnce = false;
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 61;
                                }
                            }
                            break;
                    }
                    break;

                case 10:
                    if (!StoryGameControl_Prince.isFutureGood)
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 76;
                    }
                    else
                    {
                        if (isResidentOnce)
                        {
                            isResidentOnce = false;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 63;
                        }
                    }
                    break;

                case 11:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 44;
                            break;

                        default:
                            if (!isExchangeTomato)
                            {
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 45;
                            }
                            else
                            {
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 87;
                            }
                            break;
                    }
                    break;

                case 12:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = _who;
                            StoryDialogueControl_Prince._textCount = 42;
                            break;

                        default:
                            if (!isGetTomato)
                            {
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 43;
                            }
                            else
                            {
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 55;
                            }
                            break;
                    }
                    break;

                case 15:
                    if (isSwallowHunger && StoryBagControl.isItemNumber[12] && StoryBagControl.isItemNumber[13])
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 34;
                    }
                    break;

                case 16:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            StoryUIControl_Prince.isDialogue = true;
                            StoryDialogueControl_Prince._isAboveWho1 = 1;
                            StoryDialogueControl_Prince._textCount = 6;
                            break;
                    }
                    break;

                case 17:
                    StoryUIControl_Prince.isDialogue = true;
                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                    StoryDialogueControl_Prince._textCount = 77;
                    break;

                case 21:
                    if (!isPrinceNoDie)
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 72;
                    }
                    else
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 78;
                    }
                    break;

                case 22:
                    if (!isPrinceNoDie)
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 73;
                    }
                    else
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 79;
                    }
                    break;

                case 23:
                    if (!isPrinceNoDie)
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 74;
                    }
                    else
                    {
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = _who;
                        StoryDialogueControl_Prince._textCount = 80;
                    }
                    break;
            }
        }
        StoryBagControl.isOpenBag = false;
    }
    void OnMouseEnter()
    {
        if (SettingControl.isSettingActive) return;
        if (StoryLoadingScene_Prince.isLoading) return;
        if (StorySkillControl_Prince.isClockActice) return;
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        if (_who != 3 && _who != 15 && _who != 16 && _who != 24)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ScaleObject(scaledSize));
        }
        if (!StoryUIControl_Prince.isDialogue)
        {
            isInteractableUI = true;
        }
        _aboveWho = _who;

        if (!isGet) return;
        if (!isBagGetItem) return;
        if (isGetItem) return;
        if (!isInteractable) return;
        if (!StoryBagControl.isOpenBag) return;
        if (StoryUIControl_Prince.isDialogue) return;

        for (int i = 0; i < _getItemNumber.Length; i++)
        {
            if (_getItemNumber[i] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
            {
                isBagGetItem = false;
                StoryBagControl.isOpenBag = false;
                StoryBagControl.isItemNumber[_getItemNumber[i]] = false;
                StoryBagControl._howManyGrids--;
                StoryBagControl.isRenewBag = true;
                StoryItemIntroduce_Prince.isIntroduce = true;

                switch (_who)
                {
                    case 2:
                        switch (i)
                        {
                            case 0:
                                isGiveSuppliesBox = false;
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 40;
                                break;

                            case 1:
                                if (_helpChildQian == 5)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._isAboveWho2 = 5;
                                    StoryDialogueControl_Prince._textCount = 30;
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 2:
                                if (_helpChildQian == 4)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 29;
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 3:
                                if (_helpChildQian == 4)
                                {
                                    StoryUIControl_Prince.isDialogue = true;
                                    StoryDialogueControl_Prince._isAboveWho1 = _who;
                                    StoryDialogueControl_Prince._textCount = 28;
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 4:
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._isAboveWho2 = 4;
                                StoryDialogueControl_Prince._textCount = 69;
                                break;
                        }
                        break;

                    case 6:
                        switch (i)
                        {
                            case 0:
                                isHalfGemMerge = true;
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 21;
                                break;

                            case 1:
                                isCrownGiveQian = true;
                                Showcase_House.isSpecialEnd[3] = true;
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 70;
                                break;
                        }
                        break;

                    case 7:
                        switch (i)
                        {
                            case 0:
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 17;
                                break;

                            case 1:
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 22;
                                break;
                        }
                        break;

                    case 9:
                        switch (i)
                        {
                            case 0:
                            case 1:
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 49;
                                isHelpJun = true;
                                if (i == 1)
                                    isDoubleButter = true;
                                break;
                        }
                        break;

                    case 11:
                        switch (i)
                        {
                            case 0:
                            case 1:
                                isExchangeTomato = true;
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 46;
                                if (i == 1)
                                    isDoubleButter = true;
                                break;
                        }
                        break;

                    case 12:
                        switch (i)
                        {
                            case 0:
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 56;
                                break;

                            case 1:
                                isGetTomato = true;
                                StoryUIControl_Prince.isDialogue = true;
                                StoryDialogueControl_Prince._isAboveWho1 = _who;
                                StoryDialogueControl_Prince._textCount = 54;
                                break;
                        }
                        break;

                    case 15:
                        switch (i)
                        {
                            case 0:
                                if (isSwallowHunger)
                                {
                                    if (!StoryGardenControl_Prince.isCherryTree)
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._textCount = 35;
                                        StoryGardenControl_Prince.isPlanting = true;
                                        StoryGardenControl_Prince.isGrapeTree = true;
                                    }
                                    else if (StoryGardenControl_Prince.isCherryTree && isPlantWrong)
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._textCount = 37;
                                        StoryGardenControl_Prince.isPlanting = true;
                                        StoryGardenControl_Prince.isGrapeTree = true;
                                        StoryGardenControl_Prince.isCherryTree = false;
                                    }
                                    else
                                    {
                                        StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                        StoryBagControl._howManyGrids++;
                                    }
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 1:
                                if (isSwallowHunger)
                                {
                                    if (!StoryGardenControl_Prince.isGrapeTree)
                                    {
                                        StoryUIControl_Prince.isDialogue = true;
                                        StoryDialogueControl_Prince._textCount = 90;
                                        StoryGardenControl_Prince.isPlanting = true;
                                        StoryGardenControl_Prince.isCherryTree = true;
                                    }
                                    else
                                    {
                                        StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                        StoryBagControl._howManyGrids++;
                                    }
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;
                        }
                        break;
                }

                if (!StoryBagControl.isItemNumber[_getItemNumber[i]])
                {
                    BGM.PlayOneShot(get);
                }
            }
        }
    }
    void OnMouseExit()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleObject(originalScale));
        isInteractableUI = false;
    }

    IEnumerator ScaleObject(Vector3 targetScale)
    {
        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, _scaleSpeed * Time.deltaTime);
            yield return null;
        }
        currentCoroutine = null;
    }
    IEnumerator MoveItemUI(GameObject itemUI, Vector3 start, Vector3 end)
    {
        isAnim = true;
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            itemUI.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        moveItemUI.transform.position = end;
        itemUI.SetActive(false);
        if (isSkill)
        {
            StoryUIControl_Prince.isSkillActive = true;
            isSkill = false;
        }
        isPickedUp = false;
        isAnim = false;
        isPlayerMove = true;
    }
}
