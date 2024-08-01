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

    public static bool isGet = false;

    void Start()
    {
        player = GameObject.Find("Player");

        originalScale = transform.localScale;
    }

    
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;

        StoryBagControl.isGet = true;
        StoryBagControl._whichItemToGet = 1;
    }
    void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance) return;
        StopAllCoroutines();
        StartCoroutine(ScaleObject(scaledSize));

        if (!isGet) return;
        print("Get");
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
