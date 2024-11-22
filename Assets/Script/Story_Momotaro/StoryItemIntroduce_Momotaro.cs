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
        introduceItem[1] = "Ŵ�׼a��\n��̫����ϲ�g����ʳ";
        introduceItem[2] = "��Ŵ�׼a��\n���˕�׃��׃��a��";
        introduceItem[3] = "�yŴ�׼a��\n���������׃��";
        introduceItem[4] = "��֧�İ�\n�@�ӛ]�k��ʹ��";
        introduceItem[5] = "�𹿰�\n����s����ĳ�������";
        introduceItem[6] = "�y�İ�\n��Ư�������fֻ��һ֧";
        introduceItem[7] = "�y��K\n��ǳ����";
        introduceItem[8] = "ľ�u��K\n���XС�����ϲ�g";
        introduceItem[9] = "����\n��Щ���f��";
        introduceItem[10] = "�y��\n���Դ�������������";
        introduceItem[11] = "���\n���Դ����A���ĵ�";
    }
}