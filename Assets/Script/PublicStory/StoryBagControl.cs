using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoryBagControl : MonoBehaviour
{
    public Camera cam;
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
    public static int _whatItemButton = 5;    //ӛ��Ă�����0~4

    [Header("ItemSprite")]
    public Sprite[] itemSprite;
    public static bool isGet = false;
    public static bool[] isItemNumber;        //ӛ䛓����Ă�����0~9
    public static int[] _gridsItemNumber;     //ӛ�ÿ�������ڲ��ĵ��߾�̖0~4
    public static int _howManyGrids = 0;      //ӛ䛴��ڎׂ�����

    [Header("ItemPickUp")]
    public GameObject moveItemUI;                    
    public Vector3 uiOffset = new Vector3(0, 100, 0); // UI������Ϸ�������ƫ����
    public Transform bagUIPosition;  
    public static bool isPickedUp = false;

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
        PickUpItem();
    }

    public void Bag_Button()
    {
        isOpenBag = !isOpenBag;
        isAnim = true;
    }
    public void Item_Button(int _whichItem)
    {
        isItemFollow = !isItemFollow;
        if (_whatItemButton != _whichItem)
        {
            isItemFollow = true;
        }
        _whatItemButton = _whichItem;
        StoryInteractableControl.isBagGetItem = isItemFollow;
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
            isItemFollow = false;
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
    void PickUpItem()
    {
        if (!isPickedUp) return;

        moveItemUI.SetActive(true); 
        Vector3 startPosition = cam.WorldToScreenPoint(transform.position) + uiOffset;
        moveItemUI.transform.position = startPosition;

        StartCoroutine(MoveItemUI(moveItemUI, startPosition, bagUIPosition.position));
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
    IEnumerator MoveItemUI(GameObject itemUI, Vector3 start, Vector3 end)
    {
        float duration = 1.0f;     //���m�r�g
        float elapsed = 0f;

        while (elapsed < duration)
        {
            moveItemUI.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        moveItemUI.transform.position = end;
        moveItemUI.SetActive(false);
    }
}
