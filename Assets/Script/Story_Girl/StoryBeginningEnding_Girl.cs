using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBeginningEnding_Girl : MonoBehaviour
{
    [Header("UI")]
    public GameObject storyUI;
    public Text content;
    public static bool isStory;
    int _page = 0;

    [Header("TextFile")]
    public TextAsset textStart;
    public TextAsset textEnding;
    bool isStoryStart = true;

    void Start()
    {
        isStoryStart = true;
    }

    
    void Update()
    {
        
    }

    public void Button_Left() //上一頁
    {
        
    }
    public void Button_Right() //下一頁
    {

    }
}
