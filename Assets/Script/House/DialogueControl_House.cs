using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl_House : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] whoDialogue;
    public Text[] whoContent;
    public static int _whoDia;
    public static bool isBird = false;

    [Header("Position")]
    public Transform[] dialoguePos;
    float _moveSpeed = 300f;
    bool isMove;

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
        StartCoroutine(SetTextLabelIndexUI());
    }

    void Update()
    {
        TextController();
        DialogueMove();
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
        AvatarControl_House.isTalk = !isTextFinish;
    }
    void DialogueMove()
    {
        if (!isMove) return;

        if (whoDialogue[_whoDia].transform.position == dialoguePos[1].position)
        {
            isMove = false;
        }
        else
        {
            whoDialogue[_whoDia].transform.position = Vector3.MoveTowards(whoDialogue[_whoDia].transform.position, dialoguePos[1].position, _moveSpeed * Time.deltaTime);
            for (int w = 0; w < dialoguePos.Length; w++)
            {
                if (whoDialogue[w].transform.position == dialoguePos[1].position)
                {
                    whoDialogue[w].transform.position = Vector3.MoveTowards(whoDialogue[w].transform.position, dialoguePos[2].position, _moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    IEnumerator SetTextLabelIndexUI()
    {
        isTextFinish = false;
        switch (textList[_index].Trim())
        {
            case "Player":
                _whoDia = 0;
                isMove = true;
                whoDialogue[_whoDia].transform.position = dialoguePos[0].position;
                _index++;
                break;

            case "Target":
                if (isBird)
                {
                    _whoDia = 1;
                    isMove = true;
                    whoDialogue[_whoDia].transform.position = dialoguePos[0].position;
                }
                else
                {
                    _whoDia = 2;
                    isMove = true;
                    whoDialogue[_whoDia].transform.position = dialoguePos[0].position;
                }
                _index++;
                break;

            case "Event":
                _index++;
                break;

            case "End":
                UIControl_House.isDialogue = false;
                for (int i = 0; i < whoDialogue.Length; i++)
                {
                    whoDialogue[i].transform.position = dialoguePos[0].position;
                }
                break;
        }
        whoContent[_whoDia].text = "";
        for (int i = 0; i < textList[_index].Length; i++)
        {
            whoContent[_whoDia].text += textList[_index][i];
            yield return new WaitForSeconds(_textSpend);
        }
        isTextFinish = true;
        _index++;
    }
}
