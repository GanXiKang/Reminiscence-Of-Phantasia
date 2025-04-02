using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIControl_House : MonoBehaviour
{
    public GameObject coinBG;
    public Text coinText;
    public Transform inPoint, outPoint;
    float _speed = 2f;
    bool isIn = false;
    bool isOut = false;

    void OnEnable()
    {
        isIn = true;
    }

    void Update()
    {
        InAnimation();
        OutAnimation();
    }

    void InAnimation()
    {
        if (!isIn) return;

        if (Vector3.Distance(coinBG.transform.position, outPoint.position) > 0.01f)
        {
            coinBG.transform.position = Vector3.MoveTowards(coinBG.transform.position, outPoint.position, _speed);
        }
        else
        {
            isIn = false;
        }
    }
    void OutAnimation()
    {
        if (!isOut) return;

        if (Vector3.Distance(coinBG.transform.position, inPoint.position) > 0.01f)
        {
            coinBG.transform.position = Vector3.MoveTowards(coinBG.transform.position, inPoint.position, _speed);
        }
        else
        {
            isOut = false;
            UIControl_House.isCoinAppear = false;
        }
    }
}
