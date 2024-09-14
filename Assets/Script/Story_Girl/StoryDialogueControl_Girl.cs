using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("UITransform")]
    public Transform[] target;
    public static int _isAboveWho1 = 0;
    public static int _isAboveWho2 = 0;
    bool isPlayerTalk;
    int _targetNum = 0;

    [Header("UIComponents")]
    public Transform dialogueUI;
    public Text content;

    [Header("UIChoose")]
    public Transform chooseUI;
    public Transform originalPos;
    public Transform targetPos;
    public GameObject button2UI, button3UI;
    public Text contentA, contentB, contentC, contentD, contentE;
    public float _moveSpeed = 500;
    bool isChoose = false;
    bool isChooseUI_Up = false;
    bool isChooseUI_Back = false;
    int _buttonNum;
    int _chooseNum;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int _index;
    public float _textSpend;
    public static int _textCount = 1;
    bool isTextFinish;
    bool isInsideTag = false;
    string tagContent = "";

    List<string> textList = new List<string>();

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        GetTextFormFile(textFile[_textCount]);
        StartCoroutine(SetTextLabelIndexUI());
    }

    void Update()
    {
        TextController();
        TextPosition();
        ChooseUI();
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
    void TextPosition()
    {
        Vector3 offset = new Vector3(-120f, 300f, 0f);
        if (isPlayerTalk)
        {
            Vector3 playerPos = Camera.main.WorldToScreenPoint(player.transform.position);
            dialogueUI.position = playerPos + offset;
        }
        else
        {
            if (_targetNum == 1)
            {
                Vector3 targetPos = Camera.main.WorldToScreenPoint(target[_isAboveWho1].position);
                dialogueUI.position = targetPos + offset;
            }
            else
            {
                Vector3 targetPos = Camera.main.WorldToScreenPoint(target[_isAboveWho2].position);
                dialogueUI.position = targetPos + offset;
            }
        }
    }
    void DialogurEnd()
    {
        switch (_textCount)
        {
            case 1:
                if (_chooseNum == 1)
                {
                    StoryThermometerControl_Girl.isThermometer = true;
                }
                break;   
        }
    }
    void ChooseUI()
    {
        if (!isChoose) return;

        if (isChooseUI_Up)
        {
            chooseUI.position = Vector3.MoveTowards(chooseUI.position, targetPos.position, _moveSpeed * Time.deltaTime);
            if (chooseUI.position == targetPos.position)
            {
                isChooseUI_Up = false;
                if (_buttonNum == 2)
                {
                    button2UI.SetActive(true);
                }
                else 
                {
                    button3UI.SetActive(true);
                }
            }
        }
        if (isChooseUI_Back)
        {
            chooseUI.position = Vector3.MoveTowards(chooseUI.position, originalPos.position, _moveSpeed * 2f * Time.deltaTime);
            if (chooseUI.position == originalPos.position)
            {
                isChooseUI_Back = false;
                isChoose = false;
                if (_chooseNum == 1)
                {
                    JumpToSection("A"); 
                }
                else if (_chooseNum == 2)
                {
                    JumpToSection("B");
                }
                else if (_chooseNum == 3)
                {
                    JumpToSection("C");
                }
                else if (_chooseNum == 4)
                {
                    JumpToSection("D");
                }
                else if (_chooseNum == 5)
                {
                    JumpToSection("E");
                }
            }
        }
    }
    void JumpToSection(string sectionLabel)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            if (textList[i].Trim() == sectionLabel)
            {
                _index = i + 1;
                StartCoroutine(SetTextLabelIndexUI());
                return;
            }
        }
    }

    public void ChooseButton(int _chooseButton)
    {
        _chooseNum = _chooseButton;
        isChooseUI_Back = true;
        if (_chooseButton <= 2)
        {
            button2UI.SetActive(false);
        }
        else
        {
            button3UI.SetActive(false);
        }
    }

    IEnumerator SetTextLabelIndexUI()
    {
        isTextFinish = false;
        switch (textList[_index].Trim())
        {
            case "Player":
                isPlayerTalk = true;
                _index++;
                break;

            case "Target":
                isPlayerTalk = false;
                _targetNum = 1;
                _index++;
                break;

            case "Target2":
                isPlayerTalk = false;
                _targetNum = 2;
                _index++;
                break;

            case "End":
                DialogurEnd();
                StoryUIControl_Girl.isDialogue = false;
                yield break;

            case "Choose2":
                isChoose = true;
                isChooseUI_Up = true;
                _buttonNum = 2;
                _index++;
                contentA.text = textList[_index];
                _index++;
                contentB.text = textList[_index];
                yield break;

            case "Choose3":
                isChoose = true;
                isChooseUI_Up = true;
                _buttonNum = 3;
                _index++;
                contentC.text = textList[_index];
                _index++;
                contentD.text = textList[_index];
                _index++;
                contentE.text = textList[_index];
                yield break;

            case "Event":
                break;
        }
        if (!isChoose)
        {
            content.text = "";
            for (int i = 0; i < textList[_index].Length; i++)
            {
                if (textList[_index][i] == '<')
                {
                    isInsideTag = true;
                    tagContent = "";
                }

                if (isInsideTag)
                {
                    tagContent += textList[_index][i];
                    if (textList[_index][i] == '>')
                    {
                        isInsideTag = false;
                        content.text += tagContent;
                    }
                }
                else
                {
                    content.text += textList[_index][i];
                    yield return new WaitForSeconds(_textSpend);
                }
            }
            isTextFinish = true;
            _index++;
        }
    }
}
