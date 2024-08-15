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

    public void OnPointEnter(int _whichItem)
    {
        panel.GetComponent<RectTransform>().position = buttonTransform[_whichItem].position + new Vector3(-80f, 150f, 0f);
        content.text = introduceItem[StoryBagControl._gridsItemNumber[_whichItem]].ToString();
        if (isIntroduce)
            Invoke("IntroduceDisplay", 0.7f);
    }
    public void OnPointExit()
    {
        isIntroduce = false;
        introduce.SetActive(false) ;
    }

    void ItemIntroduceContent()
    {
        introduceItem[1] = "����\n�����cȼ��̕�ȡů";
        introduceItem[2] = "���f��ë��\n��֪��߀�ܲ��ܱ�ů";
        introduceItem[3] = "����Ͱ�w\n���X֮���܉������È�";
        introduceItem[4] = "С������\nС����һ����ϲ�g";
        introduceItem[5] = "С�tñ����\n�Á�ů������";
        introduceItem[6] = "�F��\n������ǳ��Թ�";
        introduceItem[7] = "ľ��\n�������Ǳ������";
        introduceItem[8] = "�O��\n�ɫ�ǳ��r�G";
        introduceItem[9] = "���⴮\n��ζ������Ӷ��I��";
    }
    void IntroduceDisplay()
    {
        if (isIntroduce)
        {
            introduce.SetActive(true);
        }
    }
}
