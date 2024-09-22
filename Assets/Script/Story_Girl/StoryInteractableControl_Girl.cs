using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteractableControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("InteractableDistance")]
    public float _snapDistance = 12f;
    public float _scaleSpeed = 20f;
    private Coroutine currentCoroutine;
    private Vector3 originalScale;
    Vector3 scaledSize = new Vector3(1f, 1f, 1f);

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

    [Header("Rotation")]
    public Sprite getItemSprite; //可能不需要
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;
    float _speed = 180f;

    //Npc_01Girl
    int _heldMatchboxesQuantity = 6;
    public static bool isWearingLittleRedHood = false;
    //Npc_03SantaClaus
    public static bool isGetGift = false;
    //Npc_05XiaoXin
    bool isFinishExchangeGift = false;
    //Npc_06Hunter
    public static bool isKillWolf = false;
    //Npc_09Camping
    bool isGetApple = false;
    bool isGetKebab = false;
    public static bool isFirstAskFind = false;
    public static bool isAgreeFind = false;

    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        interactableUI.SetActive(isInteractableUI);

        PickUpItem();
        InteractableUI();
        GivePlayerObject();
        ExchangeItem();
        InteractableIsRotationSprite();
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
                    interactableName.text = "木枝";
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
        }
    }
    void GivePlayerObject()
    {
        if (StoryUIControl_Girl.isDialogue) return;

        if (isGiveItem)
        {
            if (_whoGive == _who)
            {
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
                isGiveItem = false;
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
    void InteractableIsRotationSprite()
    {
        if (!StoryDialogueControl_Girl.isDialogueEvent) return;
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
                        isWearingLittleRedHood = true;
                        break;
                }
            }
        }
        else
        {
            StoryDialogueControl_Girl.isDialogueEvent = false;
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
                            StoryGameControl_Girl.isWallActive = false;
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
                                if (!isKillWolf)
                                {
                                    StoryUIControl_Girl.isDialogue = true;
                                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                                    StoryDialogueControl_Girl._textCount = 32;
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
                    switch (_countMouseDown)
                    {
                        case 1:
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 11;
                            break;

                        case 3:
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 13;
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
                    StoryBagControl.isGet = true;
                    StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                    StoryBagControl._whichItem = _giveItemNumber[0];
                    if (StoryBagControl.isOpenBag)
                    {
                        StoryBagControl.isOpenBag = false;
                    }

                    isInteractableUI = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(this.gameObject, 2f);
                    break;

                case 8:
                    StoryUIControl_Girl.isDialogue = true;
                    StoryDialogueControl_Girl._textCount = 35;

                    isPickedUp = true;
                    StoryBagControl.isGet = true;
                    StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                    StoryBagControl._whichItem = _giveItemNumber[0];
                    if (StoryBagControl.isOpenBag)
                    {
                        StoryBagControl.isOpenBag = false;
                    }

                    isInteractableUI = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
                            StoryUIControl_Girl.isDialogue = true;
                            StoryDialogueControl_Girl._isAboveWho1 = _who;
                            StoryDialogueControl_Girl._textCount = 34;
                            break;
                    }
                    break;

                case 9:
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
                                    if (isFirstAskFind)
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
        if (!StoryUIControl_Girl.isDialogue)
        {
            isInteractableUI = true;
        }
        _aboveWho = _who; 

        if (!isGet) return;
        if (!isBagGetItem) return;
        if (isGetItem) return;
        if (!isInteractable) return;
        if (StoryUIControl_Girl.isDialogue) return;

        for (int i = 0; i < _getItemNumber.Length; i++)
        {
            if (_getItemNumber[i] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
            {
                isBagGetItem = false;
                StoryBagControl.isOpenBag = false;
                StoryBagControl.isItemNumber[_getItemNumber[i]] = false;
                StoryBagControl._howManyGrids--;
                StoryItemIntroduce_Girl.isIntroduce = true;
                _exchangeDifferentItemRecord = i;

                switch (_who)
                {
                    case 1:
                        switch (i)
                        {
                            case 0:
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 14;
                                break;

                            case 1:
                                isRotation = true;
                                
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 25;
                                break;

                            case 2:
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._textCount = 47;
                                break;
                        }
                        break;

                    case 4:
                        StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                        StoryBagControl._howManyGrids++;
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                        StoryDialogueControl_Girl._isAboveWho2 = 10;
                        StoryDialogueControl_Girl._textCount = 19;
                        break;

                    case 5:
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
                                isKillWolf = true;
                                StoryUIControl_Girl.isDialogue = true;
                                StoryDialogueControl_Girl._isAboveWho1 = _who;
                                StoryDialogueControl_Girl._isAboveWho2 = 1;
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
            StoryTeachControl.isTeachActive = true;
            StoryThermometerControl_Girl.isSkillActive = true;
            StoryThermometerControl_Girl._matchQuantity = 20;
            isSkill = false;
        }
        isPickedUp = false;
        isAnim = false;
        isPlayerMove = true;
    }
}
