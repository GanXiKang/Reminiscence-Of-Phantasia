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
    public float _speed = 2f;
    public static bool isOpenBag = false;
    public static bool isItemFollow = false;
    bool isAnim = false;
    float value = 0;
    int _whatItem;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
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
    public void ItemA_Button(int _whichItem)
    {
        isItemFollow = true;
        _whatItem = _whichItem;
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
        if (!isItemFollow) return;

        switch (_whatItem)
        {
            case 1:
                item[0].GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;
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
}
