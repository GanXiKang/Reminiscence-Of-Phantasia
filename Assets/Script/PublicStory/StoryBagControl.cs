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
            StartCoroutine(OpenBag());
        }
        else
        {
            StartCoroutine(CloseBag());
        }
        isAnim = false;
    }
    IEnumerator OpenBag()
    {
        if (value < 1)
        {
            value += _speed * Time.deltaTime;
        }
        else 
        {
            for (int i = 0; i < 5; i++)
            {
                item[i].SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
        }
        
    }
    IEnumerator CloseBag()
    {
        if (value < 1)
        {
            value += _speed * Time.deltaTime;
            for (int i = 0; i < 5; i++)
            {
                item[i].SetActive(false);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
