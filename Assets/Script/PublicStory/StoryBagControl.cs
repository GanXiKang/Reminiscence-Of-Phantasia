using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoryBagControl : MonoBehaviour
{
    [Header("BagUI")]
    public Image background;
    public GameObject[] item;
    public float _speed = 2f;
    bool isOpenBag = false;
    bool isAnim = false;
    float value = 0;

    [Header("ItemUIDragHandler")]
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private bool isDragging = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        Bag();
    }

    public void Bag_Button()
    {
        isOpenBag = !isOpenBag;
        isAnim = true;
    }

    void Bag()
    {
        background.fillAmount = value;

        if (isOpenBag)
        {
            if (value < 1)
            {
                value += _speed * Time.deltaTime;
            }
            else
            {
                if (!isAnim) return;
                StartCoroutine(BagItem());
            }
        }
        else
        {
            if (value > 0)
            {
                value -= _speed * 2 * Time.deltaTime;
                for (int i = 0; i < 5; i++)
                {
                    item[i].SetActive(isOpenBag);
                }
            }
        }
       
    }

    IEnumerator BagItem()
    {
        for (int i = 0; i < 5; i++)
        {
            item[i].SetActive(isOpenBag);
            yield return new WaitForSeconds(0.1f);
        }
        isAnim = false;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // °ëÍ¸Ã÷ï@Ê¾
        canvasGroup.blocksRaycasts = false; // ÔÊÔS´©Í¸
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        isDragging = false;
    }
}
