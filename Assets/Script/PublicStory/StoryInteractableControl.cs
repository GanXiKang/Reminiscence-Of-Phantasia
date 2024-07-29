using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryInteractableControl : MonoBehaviour
{
    [Header("InteractableDistance")]
    GameObject player;
    public float _snapDistance = 10f;
    public float _scaleSpeed = 5f;
    public Vector3 scaledSize = new Vector3(0.8f, 0.8f, 0.8f);
    private Vector3 originalScale;

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
            
        print("Down!!");
    }
    void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleObject(scaledSize));
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
