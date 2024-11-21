using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteractableControl_Momotaro : MonoBehaviour
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

    [Header("Item_Get")]
    public bool isGet;
    public int[] _getItemNumber;
    public static bool isBagGetItem = false;
    bool isGetItem = false;
    bool isEatGoldRice = false;

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

    //Rotation
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;
    float _speed = 180f;

    //02Goddess
    int _itemGoddess = 0;
    public static bool isGoddessGetSkill = false;
    public static bool isAnswerLie = false;
    public static bool isAnswerGold = false;
    //03Donkey
    public static bool isGiveTheRightGift = false;
    public static bool isWrongGift = false;
    bool isKnowTheRightGift = false;
    //06Raccoon
    public static bool isFinishWork = false;
    //07Dog & 08Chicken
    public static bool isMeet = false;
    public static bool isSuccessfulPerformance = false;
    //09Parrot
    public static bool isAnswerCorrect = false;

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

        if (StoryUIControl_Momotaro.isDialogue)
        {
            isInteractableUI = false;
        }

        if (!isInteractableUI) return;

        switch (_who)
        {
            case 1:
                if (_aboveWho == _who)
                {
                    interactableName.text = "ÌÒÌ«ÀÉ";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 2:
                if (_aboveWho == _who)
                {
                    interactableName.text = "ºþ";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 3:
                if (_aboveWho == _who)
                {
                    interactableName.text = "ßÔßÔ";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 4:
                if (_aboveWho == _who)
                {
                    interactableName.text = "¿ÕÖú";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 5:
                if (_aboveWho == _who)
                {
                    interactableName.text = "ßä×Ó";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 6:
                if (_aboveWho == _who)
                {
                    interactableName.text = "ºàÌ­";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 7:
                if (_aboveWho == _who)
                {
                    interactableName.text = "¶¹Íè";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 8:
                if (_aboveWho == _who)
                {
                    interactableName.text = "¼ªÌ«";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 9:
                if (_aboveWho == _who)
                {
                    interactableName.text = "Ð¡ÅÁ";
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
                        isPickedUp = true;
                        StoryBagControl.isGet = true;
                        StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                        StoryBagControl._whichItem = _giveItemNumber[0];
                        if (StoryBagControl.isOpenBag)
                        {
                            StoryBagControl.isOpenBag = false;
                        }
                        break;

                    case 2:
                        switch (_itemGoddess)
                        {
                            case 0:
                                if (!isAnswerLie)
                                {
                                    isGoddessGetSkill = true;
                                    isPickedUp = true;
                                    isSkill = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[2] = true;
                                    StoryBagControl._whichItem = _exchangeItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                else
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[1] = true;
                                    StoryBagControl._whichItem = _getItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                break;

                            case 1:
                                if (!isAnswerLie)
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[5] = true;
                                    StoryBagControl.isItemNumber[6] = true;
                                    StoryBagControl._whichItem = _exchangeItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                else
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[4] = true;
                                    StoryBagControl._whichItem = _getItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                break;

                            case 2:
                                if (isAnswerLie)
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[_exchangeItemNumber[_itemGoddess]] = true;
                                    StoryBagControl._whichItem = _exchangeItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                else
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[7] = true;
                                    StoryBagControl._whichItem = _getItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                break;

                            case 3:
                                if (!isAnswerLie)
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[10] = true;
                                    StoryBagControl.isItemNumber[11] = true;
                                    StoryBagControl._whichItem = _exchangeItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                else
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[9] = true;
                                    StoryBagControl._whichItem = _getItemNumber[_itemGoddess];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                break;
                        }
                        break;

                    case 3:
                        if (!isGiveTheRightGift)
                        {
                            isPickedUp = true;
                            StoryBagControl.isGet = true;
                            StoryBagControl.isItemNumber[_exchangeItemNumber[0]] = true;
                            StoryBagControl._whichItem = _exchangeItemNumber[0];
                            if (StoryBagControl.isOpenBag)
                            {
                                StoryBagControl.isOpenBag = false;
                            }
                        }
                        else
                        {
                            isPickedUp = true;
                            StoryBagControl.isGet = true;
                            StoryBagControl.isItemNumber[_exchangeItemNumber[1]] = true;
                            StoryBagControl._whichItem = _exchangeItemNumber[1];
                            if (StoryBagControl.isOpenBag)
                            {
                                StoryBagControl.isOpenBag = false;
                            }
                        }
                        break;

                    case 7:
                        isPickedUp = true;
                        StoryBagControl.isGet = true;
                        StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                        StoryBagControl._whichItem = _giveItemNumber[0];
                        if (StoryBagControl.isOpenBag)
                        {
                            StoryBagControl.isOpenBag = false;
                        }
                        break;

                    case 9:
                        isInteractable = true;
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
        if (!StoryDialogueControl_Momotaro.isDialogueEvent) return;

        StoryDialogueControl_Momotaro.isDialogueEvent = false;
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
        if (!StoryDialogueControl_Momotaro.isDialogueRotation) return;
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
                    case 4:
                        StoryNpcAnimator_Momotaro.isGold_Monkey = true;
                        break;

                    case 7:
                        StoryNpcAnimator_Momotaro.isSliver_Dog = true;
                        break;

                    case 8:
                        StoryNpcAnimator_Momotaro.isGold_Chicken = true;
                        break;
                }
            }
        }
        else
        {
            StoryDialogueControl_Momotaro.isDialogueRotation = false;
            isRotation = false;
            totalRotation = 0f;
            transform.rotation = initialRotation;
        }
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;
        if (StoryUIControl_Momotaro.isDialogue) return;

        if (isGive)
        {
            switch (_who)
            {
                case 1:
                    if (StoryPlayerAnimator_Momotaro.isHuman)
                    {
                        _countMouseDown++;
                        switch (_countMouseDown)
                        {
                            case 1:
                                isInteractable = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 9;
                                break;

                            default:
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 10;
                                break;
                        }
                    }
                    break;

                case 7:
                    if (StoryPlayerAnimator_Momotaro.isHuman)
                    {
                        _countMouseDown++;
                        switch (_countMouseDown)
                        {
                            case 1:
                                isMeet = true;
                                isInteractable = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._isAboveWho2 = 8;
                                StoryDialogueControl_Momotaro._textCount = 56;
                                break;

                            default:
                                if (!isSuccessfulPerformance)
                                {
                                    if (!StoryNpcAnimator_Momotaro.isSliver_Dog)
                                    {
                                        StoryUIControl_Momotaro.isDialogue = true;
                                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                        StoryDialogueControl_Momotaro._textCount = 54;
                                    }
                                    else
                                    {
                                        if (StoryNpcAnimator_Momotaro.isGold_Chicken)
                                        {
                                            StoryUIControl_Momotaro.isDialogue = true;
                                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                            StoryDialogueControl_Momotaro._isAboveWho2 = 8;
                                            StoryDialogueControl_Momotaro._textCount = 60;
                                        }
                                    }
                                }
                                else
                                {
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._isAboveWho2 = 8;
                                    StoryDialogueControl_Momotaro._textCount = 57;
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (StoryPlayerAnimator_Momotaro.isParrot)
                        {
                            if (StoryNpcAnimator_Momotaro.isGold_Chicken && StoryNpcAnimator_Momotaro.isSliver_Dog)
                            {
                                if (!isSuccessfulPerformance)
                                {
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._isAboveWho2 = 8;
                                    StoryDialogueControl_Momotaro._textCount = 63;
                                }
                            }
                        }
                    }
                    break;

                case 9:
                    if (StoryPlayerAnimator_Momotaro.isHuman)
                    {
                        _countMouseDown++;
                        switch (_countMouseDown)
                        {
                            case 1:
                                isInteractable = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 68;
                                break;

                            default:
                                if (isAnswerCorrect)
                                {
                                    if (!isEatGoldRice)
                                    {
                                        StoryUIControl_Momotaro.isDialogue = true;
                                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                        StoryDialogueControl_Momotaro._textCount = 69;
                                    }
                                }
                                else
                                {
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 71;
                                }
                                break;
                        }
                    }
                    break;
            }
        }
        else
        {
            switch (_who)
            {
                case 2:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
                            isInteractable = true;
                            StoryNpcAnimator_Momotaro.isOutLake = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 17;
                            break;
                    }
                    break;

                case 3:
                    if (isGoddessGetSkill)
                    {
                        if (StoryPlayerAnimator_Momotaro.isHuman || StoryPlayerAnimator_Momotaro.isDonkey)
                        {
                            _countMouseDown++;
                            switch (_countMouseDown)
                            {
                                case 1:
                                    isInteractable = true;
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 24;
                                    break;

                                default:
                                    if (!isEatGoldRice)
                                    {
                                        StoryUIControl_Momotaro.isDialogue = true;
                                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                        StoryDialogueControl_Momotaro._textCount = 25;
                                    }
                                    else
                                    {
                                        if (StoryNpcAnimator_Momotaro._dating == 0)
                                        {
                                            StoryUIControl_Momotaro.isDialogue = true;
                                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                            StoryDialogueControl_Momotaro._textCount = 27;
                                        }
                                        else
                                        {
                                            if (isWrongGift)
                                            {
                                                if (!isKnowTheRightGift)
                                                {
                                                    isKnowTheRightGift = true;
                                                    StoryUIControl_Momotaro.isDialogue = true;
                                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                                    StoryDialogueControl_Momotaro._isAboveWho2 = 5;
                                                    StoryDialogueControl_Momotaro._textCount = 30;
                                                }
                                                else
                                                {
                                                    StoryUIControl_Momotaro.isDialogue = true;
                                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                                    StoryDialogueControl_Momotaro._textCount = 29;
                                                }
                                            }
                                            if (isGiveTheRightGift)
                                            {
                                                if (!StoryNpcAnimator_Momotaro.isShy)
                                                {
                                                    StoryUIControl_Momotaro.isDialogue = true;
                                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                                    StoryDialogueControl_Momotaro._isAboveWho2 = 5;
                                                    StoryDialogueControl_Momotaro._textCount = 31;
                                                }
                                                else
                                                {
                                                    StoryUIControl_Momotaro.isDialogue = true;
                                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                                    StoryDialogueControl_Momotaro._isAboveWho2 = 5;
                                                    StoryDialogueControl_Momotaro._textCount = 32;
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                case 4:
                    if (isGoddessGetSkill && StoryPlayerAnimator_Momotaro.isHuman)
                    {
                        _countMouseDown++;
                        switch (_countMouseDown)
                        {
                            case 1:
                                isInteractable = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 47;
                                break;

                            default:
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 48;
                                break;
                        }
                    }
                    break;

                case 5:
                    if (!isFinishWork)
                    {
                        if (StoryPlayerAnimator_Momotaro.isHuman)
                        {
                            isInteractable = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 33;
                        }
                        if (StoryPlayerAnimator_Momotaro.isDonkey)
                        {
                            isInteractable = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 34;
                        }
                    }
                    else
                    {
                        if (StoryPlayerAnimator_Momotaro.isHuman)
                        {
                            if (StoryNpcAnimator_Momotaro._dating == 0)
                            {
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 35;
                            }
                            if (StoryNpcAnimator_Momotaro.isShy)
                            {
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = 3;
                                StoryDialogueControl_Momotaro._isAboveWho2 = _who;
                                StoryDialogueControl_Momotaro._textCount = 32;
                            }
                        }
                        if (StoryPlayerAnimator_Momotaro.isDonkey)
                        {
                            switch (StoryNpcAnimator_Momotaro._dating)
                            {
                                case 0:
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 36;
                                    break;

                                case 1:
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 38;
                                    break;

                                case 2:
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 39;
                                    break;

                                case 3:
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 40;
                                    break;

                                case 4:
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 41;
                                    break;
                            }
                        }
                    }
                    break;

                case 6:
                    if (!isFinishWork)
                    {
                        if (StoryPlayerAnimator_Momotaro.isHuman)
                        {
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 42;
                        }
                        if (StoryPlayerAnimator_Momotaro.isDonkey)
                        {
                            isInteractable = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 43;
                        }
                    }
                    else
                    {
                        if (StoryPlayerAnimator_Momotaro.isHuman)
                        {
                            if (!isEatGoldRice)
                            {
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 72;
                            }
                            else
                            {
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 75;
                            }
                        }
                        if (StoryPlayerAnimator_Momotaro.isDonkey)
                        {
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 46;
                        }
                    }
                    break;

                case 8:
                    if (StoryPlayerAnimator_Momotaro.isHuman)
                    {
                        if (!isSuccessfulPerformance)
                        {
                            if (!StoryNpcAnimator_Momotaro.isGold_Chicken)
                            {
                                if (!isMeet)
                                {
                                    isInteractable = true;
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 64;
                                }
                                else
                                {
                                    isInteractable = true;
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 66;
                                }
                            }
                            else
                            {
                                if (StoryNpcAnimator_Momotaro.isSliver_Dog)
                                {
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = 7;
                                    StoryDialogueControl_Momotaro._isAboveWho2 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 60;
                                }
                            }
                        }
                        else
                        {
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = 7;
                            StoryDialogueControl_Momotaro._isAboveWho2 = _who;
                            StoryDialogueControl_Momotaro._textCount = 57;
                        }
                    }
                    else
                    {
                        if (StoryPlayerAnimator_Momotaro.isParrot)
                        {
                            if (StoryNpcAnimator_Momotaro.isGold_Chicken && StoryNpcAnimator_Momotaro.isSliver_Dog)
                            {
                                if (!isSuccessfulPerformance)
                                {
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = 7;
                                    StoryDialogueControl_Momotaro._isAboveWho2 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 63;
                                }
                            }
                        }
                    }
                    break;
            }
        }
        StoryBagControl.isOpenBag = false;
    }
    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(ScaleObject(scaledSize));
        if (!StoryUIControl_Momotaro.isDialogue)
        {
            isInteractableUI = true;
        }
        _aboveWho = _who;

        if (!isGet) return;
        if (!isBagGetItem) return;
        if (isGetItem) return;
        if (!isInteractable) return;
        if (StoryUIControl_Momotaro.isDialogue) return;

        for (int i = 0; i < _getItemNumber.Length; i++)
        {
            if (_getItemNumber[i] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
            {
                isBagGetItem = false;
                StoryBagControl.isOpenBag = false;
                StoryBagControl.isItemNumber[_getItemNumber[i]] = false;
                StoryBagControl._howManyGrids--;
                StoryBagControl.isRenewBag = true;
                StoryItemIntroduce_Momotaro.isIntroduce = true;
                _exchangeDifferentItemRecord = i;

                switch (_who)
                {
                    case 2:
                        _itemGoddess = i;
                        switch (i)
                        {
                            case 0:
                                StoryNpcAnimator_Momotaro.isOutLake = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 18;
                                break;

                            case 1:
                                StoryNpcAnimator_Momotaro.isOutLake = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 19;
                                break;

                            case 2:
                                StoryNpcAnimator_Momotaro.isOutLake = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 20;
                                break;

                            case 3:
                                StoryNpcAnimator_Momotaro.isOutLake = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 22;
                                break;

                            case 4:
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 21;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;
                        }
                        break;

                    case 3:
                        switch (i)
                        {
                            case 0:
                                if (!isEatGoldRice)
                                {
                                    isEatGoldRice = true;
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 26;
                                    StoryRiceDumpling_Momotaro._whoEatGoldRice = _who;
                                }
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:
                                if (StoryNpcAnimator_Momotaro._dating == 4 && !isWrongGift)
                                {
                                    isWrongGift = true;
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 28;
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 2:
                                if (StoryNpcAnimator_Momotaro._dating == 4)
                                {
                                    if (!isWrongGift)
                                    {
                                        isGiveTheRightGift = true;
                                        StoryUIControl_Momotaro.isDialogue = true;
                                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                        StoryDialogueControl_Momotaro._textCount = 28;
                                    }
                                    else
                                    {
                                        isWrongGift = false;
                                        StoryUIControl_Momotaro.isDialogue = true;
                                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                        StoryDialogueControl_Momotaro._isAboveWho2 = 5;
                                        StoryDialogueControl_Momotaro._textCount = 31;
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

                    case 4:
                        switch (i)
                        {
                            case 0:
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 50;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:
                                isRotation = true;
                                StoryGameControl_Momotaro.isParrotActive = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 49;
                                break;
                        }
                        break;

                    case 5:
                        StoryUIControl_Momotaro.isDialogue = true;
                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                        StoryDialogueControl_Momotaro._textCount = 37;
                        StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                        StoryBagControl._howManyGrids++;
                        break;

                    case 6:
                        switch (i)
                        {
                            case 0:
                                if (isFinishWork)
                                {
                                    if (!isEatGoldRice)
                                    {
                                        isEatGoldRice = true;
                                        StoryUIControl_Momotaro.isDialogue = true;
                                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                        StoryDialogueControl_Momotaro._textCount = 45;
                                        StoryRiceDumpling_Momotaro._whoEatGoldRice = _who;
                                    }
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                else
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                    StoryBagControl._howManyGrids++;
                                }
                                break;

                            case 1:
                                isFinishWork = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 44;
                                break;
                        }
                        break;

                    case 7:
                        switch (i)
                        {
                            case 0:
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = 8;
                                StoryDialogueControl_Momotaro._isAboveWho2 = _who;
                                StoryDialogueControl_Momotaro._textCount = 58;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:
                                isRotation = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 55;
                                break;
                        }
                        break;

                    case 8:
                        switch (i)
                        {
                            case 0:
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._isAboveWho2 = 7;
                                StoryDialogueControl_Momotaro._textCount = 58;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 65;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 2:
                                isRotation = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 67;
                                break;
                        }
                        break;

                    case 9:
                        if (!isEatGoldRice)
                        {
                            isEatGoldRice = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 70;
                            StoryRiceDumpling_Momotaro._whoEatGoldRice = _who;
                        }
                        StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                        StoryBagControl._howManyGrids++;
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
            isSkill = false;
            StoryRiceDumpling_Momotaro.isSkillActive = true;
        }
        isPickedUp = false;
        isAnim = false;
        isPlayerMove = true;
    }
}
