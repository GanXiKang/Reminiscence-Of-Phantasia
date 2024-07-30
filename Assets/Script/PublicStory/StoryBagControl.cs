using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBagControl : MonoBehaviour
{
    [Header("BagUI")]
    public Image background;
    public GameObject[] item;
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
        if (!isAnim) return;

        if (isOpenBag)
        {

        }
        else
        {
            
        }
    }
}
