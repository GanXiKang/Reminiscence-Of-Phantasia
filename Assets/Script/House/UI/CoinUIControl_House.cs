using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIControl_House : MonoBehaviour
{
    public GameObject coinBG;
    public Text coinText;
    public Transform inPoint, outPoint;
    public static int _coinTarget;
    float _speed = 500f;
    bool isIn;
    bool isOut;
    bool isAdd;
    int _coinValue;

    void OnEnable()
    {
        isIn = true;
        bool isOut = false;
        bool isAdd = false;
        _coinValue = GameControl_House._MyCoin;
        coinText.text = _coinValue.ToString();
    }

    void Update()
    {
        InAnimation();
        OutAnimation();
        CoinCountAdd();
    }

    void InAnimation()
    {
        if (!isIn) return;

        if (Vector3.Distance(coinBG.transform.position, inPoint.position) > 0.01f)
        {
            coinBG.transform.position = Vector3.MoveTowards(coinBG.transform.position, inPoint.position, _speed * Time.deltaTime);
        }
        else
        {
            isIn = false;
            isAdd = true;
        }
    }
    void OutAnimation()
    {
        if (!isOut) return;

        if (Vector3.Distance(coinBG.transform.position, outPoint.position) > 0.01f)
        {
            coinBG.transform.position = Vector3.MoveTowards(coinBG.transform.position, outPoint.position, _speed * Time.deltaTime);
        }
        else
        {
            isOut = false;
            UIControl_House.isCoinAppear = false;
        }
    }
    void CoinCountAdd()
    {
        if (!isAdd) return;

        print("1");
        _coinValue = Mathf.RoundToInt(Mathf.MoveTowards(_coinValue, _coinTarget, _speed * Time.deltaTime));
        if (_coinValue == _coinTarget)
        {
            print("2");
            isAdd = false;
            GameControl_House._MyCoin = _coinValue;
            Invoke("StartOutAnimation", 2f);
        }
    }
    void StartOutAnimation()
    {
        isOut = true;
    }
}
