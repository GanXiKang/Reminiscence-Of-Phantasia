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

    IEnumerator IntroduceDisplay()
    {
        yield return new WaitForSeconds(0.7f);
        introduce.SetActive(true);
    }
}
