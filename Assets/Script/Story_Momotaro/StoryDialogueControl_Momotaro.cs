using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip give;

    [Header("UITransform")]
    public Transform[] target;
    public static int _isAboveWho1 = 0;
    public static int _isAboveWho2 = 0;
    bool isPlayerTalk;
    int _targetNum = 0;
    int _countEvent = 0;

    [Header("UIComponents")]
    public Transform dialogueUI;
    public Image dialogueBG;
    public Text content;
    public Sprite normal, spiked;

    [Header("UIChoose")]
    public GameObject chooseUI;
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

    List<string> textList = new List<string>();

    //Event
    public static bool isDialogueEvent = false;
    public static bool isDialogueRotation = false;

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
    void TextPosition()
    {
        Vector3 offset = new Vector3(0f, 250f, 0f);
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

    void ChooseUI()
    {
        chooseUI.SetActive(isChoose);

        if (!isChoose) return;

        if (isChooseUI_Up)
        {
            chooseUI.transform.position = Vector3.MoveTowards(chooseUI.transform.position, targetPos.position, _moveSpeed * Time.deltaTime);
            if (chooseUI.transform.position == targetPos.position)
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
            chooseUI.transform.position = Vector3.MoveTowards(chooseUI.transform.position, originalPos.position, _moveSpeed * 2f * Time.deltaTime);
            if (chooseUI.transform.position == originalPos.position)
            {
                isChooseUI_Back = false;
                isChoose = false;
                if (_chooseNum == 1)
                {
                    JumpToSection("A");
                }
                else if (_chooseNum == 2)
                {
                    if (_textCount == 47)
                    {
                        JumpToSection("A");
                    }
                    else
                    {
                        JumpToSection("B");
                    }
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
                _textSpend = 0.1f;
                StartCoroutine(SetTextLabelIndexUI());
                return;
            }
        }
    }

    IEnumerator SetTextLabelIndexUI()
    {
        isTextFinish = false;
        switch (textList[_index].Trim())
        {
            case "Normal":
                dialogueBG.sprite = normal;
                _index++;
                break;

            case "Spiked":
                dialogueBG.sprite = spiked;
                _index++;
                break;

            case "Event":
                DialogueEvent();
                _index++;
                break;
        }
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
                StoryUIControl_Momotaro.isDialogue = false;
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
        }
        if (!isChoose)
        {
            content.text = "";
            for (int i = 0; i < textList[_index].Length; i++)
            {
                content.text += textList[_index][i];
                yield return new WaitForSeconds(_textSpend);
            }
            isTextFinish = true;
            _index++;
        }
    }

    void DialogueEvent()
    {
        switch (_textCount)
        {
            case 9:
                if (_countEvent == 0)
                {
                    StoryPlayerControl.isHappy = true;
                    _countEvent++;
                }
                else
                {
                    StoryNpcAnimator_Momotaro.isSad_Momo = false;
                    StoryNpcAnimator_Momotaro.isHappy_Momo = true;
                    _countEvent = 0;
                }
                break;

            case 24:
            case 33:
            case 42:
                StoryPlayerControl.isSurprised = true;
                break;

            case 26:
                StoryPlayerAnimator_Momotaro.isDonkey = true;
                StoryRiceDumpling_Momotaro.isChangeRoles = true;
                StoryRiceDumpling_Momotaro.isRoleActive = false;
                break;

            case 45:
                if (_countEvent == 0)
                {
                    _countEvent++;
                    StoryNpcAnimator_Momotaro.isStone = true;
                }
                else
                {
                    StoryPlayerControl.isSurprised = true;
                    StoryNpcAnimator_Momotaro.isStone = false;
                    _countEvent = 0;
                }
                break;

            case 49:
                if (_countEvent == 0)
                {
                    _countEvent++;
                    StoryPlayerControl.isHappy = true;
                }
                else
                {
                    //離開山上
                    StoryNpcAnimator_Momotaro.isWalkGold_Monkey = true;
                    _countEvent = 0;
                }
                break;

            case 50:
                if (_countEvent == 0)
                {
                    StoryNpcAnimator_Momotaro.isControlled_Monkey = true;
                    _countEvent++;
                    
                }
                else
                {
                    StoryPlayerControl.isSurprised = true;
                    _countEvent = 0;
                }
                break;

            case 56:
                if (_countEvent == 0)
                {
                    StoryNpcAnimator_Momotaro.isControlled_Monkey = true;
                    _countEvent++;

                }
                else
                {
                    StoryPlayerControl.isSurprised = true;
                    _countEvent = 0;
                }
                break;
        }
    }
    void DialogurEnd()
    {
        switch (_textCount)
        {
            case 9:
                BGM.PlayOneShot(give);
                StoryGameControl_Momotaro.isForestActive = true;
                StoryInteractableControl_Momotaro.isGiveItem = true;
                StoryInteractableControl_Momotaro._whoGive = 1;
                break;

            case 47:
                //移動到山
                StoryNpcAnimator_Momotaro.isWalk_Monkey = true;
                StoryGameControl_Momotaro.isMountainActive = true;
                break;

            case 49:
                //傳送下山
                break;

            case 50:
                //壞結局 回到起點
                StoryNpcAnimator_Momotaro.isControlled_Monkey = false;
                break;

            case 56:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Momotaro.isGiveItem = true;
                StoryInteractableControl_Momotaro._whoGive = 7;
                break;
        }
    }
    void StoryEnd()
    {
        StoryUIControl_Momotaro.isStoryEnding = true;
    }
}
