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

    void Start()
    {
        ItemIntroduceContent();
    }

    public void OnPointEnter(int _whichItem)
    {
        print("OK");
    }
    public void OnPointExit()
    {
        print("Yes");
    }

    void ItemIntroduceContent()
    {
        introduceItem[1] = "火柴盒";
        introduceItem[2] = "破損的毛衣";
        introduceItem[3] = "垃圾桶的蓋子";
        introduceItem[4] = "小熊娃娃";
        introduceItem[5] = "紅色的小披肩";
        introduceItem[6] = "細長的鐵棒";
        introduceItem[7] = "斷掉的樹枝";
        introduceItem[8] = "乾净的蘋果";
        introduceItem[9] = "烤肉串";
    }
}
