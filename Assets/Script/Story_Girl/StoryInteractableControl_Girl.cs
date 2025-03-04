using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteractableControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip pickUp, get, find, fight;

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
    public static int _aboveWho;
    public static bool isInteractableUI;
    bool isInteractable = false;
    int _countMouseDown;

    [Header("Item_Give")]
    public bool isGive;
    public int[] _giveItemNumber;
    public static bool isGiveItem;
    public static int _whoGive;

    [Header("Item_Get")]
    public bool isGet;
    public int[] _getItemNumber;
    public static bool isBagGetItem;
    bool isGetItem = false;

    [Header("Item_Exchange")]
    public bool isExchange;
    public int[] _exchangeItemNumber;
    int _exchangeDifferentItemRecord;
    bool isExchangeItem = false;

    [Header("ItemPickUp")]
    public GameObject moveItemUI;
    public Transform bagUIPosition;
    public static bool isPlayerMove;
    bool isPickedUp = false;
    bool isAnim = false;

    [Header("SkillPickUp")]
    public Transform skillUIPosition;
    bool isSkill = false;

    [Header("Effects")]
    public GameObject effects;

    //Rotation
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;
    float _speed = 180f;

    //Npc_01Girl
    int _heldMatchboxesQuantity = 6;
    bool isTeach = false;
    public static bool isFinallyMatch;
    public static bool isWearingLittleRedHood;
    public static bool isTrashCanLid;
    public static bool isNeedHelp;
    //Npc_03SantaClaus
    public static bool isGetGift;
    //Npc_05XiaoXin
    bool isFinishExchangeGift;
    //Npc_06Hunter
    public static bool isCanKillWolf;
    //Npc_09Camping
    bool isGetApple = false;
    bool isGetKebab = false;
    public static bool isFirstAskFind;
    public static bool isAgreeFind;

    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        PickUpItem();
        InteractableUI();
        GivePlayerObject();
        ExchangeItem();
        RotationSprite();
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

        if (StoryUIControl_Girl.isDialogue)
        {
            isInteractableUI = false;
        }
       
        if (!isInteractableUI) return;

        switch (_who)
        {
            case 1:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小女孩";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 2:
                if (_aboveWho == _who)
                {
                    interactableName.text = "垃圾桶";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 3:
                if (_aboveWho == _who)
                {
                    interactableName.text = "聖誕老人";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 4:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小彥";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 5:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小欣";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 6:
                if (_aboveWho == _who)
                {
                    interactableName.text = "獵人";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 7:
                if (_aboveWho == _who)
                {
                    interactableName.text = "鐵棒";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 8:
                if (_aboveWho == _who)
                {
                    interactableName.text = "木材";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 9:
                if (_aboveWho == _who)
                {
                    interactableName.text = "露營者";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 11:
                if (_aboveWho == _who)
                {
                    interactableName.text = "大野狼";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;
        }
    }
    void GivePlayerObject()
    {
        if (StoryUIControl_Girl.isDialogue) return;

        if (isGiveItem)
        {
            if (_whoGive == _who)
            {
                isGiveItem = false;
                switch (_who)
                {
                    case 1:
                        isPickedUp = true;
                        isSkill = true;
                        StoryBagControl._whichItem = _giveItemNumber[0];
                        if (StoryBagControl.isOpenBag)
                        {
                            StoryBagControl.isOpenBag = false;
                        }
                        break;

                    case 2:
                        if (_countMouseDown == 1)
                        {
                            isPickedUp = true;
                            StoryBagControl.isGet = true;
                            StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                            StoryBagControl._whichItem = _giveItemNumber[0];
                            if (StoryBagControl.isOpenBag)
                            {
                                StoryBagControl.isOpenBag = false;
                            }
                        }
                        else
                        {
                            isPickedUp = true;
                            StoryBagControl.isGet = true;
                            StoryBagControl.isItemNumber[_giveItemNumber[1]] = true;
                            StoryBagControl._whichItem = _giveItemNumber[1];
                            if (StoryBagControl.isOpenBag)
                            {
                                StoryBagControl.isOpenBag = false;
                            }
                        }
                        break;

                    case 3:
                        isGetGift = true;
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
            }
        }
    }
    void ExchangeItem()
    {
        if (!isExchange) return;
        if (!isExchangeItem) return;
        if (!StoryDialogueControl_Girl.isDialogueEvent) return;

        StoryDialogueControl_Girl.isDialogueEvent = false;
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
    void RotationSprite()
    {
        if (!StoryDialogueControl_Girl.isDialogueRotation) return;
        if (!isRotation) return;

        float rotationThisFrame = _speed * Time.deltaTime;
        totalRotation += rotationThisFrame;

        if (totalRotation <= 120f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, rotationThisFrame, 0f);
            transform.rotation = transform.rotation * deltaRotation;
            if (totalRotation > 90f)
            {
                switch (_who)
                {
                    case 1:
                        if (!isWearingLittleRedHood)
                        {
                            isWearingLittleRedHood = true;
                        }
                        break;

                    case 4:
                        StoryNpcAnimator_Girl.isAngry = true;
                        break;

                    case 5:
                        StoryNpcAnimator_Girl.isHappy_Cri = true;
                        break;

                    case 6:
                        isCanKillWolf = true;
                        break;

                    case 9:
                        StoryNpcAnimator_Girl.isNormal = true;
                        break;
                }
            }
        }
        else
        {
            StoryDialogueControl_Girl.isDialogueRotation = false;
            isRotation = false;
            totalRotation = 0f;
            transform.rotation = initialRotation;
        }
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;
        if (StoryUIControl_Girl.isDialogue) return;

        if (isGive)
        {
            switch (_who)
            {
                case 1:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 2;
                            break;

                        default:
                            if (!isWearingLittleRedHood)
                            {
                                if (StoryThermometerControl_Girl._matchQuantity > 0)
                                {
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 6;
                                }
                                else if (StoryThermometerControl_Girl._matchQuantity <= 0 && _heldMatchboxesQuantity != 0)
                                {
                                    _heldMatchboxesQuantity--;
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 7;
                                }
                                else if (StoryThermometerControl_Girl._matchQuantity <= 0 && _heldMatchboxesQuantity == 0)
                                {
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 8;
                                }
                            }
                            else
                            {
                                if (!StoryNpcAnimator_Girl.isShotRunAway)
                                {
                                    if (StoryNpcAnimator_Girl.isHide)
                                    {
                                        isNeedHelp = true;
                                        StoryNpcAnimator_Girl._direction = 0;
                                        StoryNpcAnimator_Girl.isHide = false;
                                        StoryUIControl_Girl.isDialogue = true;
                                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                                        StoryDialogueControl_Girl._textCount = 49;
                                    }
                                    else
                                    {
                                        if (isNeedHelp)
                                        {
                                            StoryUIControl_Girl.isDialogue = true;
                                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                                            StoryDialogueControl_Girl._textCount = 32;
                                        }
                                    }
                                }
                                else
                                {
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 46;
                                }
                            }
                            break;
                    }
                    break;

                case 2:
                    _countMouseDown++;
                    BGM.PlayOneShot(find);
                    switch (_countMouseDown)
                    {
                        case 1:
                            StoryNpcAnimator_Girl.isOpen = true;
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 11;
                            break;

                        case 3:
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 13;
                            effects.SetActive(false);
                            Destroy(effects, 1f);
                            break;

                        default:
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 12;
                            break;
                    }
                    break;

                case 3:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 15;
                            break;

                        default:
                            if (!isGetGift)
                            {
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 16;
                            }
                            else
                            {
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 17;
                            }
                            break;
                    }
                    break;

                case 7:
                    StoryUIControl_Girl.isDialogue = true;
                    StoryDialogueControl_Girl._textCount = 36;

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

                case 8:
                    StoryUIControl_Girl.isDialogue = true;
                    StoryDialogueControl_Girl._textCount = 35;

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
            }
        }
        else
        {
            switch (_who)
            {
                case 4:
                    isInteractable = true;
                    StoryUIControl_Girl.isDialogue = true;
                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                    StoryDialogueControl_Girl._textCount = 18;
                    break;

                case 5:
                    if (!isGetGift)
                    {
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                        StoryDialogueControl_Girl._textCount = 20;
                    }
                    else
                    {
                        if (!isFinishExchangeGift)
                        {
                            isInteractable = true;
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 21;
                        }
                        else
                        {
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 23;
                        }
                    }
                    break;

                case 6:
                    if (isNeedHelp)
                    {
                        _countMouseDown++;
                        switch (_countMouseDown)
                        {
                            case 1:
                                isInteractable = true;
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 33;
                                break;

                            default:
                                if (!StoryNpcAnimator_Girl.isMoveSeeWolf)
                                {
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 34;
                                }
                                else
                                {
                                    if (!StoryNpcAnimator_Girl.isShotRunAway)
                                    {
                                        StoryUIControl_Girl.isDialogue = true;
                                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                                        StoryDialogueControl_Girl._isAboveWho2 = 1;
                                        StoryDialogueControl_Girl._textCount = 50;
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case 9:
                    if (isNeedHelp)
                    {
                        _countMouseDown++;
                        switch (_countMouseDown)
                        {
                            case 1:
                                isInteractable = true;
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 37;
                                break;

                            default:
                                if (!isGetApple)
                                {
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 38;
                                }
                                else
                                {
                                    if (!isGetKebab)
                                    {
                                        if (!isFirstAskFind)
                                        {
                                            isFirstAskFind = true;
                                            StoryUIControl_Girl.isDialogue = true;
                                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                                            StoryDialogueControl_Girl._textCount = 40;
                                        }
                                        else
                                        {
                                            if (!isAgreeFind)
                                            {
                                                StoryUIControl_Girl.isDialogue = true;
                                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                                StoryDialogueControl_Girl._textCount = 41;
                                            }
                                            else
                                            {
                                                StoryUIControl_Girl.isDialogue = true;
                                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                                StoryDialogueControl_Girl._textCount = 42;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        StoryUIControl_Girl.isDialogue = true;
                                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                                        StoryDialogueControl_Girl._textCount = 48;
                                    }
                                }
                                break;
                        }
                    }
                    break;

                case 11:
                    if (isTrashCanLid)
                    {
                        BGM.PlayOneShot(fight);
                        StoryNpcAnimator_Girl.isScared = true;
                        StoryNpcAnimator_Girl.isFightRunAway = true;
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = 1;
                        StoryDialogueControl_Girl._textCount = 51;
                    }
                    break;
            }
        }
        StoryBagControl.isOpenBag = false;
    }
    void OnMouseEnter()
    {
        if (SettingControl.isSettingActive) return;
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleObject(scaledSize));
        if (!StoryUIControl_Girl.isDialogue)
        {
            isInteractableUI = true;
        }
        _aboveWho = _who; 

        if (!isGet) return;
        if (!isBagGetItem) return;
        if (isGetItem) return;
        if (!isInteractable) return;
        if (!StoryBagControl.isOpenBag) return;
        if (StoryUIControl_Girl.isDialogue) return;

        for (int i = 0; i < _getItemNumber.Length; i++)
        {
            if (_getItemNumber[i] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
            {
                isBagGetItem = false;
                StoryBagControl.isOpenBag = false;
                StoryBagControl.isItemNumber[_getItemNumber[i]] = false;
                StoryBagControl._howManyGrids--;
                StoryBagControl.isRenewBag = true;
                StoryItemIntroduce_Girl.isIntroduce = true;
                _exchangeDifferentItemRecord = i;

                switch (_who)
                {
                    case 1:
                        switch (i)
                        {
                            case 0:
                                if (!isWearingLittleRedHood)
                                {
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 14;
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 1:
                                isRotation = true;
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 25;
                                break;

                            case 2:
                                if (StoryBagControl.isItemNumber[6] && isNeedHelp)
                                {
                                    isTrashCanLid = true;
                                    StoryBagControl.isItemNumber[6] = false;
                                    StoryBagControl._howManyGrids--;
                                    StoryPlayerAnimator_Girl.isIronRod = true;
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 47;
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;
                        }
                        break;

                    case 4:
                        isRotation = true;
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                        StoryDialogueControl_Girl._isAboveWho2 = 10;
                        StoryDialogueControl_Girl._textCount = 19;
                        break;

                    case 5:
                        isRotation = true;
                        isExchangeItem = true;
                        isFinishExchangeGift = true;
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                        StoryDialogueControl_Girl._textCount = 22;
                        break;

                    case 6:
                        switch (i)
                        {
                            case 0:
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 44;
                                break;

                            case 1:
                                isRotation = true;
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 45;
                                break;
                        }
                        break;

                    case 9:
                        switch (i)
                        {
                            case 0:
                                if (isAgreeFind)
                                {
                                    isRotation = true;
                                    isGetKebab = true;
                                    isExchangeItem = true;
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 43;
                                }
                                else 
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 1:
                                isExchangeItem = true;
                                isGetApple = true;
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 39;
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
            if (!isTeach)
            {
                isTeach = true;
                StoryTeachControl.isTeachActive = true;
            }
            StoryThermometerControl_Girl.isSkillActive = true;
            if (!isFinallyMatch)
            {
                StoryThermometerControl_Girl._matchQuantity = 15;
            }
            else 
            {
                StoryThermometerControl_Girl._matchQuantity += 30;
                StoryNpcAnimator_Girl._direction = 1;
                StoryNpcAnimator_Girl.isGoGrandmom = true;
            }
            isSkill = false;
        }
        isPickedUp = false;
        isAnim = false;
        isPlayerMove = true;
    }
}
