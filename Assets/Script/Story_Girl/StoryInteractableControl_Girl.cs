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
    Vector3 scaledSize = new Vector3(0.55f, 0.55f, 0.55f);
    private Vector3 originalScale;

    [Header("Item")]
    public int _giveItemNumber;
    public int _getItemNumber;
    public static bool isBagGetItem = false;
    bool isGiveItem = false;
    bool isGetItem = false;

    [Header("Rotation")]
    public Sprite getItemSprite;
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;
    float _speed = 180f;

    [Header("ItemPickUp")]
    public GameObject moveItemUI;
    public Transform bagUIPosition;
    public static bool isPlayerMove = true;
    bool isPickedUp = false;
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
            Quaternion deltaRotation = Quaternion.Euler(0f, rotationThisFrame, 0f); // 繞Y軸旋轉
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
                }
                break;

            case 2:
                if (_aboveWho == _who)
                {
                    interactableName.text = "垃圾桶";
                }
                break;

            case 3:
                if (_aboveWho == _who)
                {
                    interactableName.text = "聖誕老人";
                }
                break;

            case 4:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小彥";
                }
                break;

            case 5:
                if (_aboveWho == _who)
                {
                    interactableName.text = "小欣";
                }
                break;
        }
        Vector3 worldPos = transform.position + new Vector3(0f, 8f, 0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        interactableUI.transform.position = screenPos;
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        if (!isGiveItem)
        {
            isGiveItem = true;
            isPickedUp = true;
            StoryBagControl.isGet = true;
            StoryBagControl.isItemNumber[_giveItemNumber] = true;
            StoryBagControl._whichItem = _giveItemNumber;
            if (StoryBagControl.isOpenBag)
                StoryBagControl.isOpenBag = false;
        }
    }
    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        StopAllCoroutines();
        StartCoroutine(ScaleObject(scaledSize));
        isInteractableUI = true;
        _aboveWho = _who;

        if (!isBagGetItem) return;
        if (isGetItem) return;

        if (_getItemNumber == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
        {
            isBagGetItem = false;
            isGetItem = true;
            isRotation = true;
            StoryBagControl.isOpenBag = false;
            StoryBagControl.isItemNumber[_getItemNumber] = false;
            StoryBagControl._howManyGrids--;
        }
    }
    void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleObject(originalScale));
        isInteractableUI = false;
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
            Debug.Log($"Elapsed: {elapsed}, deltaTime: {Time.deltaTime}");
            itemUI.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        moveItemUI.transform.position = end;
        itemUI.SetActive(false);
        isPickedUp = false;
        isAnim = false;
        isPlayerMove = true;
        Debug.Log("MoveItemUI completed");
    }
}
