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
    bool isInteractable = true;  //�yԇ����  ����Ȍ�Ԓ�󻥄�
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

    [Header("Effects")]
    public GameObject effects;

    //Rotation
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;
    float _speed = 180f;

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
                    interactableName.text = "��̫��";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 2:
                if (_aboveWho == _who)
                {
                    interactableName.text = "��";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 3:
                if (_aboveWho == _who)
                {
                    interactableName.text = "����";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 4:
                if (_aboveWho == _who)
                {
                    interactableName.text = "����";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 5:
                if (_aboveWho == _who)
                {
                    interactableName.text = "����";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 6:
                if (_aboveWho == _who)
                {
                    interactableName.text = "��̭";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 7:
                if (_aboveWho == _who)
                {
                    interactableName.text = "����";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 8:
                if (_aboveWho == _who)
                {
                    interactableName.text = "��̫";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;

            case 9:
                if (_aboveWho == _who)
                {
                    interactableName.text = "С��";
                    Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                    interactableUI.transform.position = screenPos;
                }
                break;
        }
    }
    void GivePlayerObject()
    {
        if (StoryUIControl_Momotaro.isDialogue) return;

        if (isGiveItem)
        {
            if (_whoGive == _who)
            {
                isGiveItem = false;
                switch (_who)
                {
                    case 1:
                        
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
                case 7:
                case 8:
                case 9:
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
        else
        {
            switch (_who)
            {
                case 1:

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
                        switch (i)
                        {
                            case 0:
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
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[5] = true;
                                StoryBagControl.isItemNumber[6] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[i];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
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

                            case 3:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[10] = true;
                                StoryBagControl.isItemNumber[11] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[i];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;

                            case 4:
                            case 5:
                                if (!StoryBagControl.isItemNumber[5] && !StoryBagControl.isItemNumber[6])
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = false;
                                    StoryBagControl._howManyGrids++;
                                }
                                else
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[5] = false;
                                    StoryBagControl.isItemNumber[6] = false;
                                    StoryBagControl.isItemNumber[_exchangeItemNumber[i]] = true;
                                    StoryBagControl._whichItem = _exchangeItemNumber[i];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                break;

                            case 6:
                                isPickedUp = true;
                                StoryBagControl.isGet = true;
                                StoryBagControl.isItemNumber[_exchangeItemNumber[i]] = true;
                                StoryBagControl._whichItem = _exchangeItemNumber[i];
                                if (StoryBagControl.isOpenBag)
                                {
                                    StoryBagControl.isOpenBag = false;
                                }
                                break;

                            case 7:
                            case 8:
                                if (!StoryBagControl.isItemNumber[10] && !StoryBagControl.isItemNumber[11])
                                {
                                    StoryBagControl.isItemNumber[_getItemNumber[i]] = false;
                                    StoryBagControl._howManyGrids++;
                                }
                                else
                                {
                                    isPickedUp = true;
                                    StoryBagControl.isGet = true;
                                    StoryBagControl.isItemNumber[10] = false;
                                    StoryBagControl.isItemNumber[11] = false;
                                    StoryBagControl.isItemNumber[_exchangeItemNumber[i]] = true;
                                    StoryBagControl._whichItem = _exchangeItemNumber[i];
                                    if (StoryBagControl.isOpenBag)
                                    {
                                        StoryBagControl.isOpenBag = false;
                                    }
                                }
                                break;
                        }
                        break;

                    case 3:
                        switch (i)
                        {
                            case 0:
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
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:

                                break;
                        }
                        break;

                    case 6:
                        switch (i)
                        {
                            case 0:
                                StoryRiceDumpling_Momotaro._whoEatGoldRice = _who;
                                StoryBagControl.isItemNumber[_getItemNumber[i]] = true;
                                StoryBagControl._howManyGrids++;
                                break;

                            case 1:

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
