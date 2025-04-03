using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIControl_House : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource BGM;
    public AudioClip coin;

    [Header("CoinUI")]
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
        isOut = false;
        isAdd = false;
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
            BGM.PlayOneShot(coin);
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

        int _coinTotal = _coinValue + _coinTarget;
        _coinValue = Mathf.RoundToInt(Mathf.MoveTowards(_coinValue, _coinTotal, _speed * 3 * Time.deltaTime));
        coinText.text = _coinValue.ToString();

        if (_coinValue == _coinTotal)
        {
            isAdd = false;
            GameControl_House._MyCoin = _coinTotal;
            Invoke("StartOutAnimation", 2f);
        }
    }
    void StartOutAnimation()
    {
        isOut = true;
    }
}
