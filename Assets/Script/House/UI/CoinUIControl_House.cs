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
    float _speed = 250f;
    bool isIn = false;
    bool isOut = false;
    bool isAdd = false;
    int _coinValue;

    void OnEnable()
    {
        isIn = true;
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

        if (Vector3.Distance(coinBG.transform.position, outPoint.position) > 0.01f)
        {
            coinBG.transform.position = Vector3.MoveTowards(coinBG.transform.position, outPoint.position, _speed * Time.deltaTime);
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

        if (Vector3.Distance(coinBG.transform.position, inPoint.position) > 0.01f)
        {
            coinBG.transform.position = Vector3.MoveTowards(coinBG.transform.position, inPoint.position, _speed * Time.deltaTime);
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

        _coinValue = Mathf.RoundToInt(Mathf.MoveTowards(_coinValue, _coinTarget, _speed * Time.deltaTime));
        if (_coinValue == _coinTarget)
        {
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
