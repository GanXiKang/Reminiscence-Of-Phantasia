using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBagControl : MonoBehaviour
{
    [Header("BagUI")]
    public Image background;
    public GameObject[] item;
    public float _speed = 1.5f;
    bool isOpenBag = false;
    bool isAnim = false;
    float value = 0;


    void Start()
    {
        
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
        if (!isAnim) return;

        if (isOpenBag)
        {
            if (value < 1)
            {
                value += _speed * Time.deltaTime;
            }
            else
            {
                StartCoroutine(BagItem());
            }
        }
        else
        {
            if (value > 0)
            {
                value -= _speed * Time.deltaTime;
            }
            else 
            {
                StartCoroutine(BagItem());
            }
        }
        //isAnim = false;
    }
    IEnumerator BagItem()
    {
        for (int i = 0; i < 5; i++)
        {
            item[i].SetActive(isOpenBag);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
