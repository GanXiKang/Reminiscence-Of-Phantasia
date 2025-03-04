using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryItemIntroduce_Momotaro : MonoBehaviour
{
    [Header("IntroduceUI")]
    public GameObject introduce;
    public GameObject panel;
    public Text content;
    string[] introduceItem = new string[12];
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
        introduce.SetActive(isIntroduce);
    }
    public void OnPointExit()
    {
        introduce.SetActive(false);
    }

    void ItemIntroduceContent()
    {
        introduceItem[1] = "糯米糰子\n桃太郎最喜歡的零食";
        introduceItem[2] = "金糯米糰子\n吃了會變出變身糰子";
        introduceItem[3] = "銀糯米糰子\n吃下後就能變身";
        introduceItem[4] = "單支鼓棒\n這樣沒辦法使用";
        introduceItem[5] = "金箍棒\n能伸縮自如的超強武器";
        introduceItem[6] = "銀鼓棒\n很漂亮但依舊只有一支";
        introduceItem[7] = "銀鈴鐺\n鈴聲非常清脆";
        introduceItem[8] = "木製鈴鐺\n感覺小動物會喜歡";
        introduceItem[9] = "笛子\n有些老舊了";
        introduceItem[10] = "銀笛\n可以吹出優美的旋律";
        introduceItem[11] = "金笛\n可以吹出華麗的笛聲";
    }
}