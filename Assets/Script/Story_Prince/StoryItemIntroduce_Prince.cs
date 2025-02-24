using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryItemIntroduce_Prince : MonoBehaviour
{
    [Header("IntroduceUI")]
    public GameObject introduce;
    public GameObject panel;
    public Text content;
    string[] introduceItem = new string[14];
    public static bool isIntroduce = true;

    [Header("IntroduceTransform")]
    public Transform[] buttonTransform;

    void Start()
    {
        ItemIntroduceContent();
    }

    void Update()
    {
        if (!StoryBagControl.isOpenBag && introduce.activeSelf)
            introduce.SetActive(false);
    }

    public void OnPointEnter(int _whichItem)
    {
        panel.GetComponent<RectTransform>().position = buttonTransform[_whichItem].position + new Vector3(-80f, 150f, 0f);
        content.text = introduceItem[StoryBagControl._gridsItemNumber[_whichItem]].ToString();
        if (isIntroduce)
        {
            introduce.SetActive(true);
        }
        else
        {
            introduce.SetActive(false);
        }
    }
    public void OnPointExit()
    {
        introduce.SetActive(false);
    }

    void ItemIntroduceContent()
    {
        introduceItem[1] = "玉米\n新鮮的玉米";
        introduceItem[2] = "小麥\n還沒加工過的小麥";
        introduceItem[3] = "奶油\n一整塊的奶油";
        introduceItem[4] = "醬油\n瓶裝醬油";
        introduceItem[5] = "番茄醬\n瓶裝番茄醬";
        introduceItem[6] = "小蛋糕\n看起來很好吃";
        introduceItem[7] = "物資箱\n拿回現在的時間吧";
        introduceItem[8] = "寶石\n王子雕像身上的寶石";
        introduceItem[9] = "繩索\n能夠用來抓住東西";
        introduceItem[10] = "木板\n凹凹凸凸的";
        introduceItem[11] = "葡萄籽\n裝有葡萄籽的紙袋";
        introduceItem[12] = "櫻桃籽\n裝有櫻桃籽的紙袋";
        introduceItem[13] = "美乃滋\n瓶裝美乃滋";
        introduceItem[14] = "美乃滋\n瓶裝美乃滋";
        introduceItem[15] = "美乃滋\n瓶裝美乃滋";
    }
}
