using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryInteractableControl : MonoBehaviour
{
    GameObject player;
    public Camera cam;

    [Header("InteractableDistance")]
    public float _snapDistance = 12f;
    public float _scaleSpeed = 5f;
    public Vector3 scaledSize = new Vector3(0.55f, 0.55f, 0.55f);
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
    Vector3 uiOffset = new Vector3(0, 120, 0);
    private bool isPickedUp = false;

    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        InteractableIsRotationSprite();
        PickUpItem();
    }

    void InteractableIsRotationSprite()
    {
        if (!isRotation) return;
        
        float rotationThisFrame = _speed * Time.deltaTime;
        totalRotation += rotationThisFrame;

        if (totalRotation <= 120f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, rotationThisFrame, 0f); // À@YÝSÐýÞD
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
        if (!isPickedUp) return;
        if (isGiveItem) return;
        print("OK");
        moveItemUI.SetActive(true);
        Vector3 startPosition = cam.WorldToScreenPoint(transform.position) + uiOffset;
        moveItemUI.transform.position = startPosition;

        StartCoroutine(MoveItemUI(moveItemUI, startPosition, bagUIPosition.position));
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
            if (StoryBagControl.isOpenBag)
                StoryBagControl.isOpenBag = false;
        }
    }
    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        StopAllCoroutines();
        StartCoroutine(ScaleObject(scaledSize));

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
        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            moveItemUI.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        moveItemUI.transform.position = end;
        moveItemUI.SetActive(false);
        isPickedUp = false;
    }
}
