using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("UITransform")]
    public Transform[] target;
    public static int _isAboveWho = 0;
    bool isPlayerTalk;

    [Header("UIComponents")]
    public Transform dialogueUI;
    public Text content;

    [Header("UIChoose")]
    public Transform chooseUI;
    public Transform originalPos;
    public Transform targetPos;
    public GameObject buttonUI;
    public Text contentA;
    public Text contentB;
    public float _moveSpeed = 500;
    bool isChoose = false;
    bool isChooseUI_Up = false;
    bool isChooseUI_Back = false;
    int _chooseNum;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int _index;
    public float _textSpend;
    public static int _textCount = 1;
    bool textFinish;

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
        if (textFinish)
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
            Vector3 targetPos = Camera.main.WorldToScreenPoint(target[_isAboveWho].position);
            dialogueUI.position = targetPos + offset;
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
                buttonUI.SetActive(true);
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
        buttonUI.SetActive(false);
    }

    IEnumerator SetTextLabelIndexUI()
    {
        textFinish = false;
        switch (textList[_index].Trim())
        {
            case "Player":
                isPlayerTalk = true;
                _index++;
                break;

            case "Target":
                isPlayerTalk = false;
                _index++;
                break;

            case "End":
                DialogurEnd();
                StoryUIControl_Girl.isDialogue = false;
                yield break;

            case "Choose":
                isChoose = true;
                isChooseUI_Up = true;
                _index++;
                contentA.text = textList[_index];
                _index++;
                contentB.text = textList[_index];
                yield break;
        }
        if (!isChoose)
        {
            content.text = "";
            for (int i = 0; i < textList[_index].Length; i++)
            {
                content.text += textList[_index][i];
                yield return new WaitForSeconds(_textSpend);
            }
            textFinish = true;
            _index++;
        }
    }
}
