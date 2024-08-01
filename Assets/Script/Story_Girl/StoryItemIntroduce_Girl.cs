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

    [Header("IntroduceTransform")]
    public Transform[] buttonTransform;

    void Start()
    {
        ItemIntroduceContent();
    }

    public void OnPointEnter(int _whichItem)
    {
        introduce.SetActive(true);
        switch (_whichItem)
        {
            case 0:
                introduce.GetComponent<RectTransform>().position = buttonTransform[0].position;
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;
        }
    }
    public void OnPointExit()
    {
        introduce.SetActive(false);
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
}
