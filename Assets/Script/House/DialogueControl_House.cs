using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl_House : MonoBehaviour
{
    [Header("UI")]
    public RectTransform[] whoDialogue;
    public Text[] whoContent;
    public static int _whoDialogue = 1;
    public static bool isBird = false;

    [Header("Position")]
    public RectTransform[] dialoguePos;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int _index;
    public float _textSpend;
    public static int _textCount = 1;
    bool isTextFinish;

    List<string> textList = new List<string>();

    void OnEnable()
    {
        GetTextFormFile(textFile[_textCount]);
        //StartCoroutine(SetTextLabelIndexUI());
        StartCoroutine(UIMoveToPosition(whoDialogue[1], dialoguePos[1].anchoredPosition));
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
                //_whoDialogue = 1;
                //StartCoroutine(UIMoveToPosition(whoDialogue[1], dialoguePos[1].anchoredPosition));
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
    IEnumerator UIMoveToPosition(RectTransform message, Vector2 targetPos)
    {
        Vector2 startPos = message.anchoredPosition;
        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            message.anchoredPosition = Vector2.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        message.anchoredPosition = targetPos;
    }
}
