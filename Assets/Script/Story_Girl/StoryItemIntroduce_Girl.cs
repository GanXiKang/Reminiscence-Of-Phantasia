using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryItemIntroduce_Girl : MonoBehaviour
{
    [Header("IntroduceUI")]
    public GameObject introduce;
    public GameObject panel;
    public Text content;
    string[] introduceItem = new string[10];
    public static bool isIntroduce = true;

    [Header("IntroduceTransform")]
    public Transform[] buttonTransform;

    void Start()
    {
        ItemIntroduceContent();
    }

    void Update()
    {
        if(!StoryBagControl.isOpenBag && introduce.activeSelf)
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
        introduceItem[1] = "火柴盒\n可以點燃來短暫取暖";
        introduceItem[2] = "破舊的毛衣\n不知道還能不能保暖";
        introduceItem[3] = "垃圾桶蓋\n感覺之後能夠派上用場";
        introduceItem[4] = "小熊娃娃\n小朋友一定很喜歡";
        introduceItem[5] = "小紅帽披肩\n用來保暖剛剛好";
        introduceItem[6] = "鐵棒\n摸起來非常堅固";
        introduceItem[7] = "木棒\n看起來是被砍斷的";
        introduceItem[8] = "蘋果\n顏色非常鮮艷";
        introduceItem[9] = "烤肉串\n香味四溢肚子都餓了";
    }
}
