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
        introduceItem[1] = "\n";
        introduceItem[2] = "\n";
        introduceItem[3] = "\n";
        introduceItem[4] = "\n";
        introduceItem[5] = "\n";
        introduceItem[6] = "\n";
        introduceItem[7] = "\n";
        introduceItem[8] = "\n";
        introduceItem[9] = "\n";
    }
}
