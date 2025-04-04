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
    public static int _enterCount;
    bool isEnterOnce = true;

    [Header("Coin")]
    public Text coinAmount;
    public Button buyButton;
    int _productCoin = 0;

    [Header("ProductContent")]
    public GameObject[] scissors;
    public GameObject[] shading;
    int _productNum;

    //Plot
    public static bool isPlotBut = false;

    void Update()
    {
        storeUI.SetActive(isStoreActive);
        scissorsUI.SetActive(_enterCount == 1);
        shadingUI.SetActive(_enterCount > 1);

        OpenUI();
        Coin();
    }

    void OpenUI()
    {
        if (!isStoreActive) return;

        if (isEnterOnce)
        {
            _enterCount++;
            _productCoin = 0;
            CatControl_House.isBag = true;
            isEnterOnce = false;
        }

        if (storeUI.GetComponent<RectTransform>().localScale.x < 1)
            storeUI.GetComponent<RectTransform>().localScale += new Vector3(2f, 2f, 0f) * Time.deltaTime;
    }
    void Coin()
    {
        coinAmount.text = GameControl_House._MyCoin + " / " + _productCoin;
        buyButton.interactable = GameControl_House._MyCoin >= _productCoin;
        if (_productCoin == 0)
            buyButton.interactable = false;
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
            _productNum = _content;
        }
    }
    public void Button_Info(int _num)
    {
        BGM.PlayOneShot(info);
        if (scissorsUI.activeSelf)
        {
            scissors[_num + 2].SetActive(scissors[_num].activeSelf);
            scissors[_num].SetActive(!scissors[_num].activeSelf);
        }
        else if (shadingUI.activeSelf)
        {
            switch (_num)
            {
                case 1:
                    shading[3].SetActive(shading[1].activeSelf);
                    shading[1].SetActive(!shading[1].activeSelf);
                    break;

                case 2:
                    shading[7].SetActive(shading[5].activeSelf);
                    shading[5].SetActive(!shading[5].activeSelf);
                    break;

                case 3:
                    shading[11].SetActive(shading[9].activeSelf);
                    shading[9].SetActive(!shading[9].activeSelf);
                    break;

                case 4:
                    shading[15].SetActive(shading[13].activeSelf);
                    shading[13].SetActive(!shading[13].activeSelf);
                    break;

                case 5:
                    shading[19].SetActive(shading[17].activeSelf);
                    shading[17].SetActive(!shading[17].activeSelf);
                    break;

                case 6:
                    shading[23].SetActive(shading[21].activeSelf);
                    shading[21].SetActive(!shading[21].activeSelf);
                    break;

                case 7:
                    shading[27].SetActive(shading[25].activeSelf);
                    shading[25].SetActive(!shading[25].activeSelf);
                    break;

                case 8:
                    shading[31].SetActive(shading[29].activeSelf);
                    shading[29].SetActive(!shading[29].activeSelf);
                    break;

                case 9:
                    shading[35].SetActive(shading[33].activeSelf);
                    shading[33].SetActive(!shading[33].activeSelf);
                    break;
            }
        }
    }
    public void Button_Buy()
    {
        BGM.PlayOneShot(buy);
        GameControl_House._MyCoin -= _productCoin;
        _productCoin = 0;
        _productNum = 0;
        CatControl_House.isHappy = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 4;

        if (scissorsUI.activeSelf)
        {
            isPlotBut = true;
            for (int i = 1; i < scissors.Length; i++)
                scissors[i].GetComponent<Button>().interactable = false;
        }
        else if (shadingUI.activeSelf)
        {
            WorkbenchControl_House.isRenewColorLock = true;
            WorkbenchControl_House.isColorUnlock[_productNum] = true;
            switch (_productNum)
            {
                case 1:
                    for (int i = 1; i <= 4; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 2:
                    for (int i = 5; i <= 8; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 3:
                    for (int i = 9; i <= 12; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 4:
                    for (int i = 13; i <= 16; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 5:
                    for (int i = 17; i <= 20; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 6:
                    for (int i = 21; i <= 24; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 7:
                    for (int i = 25; i <= 28; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 8:
                    for (int i = 29; i <= 32; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;

                case 9:
                    for (int i = 31; i <= 36; i++)
                        shading[i].GetComponent<Button>().interactable = false;
                    break;
            }
        }
    }
    public void Button_Leave()
    {
        if (!isPlotBut) return;

        BGM.PlayOneShot(leave);
        CatControl_House.isBag_On = true;
        CatControl_House.isBye = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 6;
        Invoke("LeaveState", 1f);
    }

    void LeaveState()
    {
        isEnterOnce = true;
        DoorControl_House.isLeave = true;
        DialogueControl_House.isAutoNext = true;
        DialogueControl_House._paragraph = 7;
        storeUI.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);

        if (_enterCount == 1)
            UIAboveObject_House.isStoreHintActive = false;
    }
}
