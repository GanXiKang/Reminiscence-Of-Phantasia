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
        introduceItem[1] = "����\n���r������";
        introduceItem[2] = "С��\n߀�]�ӹ��^��С��";
        introduceItem[3] = "����\nһ���K������";
        introduceItem[4] = "�u��\nƿ�b�u��";
        introduceItem[5] = "�����u\nƿ�b�����u";
        introduceItem[6] = "С����\n������ܺó�";
        introduceItem[7] = "���Y��\n�ûجF�ڵĕr�g��";
        introduceItem[8] = "��ʯ\n���ӵ������ϵČ�ʯ";
        introduceItem[9] = "�K��\n�܉��Á�ץס�|��";
        introduceItem[10] = "ľ��\n����͹͹��";
        introduceItem[11] = "������\n�b�������ѵļ���";
        introduceItem[12] = "������\n�b�Й����ѵļ���";
        introduceItem[13] = "������\nƿ�b������";
        introduceItem[14] = "������\nƿ�b������";
        introduceItem[15] = "������\nƿ�b������";
    }
}
