using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] whoDialogue;
    public Text[] whoContent;
    public static int _whoDialogue;
    public static bool isBird;

    [Header("Position")]
    public Transform[] dialoguePos;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int _index;
    public float _textSpend;
    public static int _textCount = 1;
    bool isTextFinish;

    List<string> textList = new List<string>();

    void Start()
    {
        UIControl_House.isDialogue = true;
        isBird = false;
    }

    void OnEnable()
    {
        GetTextFormFile(textFile[_textCount]);
        StartCoroutine(SetTextLabelIndexUI());
    }

    void Update()
    {
        TextController();
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        _index = 0;

        var lineDate = file.text.Split("\n");

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    void TextController()
    {
        if (SettingControl.isSettingActive) return;

        if (isTextFinish)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _textSpend = 0.1f;
                StartCoroutine(SetTextLabelIndexUI());
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _textSpend = 0f;
            }
        }
    }

    IEnumerator SetTextLabelIndexUI()
    {
        isTextFinish = false;
        switch (textList[_index].Trim())
        {
            case "Player":
                _whoDialogue = 1;
                _index++;
                break;

            case "Target":
                if (isBird)
                {
                    _whoDialogue = 2;
                }
                else
                {
                    _whoDialogue = 3;
                }
                _index++;
                break;

            case "Event":
                _index++;
                break;
        }
        whoContent[_whoDialogue].text = "";
        for (int i = 0; i < textList[_index].Length; i++)
        {
            whoContent[_whoDialogue].text += textList[_index][i];
            yield return new WaitForSeconds(_textSpend);
        }
        isTextFinish = true;
        _index++;
    }
}
