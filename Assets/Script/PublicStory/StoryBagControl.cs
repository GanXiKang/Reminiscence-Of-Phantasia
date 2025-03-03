using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBagControl : MonoBehaviour
{
    private Canvas canvas;
    private Coroutine currentCoroutine;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip open, item;

    [Header("BagUI")]
    public Image background;
    public Button bag;
    public static bool isOpenBag;
    public float _speed = 2f;
    float value = 0;
    bool isAnim = false;

    [Header("BagButton")]
    public GameObject[] itemButton;
    public GameObject[] itemBG;
    public static bool isItemFollow;
    public static int _whatItemButton;

    [Header("ItemSprite")]
    public Sprite[] itemSprite;
    public static bool isGet;
    public static bool isRenewBag;
    public static bool[] isItemNumber;        
    public static int[] _gridsItemNumber;
    public static int _howManyGrids;

    [Header("GetItemMoveUI")]
    public Image getMoveItem;
    public static int _whichItem;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();

        ItemNumber_Start();
        Static_Start();
    }

    void ItemNumber_Start()
    {
        _howManyGrids = 0;
        isItemNumber = new bool[itemSprite.Length];
        _gridsItemNumber = new int[5];
        for (int i = 0; i < itemSprite.Length; i++)
        {
            isItemNumber[i] = false;
        }
    }
    void Static_Start()
    {
        isOpenBag = false;
        isItemFollow = false;
        isGet = false;
        isRenewBag = false;
        _whatItemButton = 5;
        _howManyGrids = 0;
        _whichItem = 0;
    }

    void Update()
    { 
        BagGirdDisplay();
        RenewBagGirdSprite();
        Bag();
        ItemFollow();
        RenewGetItemMoveUI();
    }

    public void Bag_Button()
    {
        BGM.PlayOneShot(open);
        isOpenBag = !isOpenBag;
        isAnim = true;
    }
    public void Item_Button(int _whichItem)
    {
        BGM.PlayOneShot(item);
        isItemFollow = !isItemFollow;
        if (_whatItemButton != _whichItem)
        {
            isItemFollow = true;
        }
        _whatItemButton = _whichItem;

        StoryInteractableControl_Girl.isBagGetItem = isItemFollow;
        StoryItemIntroduce_Girl.isIntroduce = !isItemFollow;

        StoryInteractableControl_Momotaro.isBagGetItem = isItemFollow;
        StoryItemIntroduce_Momotaro.isIntroduce = !isItemFollow;

        StoryInteractableControl_Prince.isBagGetItem = isItemFollow;
        StoryItemIntroduce_Prince.isIntroduce = !isItemFollow;
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
    void RenewBagGirdSprite()
    {
        if (!isRenewBag) return;

        BagGirdSprite();
        isRenewBag = false;
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
                currentCoroutine = StartCoroutine(BagItem());
            }
            bag.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            isItemFollow = false;
            if (value > 0)
            {
                value -= _speed * 2 * Time.deltaTime;
            }
            else
            {
                bag.GetComponent<CanvasGroup>().alpha = 1;
            }
            for (int i = 0; i < 5; i++)
            {
                itemBG[i].SetActive(false);
                itemButton[i].SetActive(false);
            }
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
        }
    }
    void ItemFollow()
    {
        if (!isItemFollow)
        {
            for (int i = 0; i < 5; i++)
            {
                itemButton[i].GetComponent<RectTransform>().position = itemBG[i].GetComponent<RectTransform>().position;
            }
        }
        else
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out localPoint);
            itemButton[_whatItemButton].GetComponent<RectTransform>().anchoredPosition = localPoint;
            for (int i = 0; i < 5; i++)
            {
                if (i != _whatItemButton)
                {
                    itemButton[i].GetComponent<RectTransform>().position = itemBG[i].GetComponent<RectTransform>().position;
                }
            }
        }
        
    }
    void RenewGetItemMoveUI()
    {
        getMoveItem.sprite = itemSprite[_whichItem];
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
