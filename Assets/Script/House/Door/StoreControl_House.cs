using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip leave, back, comfirm, info, buy;

    [Header("UI")]
    public GameObject storeUI;
    public GameObject scissorsUI;
    public GameObject shadingUI;
    public static bool isStoreActive = false;
    bool isContentActive = false;

    [Header("Coin")]
    public Text coinAmount;
    public Button buyButton;
    int _productCoin = 0;
    bool isBuy = false;

    [Header("ProductContent")]
    public GameObject[] scissors;
    public GameObject[] shading;

    void Update()
    {
        storeUI.SetActive(isStoreActive);

        OpenUI();
        Coin();
    }

    void OpenUI()
    {
        if (!isStoreActive) return;

        if (storeUI.GetComponent<RectTransform>().localScale.x < 1)
            storeUI.GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
    }
    void Coin()
    {
        coinAmount.text = GameControl_House._MyCoin + " / " + _productCoin;
        buyButton.interactable = GameControl_House._MyCoin >= _productCoin;
        if (_productCoin == 0)
        {
            buyButton.interactable = false;
        }
    }

    public void Button_Product(int _product)
    {
        BGM.PlayOneShot(comfirm);
        _productCoin = 0;
        CatControl_House.isBag = true;
    }
    public void Button_Content(int _content)
    {
        BGM.PlayOneShot(comfirm);
        if (scissorsUI.activeSelf)
        {
            _productCoin = 250;
        }
        else if (shadingUI.activeSelf)
        {
            _productCoin = 350;
        }
    }
    public void Button_Info(int _num)
    {
        BGM.PlayOneShot(info);
        if (scissorsUI.activeSelf)
        {
            scissors[_num].SetActive(!scissors[_num].activeSelf);
            scissors[_num].SetActive(scissors[_num].activeSelf);
        }
        else if (shadingUI.activeSelf)
        {
            switch (_num)
            {
                case 1:

                    break;
            }
        }
    }
    public void Button_Buy()
    {
        BGM.PlayOneShot(buy);
        isBuy = true;
        GameControl_House._MyCoin -= _productCoin;
        CatControl_House.isHappy = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 4;

        if (scissorsUI.activeSelf)
        {
            for (int i = 1; i < scissors.Length; i++)
                scissors[i].GetComponent<Button>().interactable = false;
        }
        else if (shadingUI.activeSelf)
        {
            
        }
    }
    public void Button_Back()
    {
        BGM.PlayOneShot(back);
        isContentActive = false;
        CatControl_House.isBag_On = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 5;
    }
    public void Button_Leave()
    {
        BGM.PlayOneShot(leave);
        CatControl_House.isBye = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 6;
        Invoke("LeaveState", 1f);
    }

    void LeaveState()
    {
        DoorControl_House.isLeave = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 7;
        isContentActive = false;
        storeUI.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
    }
}
