using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteractableControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("InteractableDistance")]
    public float _snapDistance = 12f;
    public float _scaleSpeed = 5f;
    private Vector3 originalScale;
    Vector3 scaledSize = new Vector3(0.55f, 0.55f, 0.55f);

    [Header("InteractableUI")]
    public GameObject interactableUI;
    public Text interactableName;
    public int _who;
    public static int _aboveWho = 0;
    public static bool isInteractableUI = false;
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
    public Sprite getItemSprite;
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;
    float _speed = 180f;

    //Npc_01Girl
    int _heldMatchboxesQuantity = 6;
    bool isWearingLittleRedRidingHood = false;
    //Npc_03SantaClaus
    bool isGetGift = false;

    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        interactableUI.SetActive(isInteractableUI);

        InteractableIsRotationSprite();
        PickUpItem();
        InteractableUI();
        GivePlayerObject();
    }

    void InteractableIsRotationSprite()
    {
        if (!isRotation) return;

        float rotationThisFrame = _speed * Time.deltaTime;
        totalRotation += rotationThisFrame;

        if (totalRotation <= 120f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, rotationThisFrame, 0f);
            transform.rotation = transform.rotation * deltaRotation;
            if (totalRotation > 90f)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = getItemSprite;
            }
        }
        else
        {
            isRotation = false;
            totalRotation = 0f;
            transform.rotation = initialRotation;
        }
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
                    InteractableNameUIPosition();
                }
                break;

            case 2:
                if (_aboveWho == _who)
                {
                    interactableName.text = "垃圾桶";
                    InteractableNameUIPosition();
                }
                break;

            case 3:
                if (_aboveWho == _who)
                {
                    interactableName.text = "聖誕老人";
                    InteractableNameUIPosition();
                }
                break;

            case 4:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小彥";
                    InteractableNameUIPosition();
                }
                break;

            case 5:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小欣";
                    InteractableNameUIPosition();
                }
                break;

            case 6:
                if (_aboveWho == _who)
                {
                    interactableName.text = "獵人";
                    InteractableNameUIPosition();
                }
                break;

            case 7:
                if (_aboveWho == _who)
                {
                    interactableName.text = "鐵棒";
                    InteractableNameUIPosition();
                }
                break;

            case 8:
                if (_aboveWho == _who)
                {
                    interactableName.text = "木枝";
                    InteractableNameUIPosition();
                }
                break;

            case 9:
                if (_aboveWho == _who)
                {
                    interactableName.text = "露營者";
                    InteractableNameUIPosition();
                }
                break;
        }
    }
    void InteractableNameUIPosition()
    {
        Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        interactableUI.transform.position = screenPos;
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

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;
        if (StoryUIControl_Girl.isDialogue) return;
        if (!isGive) return;

        switch (_who)
        {
            case 1:
                _countMouseDown++;
                switch (_countMouseDown)
                {
                    case 1:
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                        StoryDialogueControl_Girl._textCount = 2;
                        break;

                    default:
                        if (!isWearingLittleRedRidingHood)
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

                    case 2:
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                        StoryDialogueControl_Girl._textCount = 12;
                        break;

                    case 3:
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = _who;
                        StoryDialogueControl_Girl._textCount = 13;
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
    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        StartCoroutine(ScaleObject(scaledSize));
        if (!StoryUIControl_Girl.isDialogue)
        {
            isInteractableUI = true;
        }
        _aboveWho = _who; 

        if (!isGet) return;
        if (!isBagGetItem) return;
        if (isGetItem) return;

        if (_getItemNumber[0] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
        {
            StoryBagControl.isItemNumber[_getItemNumber[0]] = false;
            _exchangeDifferentItemRecord = 0;
        }
        else if (_getItemNumber[1] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
        {
            StoryBagControl.isItemNumber[_getItemNumber[1]] = false;
            _exchangeDifferentItemRecord = 1;
        }
        else if (_getItemNumber[2] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
        {
            StoryBagControl.isItemNumber[_getItemNumber[2]] = false;
            _exchangeDifferentItemRecord = 2;
        }
        else if (_getItemNumber[3] == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
        {
            StoryBagControl.isItemNumber[_getItemNumber[3]] = false;
            _exchangeDifferentItemRecord = 3;
        }
        StoryItemIntroduce_Girl.isIntroduce = true;
        StoryBagControl.isOpenBag = false;
        StoryBagControl._howManyGrids--;
        isRotation = true;
        if (_who != 9)
        {
            isBagGetItem = false;
        }
        Invoke("ExchangeItem", 1f);
    }
    void OnMouseExit()
    {
        StartCoroutine(ScaleObject(originalScale));
        isInteractableUI = false;
    }

    void ExchangeItem()
    {
        if (!isExchange) return;

        isPickedUp = true;
        StoryBagControl.isGet = true;
        StoryBagControl.isItemNumber[_exchangeItemNumber[_exchangeDifferentItemRecord]] = true;
        StoryBagControl._whichItem = _exchangeItemNumber[_exchangeDifferentItemRecord];
        if (StoryBagControl.isOpenBag)
        {
            StoryBagControl.isOpenBag = false;
        }         
    }

    IEnumerator ScaleObject(Vector3 targetScale)
    {
        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * _scaleSpeed);
            yield return null;
        }
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
            StoryThermometerControl_Girl.isSkillActive = true;
            StoryThermometerControl_Girl._matchQuantity = 20;
            isSkill = false;
        }
        isPickedUp = false;
        isAnim = false;
        isPlayerMove = true;
    }
}
