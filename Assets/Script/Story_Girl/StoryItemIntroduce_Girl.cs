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
        introduceItem[1] = 
    }
}
