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
    bool isInteractable = true;  //œyÔ‡¸ü¸Ä  ÓÃì¶ÏÈŒ¦Ô’ºó»¥„Ó
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
    int _itemGoddess;
    public static bool isAnswerLie = false;
    //06Raccoon
    public static bool isFinishWork = false;
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
                            case 1:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[5] = true;
                                StoryBagControl.isItemNumber[6] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[_itemGoddess];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;

                            case 2:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_exchangeItemNumber[_itemGoddess]] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[_itemGoddess];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;

                            case 4:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[10] = true;
                                StoryBagControl.isItemNumber[11] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[_itemGoddess];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
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
                    case 1:
                        
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
                    break;

                case 7:
                    StoryUIControl_Momotaro.isDialogue = true;
                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                    StoryDialogueControl_Momotaro._isAboveWho2 = 8;
                    StoryDialogueControl_Momotaro._textCount = 56;
                    break;

                case 9:
                    _countMouseDown++;
                    switch (_countMouseDown)
                    {
                        case 1:
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
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 27;
                            }
                            break;
                    }
                    break;

                case 4:
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
                    break;

                case 5:
                    if (!isFinishWork)
                    {
                        if (!StoryPlayerAnimator_Momotaro.isDonkey)
                        {
                            isInteractable = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 33;
                        }
                        else
                        {
                            isInteractable = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 34;
                        }
                    }
                    break;

                case 6:
                    if (!isFinishWork)
                    {
                        if (!StoryPlayerAnimator_Momotaro.isDonkey)
                        {
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 42;
                        }
                        else
                        {
                            isInteractable = true;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 43;
                        }
                    }
                    else
                    {
                        if (!StoryPlayerAnimator_Momotaro.isDonkey)
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
                                StoryDialogueControl_Momotaro._textCount = 45;
                            }
                        }
                        else
                        {
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                            StoryDialogueControl_Momotaro._textCount = 46;
                        }
                    }
                    break;

                case 8:
                    StoryUIControl_Momotaro.isDialogue = true;
                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                    StoryDialogueControl_Momotaro._textCount = 64;
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
                            case 0: //œyÔ‡ÓÃ
                                isPickedUp = true;
                                isSkill = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[2] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[i];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
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
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 21;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 4:
                                StoryNpcAnimator_Momotaro.isOutLake = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 22;
                                break;
                        }
                        break;

                    case 3:
                        switch (i)
                        {
                            case 0:
                                isEatGoldRice = true;
                                StoryUIControl_Momotaro.isDialogue = true;
                                StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                StoryDialogueControl_Momotaro._textCount = 26;
                                StoryRiceDumpling_Momotaro._whoEatGoldRice = _who;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 2:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_exchangeItemNumber[i]] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[i];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
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
                                StoryGameControl_Momotaro.isParrotActive = true;
                                StoryNpcAnimator_Momotaro.isGold_Monkey = true;
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
                                    isEatGoldRice = true;
                                    StoryUIControl_Momotaro.isDialogue = true;
                                    StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                                    StoryDialogueControl_Momotaro._textCount = 45;
                                    StoryRiceDumpling_Momotaro._whoEatGoldRice = _who;
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
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:

                                break;
                        }
                        break;

                    case 8:
                        switch (i)
                        {
                            case 0:
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:

                                break;
                        }
                        break;

                    case 9:
                        isEatGoldRice = true;
                        StoryUIControl_Momotaro.isDialogue = true;
                        StoryDialogueControl_Momotaro._isAboveWho1 = _who;
                        StoryDialogueControl_Momotaro._textCount = 70;
                        StoryRiceDumpling_Momotaro._whoEatGoldRice = _who;
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
