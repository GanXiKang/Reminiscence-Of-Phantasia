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
    public static bool isOpenBag = false;
    public float _speed = 2f;
    float value = 0;
    bool isAnim = false;

    [Header("BagButton")]
    public GameObject[] itemButton;
    public GameObject[] itemBG;
    public static bool isItemFollow = false;
    public static int _whatItemButton = 5;    //記錄哪個格子0~4

    [Header("ItemSprite")]
    public Sprite[] itemSprite;
    public static bool isGet = false;
    public static bool[] isItemNumber;        //記錄擁有哪個道具0~9
    public static int[] _gridsItemNumber;     //記錄每個格子内部的道具編號0~4
    public static int _howManyGrids = 0;      //記錄存在幾個格子

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        ItemNumber_Start();
    }

    void Update()
    {
        BagGirdDisplay();
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
        if (_whatItemButton != _whichItem)
        {
            isItemFollow = true;
            _whatItemButton = _whichItem;
        }
        else
        {
            isItemFollow = false;
            _whatItemButton = 5;
        }
        StoryInteractableControl.isGetItem = isItemFollow;
        StoryItemIntroduce_Girl.isIntroduce = !isItemFollow;
    }

    void ItemNumber_Start()
    {
        isItemNumber = new bool[itemSprite.Length];
        _gridsItemNumber = new int[5];
        for (int i = 0; i < itemSprite.Length; i++)
        {
            isItemNumber[i] = false;
        }
    }
    void BagGirdDisplay()
    {
        if (isGet)
        {
            _howManyGrids = 0;
            for (int i = 0; i < isItemNumber.Length; i++)
            {
                if (isItemNumber[i])
                    _howManyGrids++;
            print(_howManyGrids);
            }
            if (_howManyGrids > 5)
                _howManyGrids = 5;
            BagGirdSprite();
        isGet = false;
        }
    }
    void BagGirdSprite()
    {
        int gridIndex = 0;
        for (int p = 0; p < isItemNumber.Length; p++)
        {
            if (isItemNumber[p])
            {
                if (gridIndex < _howManyGrids)
                {
                    itemBG[gridIndex].GetComponent<Image>().sprite = itemSprite[p];
                    itemButton[gridIndex].GetComponent<Image>().sprite = itemSprite[p];
                    _gridsItemNumber[gridIndex] = p;
                    gridIndex++;
                }
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
                for (int i = 0; i < _howManyGrids; i++)
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
            if (i != _whatItemButton)
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
        itemButton[_whatItemButton].GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    IEnumerator BagItem()
    {
        for (int i = 0; i < _howManyGrids; i++)
        {
            itemBG[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            itemButton[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        isAnim = false;
    }
}
