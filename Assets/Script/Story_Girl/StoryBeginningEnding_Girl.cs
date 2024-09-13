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

    [Header("TextFile")]
    public TextAsset textStart;
    public TextAsset textEnding;
    public static bool isStoryEnding = false;
    float _textSpend = 0.1f;
    bool textFinish;
    int _page;

    List<string> textList = new List<string>();

    void Start()
    {
        GetTextFormFile(textStart);
        StartCoroutine(StorySystemUI());
    }

    
    void Update()
    {
        TextController();
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        _page = 0;

        var lineDate = file.text.Split("\n");

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    void TextController()
    {
        if (!textFinish)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _textSpend = 0f;
            }
        }
    }

    public void Button_Left() //上一頁
    {
        if (_page != 0)
        {
            _page--;
            StartCoroutine(StorySystemUI());
        }
    }
    public void Button_Right() //下一頁
    {
        if (_page <= 3)
        {
            _page++;
            StartCoroutine(StorySystemUI());
        }
    }
    IEnumerator StorySystemUI()
    {
        textFinish = false;
        content.text = "";
        for (int i = 0; i < textList[_page].Length; i++)
        {
            content.text += textList[_page][i];
            yield return new WaitForSeconds(_textSpend);
        }
        textFinish = true;
    }
}
