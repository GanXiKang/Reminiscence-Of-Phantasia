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
    public Sprite[] item;
    public GameObject[] itemButton;
    public GameObject[] itemBG;
    public float _speed = 2f;
    public static bool isOpenBag = false;
    public static bool isItemFollow = false;
    public static bool isGet = false;
    public static int _whichItemToGet;
    float value = 0;
    bool isAnim = false;
    int _whatItem = 5;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();

        isGet = true;
        _whichItemToGet = 1;
    }

    void Update()
    {
        BagDisplay();
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
    }

    void BagDisplay()
    {
        if (!isGet) return;

        for (int i = 0; i < 5; i++)
        {
            print("In");
            if (itemBG[i].GetComponent<Image>().sprite != null)
            {
                print("Get");
            }
            else
            {
                print("OK");
                itemBG[i].GetComponent<Image>().sprite = item[_whichItemToGet];
                itemButton[i].GetComponent<Image>().sprite = item[_whichItemToGet];
                isGet = true;
            }
        }
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
                    itemBG[i].SetActive(false);
                    itemButton[i].SetActive(false);
                }
            }
        }
    }
    void ItemMove()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i != _whatItem)
            {
                itemButton[i].GetComponent<RectTransform>().position = itemBG[i].GetComponent<RectTransform>().position;
            }
        }

        if (!isItemFollow) return;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint);
        itemButton[_whatItem].GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    IEnumerator BagItem()
    {
        for (int i = 0; i < 5; i++)
        {
            itemBG[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            itemButton[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        isAnim = false;
    }
}
