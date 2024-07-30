using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoryBagControl : MonoBehaviour
{
    private Canvas canvas;

    [Header("BagUI")]
    public Image background;
    public GameObject[] item;
    public GameObject[] itemBG;
    public float _speed = 2f;
    public static bool isOpenBag = false;
    public static bool isItemFollow = false;
    bool isAnim = false;
    float value = 0;
    int _whatItem = 5;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        print(_whatItem);
    }

    void Update()
    {
        Bag();
        ItemMove();
    }

    public void Bag_Button()
    {
        isOpenBag = !isOpenBag;
        isAnim = true;
    }
    public void Item_Button(int _whichItem)
    {
        if (_whatItem != _whichItem)
        {
            isItemFollow = true;
            _whatItem = _whichItem;
        }
        else
        {
            isItemFollow = false;
            _whatItem = 5;
        }
        StoryInteractableControl.isGet = isItemFollow;
        print(_whatItem);
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
    void ItemMove()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    if (i == _whatItem) return;
        //    item[i].GetComponent<RectTransform>().position = itemBG[i].GetComponent<RectTransform>().position;
        //}

        if (!isItemFollow) return;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint);
        item[_whatItem].GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    IEnumerator BagItem()
    {
        for (int i = 0; i < 5; i++)
        {
            itemBG[i].SetActive(true);
            yield return new WaitForSeconds(0.05f);
            item[i].SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
        isAnim = false;
    }
}
