using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryInteractableControl : MonoBehaviour
{
    [Header("InteractableDistance")]
    GameObject player;
    public float _snapDistance = 12f;
    public float _scaleSpeed = 5f;
    public Vector3 scaledSize = new Vector3(0.55f, 0.55f, 0.55f);
    private Vector3 originalScale;

    [Header("Item")]
    public int _giveItemNumber;
    public int _getItemNumber;
    public static bool isGetItem = false;
    bool isGiveItem = false;

    [Header("Rotation")]
    public float _speed = 150f;
    private float totalRotation = 0f;
    private Quaternion initialRotation;
    bool isRotation = false;

    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        InteractableIsRotationSprite();
    }

    void InteractableIsRotationSprite()
    {
        if (!isRotation) return;
        
        float rotationThisFrame = _speed * Time.deltaTime;
        totalRotation += rotationThisFrame;

        if (totalRotation <= 360f)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, rotationThisFrame, 0f); // À@YÝSÐýÞD
            transform.rotation = transform.rotation * deltaRotation;
        }
        else
        {
            totalRotation = 0f;
            transform.rotation = initialRotation;
        }
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        if (!isGiveItem)
        {
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

        if (!isGetItem) return;

        if (_getItemNumber == StoryBagControl._gridsItemNumber[StoryBagControl._whatItemButton])
        {
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
}
