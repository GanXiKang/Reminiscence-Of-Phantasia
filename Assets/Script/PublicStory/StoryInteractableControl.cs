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


    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;
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
            StoryBagControl.isOpenBag = false;
            StoryBagControl.isItemNumber[_getItemNumber] = false;
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
