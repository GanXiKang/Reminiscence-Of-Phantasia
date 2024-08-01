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
        introduceItem[1] = "»ð²ñºÐ";
        introduceItem[2] = "ÆÆ“pµÄÃ«ÒÂ";
        introduceItem[3] = "À¬»øÍ°µÄÉw×Ó";
        introduceItem[4] = "Ð¡ÐÜÍÞÍÞ";
        introduceItem[5] = "¼tÉ«µÄÐ¡Åû¼ç";
        introduceItem[6] = "¼šéLµÄèF°ô";
        introduceItem[7] = "”àµôµÄ˜äÖ¦";
        introduceItem[8] = "Ç¬¾»µÄÌO¹û";
        introduceItem[9] = "¿¾Èâ´®";
    }
}
