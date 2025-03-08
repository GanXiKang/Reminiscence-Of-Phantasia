using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Prince : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip normalMusia, spikedMusia;
    public AudioClip give;

    [Header("UITransform")]
    public Transform[] target;
    public static int _isAboveWho1;
    public static int _isAboveWho2;
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
    bool isCanKeyCode = false;
    int _buttonNum;
    int _chooseNum;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int _index;
    public float _textSpend;
    public static int _textCount;
    bool isTextFinish;

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
        if (SettingControl.isSettingActive) return;
        if (StoryLoadingScene_Prince.isLoading) return;

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
        isCanKeyCode = false;
        if (_chooseButton <= 2)
            button2UI.SetActive(false);
        else
            button3UI.SetActive(false);
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
                isCanKeyCode = true;
                if (_buttonNum == 2)
                    button2UI.SetActive(true);
                else
                    button3UI.SetActive(true);
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
                    switch (_textCount)
                    {
                        case 10:
                        case 11:
                            StoryInteractableControl_Prince.isCanHelpPrince = true;
                            break;

                        case 15:
                            StoryInteractableControl_Prince.isTakeGem = true;
                            break;
                    }
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
        if (isCanKeyCode)
        {
            if (_buttonNum == 2)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    ChooseButton(1);
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    ChooseButton(2);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    ChooseButton(3);
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    ChooseButton(4);
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    ChooseButton(5);
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
                BGM.PlayOneShot(normalMusia);
                _index++;
                break;

            case "Spiked":
                dialogueBG.sprite = spiked;
                BGM.PlayOneShot(spikedMusia);
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
                StoryUIControl_Prince.isDialogue = false;
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
            case 1:
            case 6:
            case 19:
                StoryPlayerControl.isSad = true;
                break;

            case 3:
            case 4:
            case 5:
                StoryPlayerControl.isSurprised = true;
                break;

            case 7:
                switch (_countEvent)
                {
                    case 0:
                        //獲得善能
                        _countEvent++;
                        break;

                    case 1:
                        //Swallow Sad
                        _countEvent = 0;
                        break;
                }
                break;

            case 15:
                if (StoryInteractableControl_Prince.isTakeGem)
                {
                    //雕像破敗 
                    StoryPlayerControl.isSurprised = true;
                }
                else
                {
                    BGM.PlayOneShot(give);
                    StoryInteractableControl_Momotaro.isGiveItem = true;
                    StoryInteractableControl_Momotaro._whoGive = 1;
                }
                break;
        }
    }
    void DialogurEnd()
    {
        switch (_textCount)
        {
            case 3:
                //幽靈離開
                break;

            case 4:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Prince.isGiveItem = true;
                StoryInteractableControl_Prince._whoGive = 1;
                StoryTeachControl.isTeachActive = true;
                break;

            case 6:
                StoryInteractableControl_Prince.isSwallowFindPrince = true;
                StorySkillControl_Prince.isFirstBackNow = true;
                StorySkillControl_Prince.isDisabledClock = false;
                break;

            case 15:
                if (StoryInteractableControl_Prince.isTakeGem)
                {
                    //壞結束
                    StoryInteractableControl_Prince.isTakeGem = false;
                    BlackScreenControl.isOpenBlackScreen = true;
                    StoryPlayerControl.isSad = true;
                }
                break;
        }
    }
    void StoryEnd()
    {
        StoryUIControl_Prince.isStoryEnding = true;
    }
}
