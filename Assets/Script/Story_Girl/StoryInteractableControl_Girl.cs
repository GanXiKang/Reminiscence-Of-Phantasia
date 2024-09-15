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

    [Header("Item")]
    public bool isGive;
    public int[] _giveItemNumber;
    public bool isGet;
    public int[] _getItemNumber;
    public bool isExchange;
    public int[] _exchangeItemNumber;
    public static bool isBagGetItem = false;
    bool isGiveItem = false;
    bool isGetItem = false;
    int _exchangeDifferentItemRecord;
    int _countMouseDown;

    [Header("Rotation")]
    public Sprite getItemSprite;
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;
    float _speed = 180f;

    [Header("ItemPickUp")]
    public GameObject moveItemUI;
    public Transform bagUIPosition;
    public Transform skillUIPosition;
    public static bool isPlayerMove = true;
    public static bool isPickedUp = false;
    bool isAnim = false;

    [Header("InteractableUI")]
    public GameObject interactableUI;
    public Text interactableName;
    public int _who;
    public static int _aboveWho = 0;
    public static bool isInteractableUI = false;

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

        StartCoroutine(MoveItemUI(moveItemUI, startPosition, bagUIPosition.position));
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
                    InteractableUIPosition();
                }
                break;

            case 2:
                if (_aboveWho == _who)
                {
                    interactableName.text = "垃圾桶";
                    InteractableUIPosition();
                }
                break;

            case 3:
                if (_aboveWho == _who)
                {
                    interactableName.text = "聖誕老人";
                    InteractableUIPosition();
                }
                break;

            case 4:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小彥";
                    InteractableUIPosition();
                }
                break;

            case 5:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小欣";
                    InteractableUIPosition();
                }
                break;

            case 6:
                if (_aboveWho == _who)
                {
                    interactableName.text = "獵人";
                    InteractableUIPosition();
                }
                break;

            case 7:
                if (_aboveWho == _who)
                {
                    interactableName.text = "鐵棒";
                    InteractableUIPosition();
                }
                break;

            case 8:
                if (_aboveWho == _who)
                {
                    interactableName.text = "木枝";
                    InteractableUIPosition();
                }
                break;

            case 9:
                if (_aboveWho == _who)
                {
                    interactableName.text = "露營者";
                    InteractableUIPosition();
                }
                break;
        }
    }
    void InteractableUIPosition()
    {
        Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        interactableUI.transform.position = screenPos;
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;
        if (!isGive) return;

        switch (_who)
        {
            case 1:
                if (!isGiveItem)
                {
                    isGiveItem = true;
                    StoryUIControl_Girl.isDialogue = true;
                    StoryDialogueControl_Girl._isAboveWho1 = _who;
                    StoryDialogueControl_Girl._textCount = 2;

                    if (StoryBagControl.isOpenBag)
                    {
                        StoryBagControl.isOpenBag = false;
                    }
                }
                break;

            case 2:
                _countMouseDown++;
                if (_countMouseDown == 3)
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
                if (!isGiveItem && _giveItemNumber[0] != 0)
                {
                    isGiveItem = true;
                    isPickedUp = true;
                    StoryBagControl.isGet = true;
                    StoryBagControl.isItemNumber[_giveItemNumber[0]] = true;
                    StoryBagControl._whichItem = _giveItemNumber[0];

                    if (StoryBagControl.isOpenBag)
                    {
                        StoryBagControl.isOpenBag = false;
                    }
                }
                break;

            case 7:
                isInteractableUI = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(this.gameObject, 2f);
                break;

            case 8:
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
        isPickedUp = false;
        isAnim = false;
        isPlayerMove = true;
    }
}
