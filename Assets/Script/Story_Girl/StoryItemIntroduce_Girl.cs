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
        introduceItem[1] = "����";
        introduceItem[2] = "�Ɠp��ë��";
        introduceItem[3] = "����Ͱ���w��";
        introduceItem[4] = "С������";
        introduceItem[5] = "�tɫ��С����";
        introduceItem[6] = "���L���F��";
        introduceItem[7] = "����Ę�֦";
        introduceItem[8] = "Ǭ�����O��";
        introduceItem[9] = "���⴮";
    }

    IEnumerator IntroduceDisplay()
    {
        yield return new WaitForSeconds(0.7f);
        introduce.SetActive(true);
    }
}
