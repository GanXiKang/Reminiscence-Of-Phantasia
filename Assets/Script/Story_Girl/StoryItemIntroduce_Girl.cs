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
        if (!isIntroduce)
            StopCoroutine(IntroduceDisplay());
    }

    public void OnPointEnter(int _whichItem)
    {
        panel.GetComponent<RectTransform>().position = buttonTransform[_whichItem].position + new Vector3(-80f, 150f, 0f);
        content.text = introduceItem[StoryBagControl._gridsItemNumber[_whichItem]].ToString();
        if (isIntroduce)
            StartCoroutine(IntroduceDisplay());
    }
    public void OnPointExit()
    {
        introduce.SetActive(false) ;
        StopCoroutine(IntroduceDisplay());
    }

    void ItemIntroduceContent()
    {
        introduceItem[1] = "火柴盒\n可以點燃來短暫取暖";
        introduceItem[2] = "破損的毛衣";
        introduceItem[3] = "垃圾桶的蓋子";
        introduceItem[4] = "小熊娃娃";
        introduceItem[5] = "紅色的小披肩";
        introduceItem[6] = "細長的鐵棒";
        introduceItem[7] = "斷掉的樹枝";
        introduceItem[8] = "乾净的蘋果";
        introduceItem[9] = "烤肉串";
    }

    IEnumerator IntroduceDisplay()
    {
        yield return new WaitForSeconds(0.7f);
        introduce.SetActive(true);
    }
}
