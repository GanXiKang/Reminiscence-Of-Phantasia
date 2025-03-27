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
    public AudioClip give, gainEnergy, shock, drowning, fail, success;

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

                        case 89:
                            StoryUIControl_Prince.isSuppliesActive = true;
                            break;
                    }
                }
                else if (_chooseNum == 2)
                {
                    switch (_textCount)
                    {
                        case 89:
                            JumpToSection("A");
                            break;

                        default:
                            JumpToSection("B");
                            break;
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
            case 42:
                StoryPlayerControl.isSad = true;
                break;

            case 4:
            case 5:
            case 17:
            case 23:
            case 36:
            case 82:
                StoryPlayerControl.isSurprised = true;
                break;

            case 10:
            case 11:
            case 21:
            case 35:
            case 37:
            case 90:
                StoryPlayerControl.isHappy = true;
                break;

            case 2:
                switch (_countEvent)
                {
                    case 0:
                        StoryPlayerControl.isSurprised = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryPlayerControl.isHappy = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 3:
                StoryGhostControl_Prince.isWarp = true;
                StoryPlayerControl.isSurprised = true;
                break;

            case 7:
                switch (_countEvent)
                {
                    case 0:
                        BGM.PlayOneShot(gainEnergy);
                        StoryPlayerControl.isHappy = true;
                        StorySkillControl_Prince.isGainEnegry = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryNpcAnimator_Prince.isSurprise_Swallow = true;
                        _countEvent++;
                        break;

                    case 2:
                        StoryNpcAnimator_Prince.isSurprise_Swallow = false;
                        StoryNpcAnimator_Prince.isSad_Swallow = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 15:
                if (StoryInteractableControl_Prince.isTakeGem)
                {
                    switch (_countEvent)
                    {
                        case 0:
                            StoryGameControl_Prince.isBroken = true;
                            StoryPlayerControl.isSurprised = true;
                            _countEvent++;
                            break;

                        case 1:
                            StoryGhostControl_Prince.isWarp = true;
                            _countEvent = 0;
                            break;
                    }
                }
                else
                {
                    switch (_countEvent)
                    {
                        case 0:
                            StoryPlayerControl.isSad = true;
                            StoryGhostControl_Prince.isWarp = true;
                            _countEvent++;
                            break;

                        case 1:
                            BGM.PlayOneShot(give);
                            StoryInteractableControl_Prince.isGiveItem = true;
                            StoryInteractableControl_Prince._whoGive = 1;
                            StoryInteractableControl_Prince._whoGiveNumber = 0;
                            _countEvent = 0;
                            break;
                    }
                }
                break;

            case 22:
                switch (_countEvent)
                {
                    case 0:
                        StoryPlayerControl.isSurprised = true;
                        _countEvent++;
                        break;

                    case 1:
                        BGM.PlayOneShot(give);
                        StoryInteractableControl_Prince.isGiveItem = true;
                        StoryInteractableControl_Prince._whoGive = 7;
                        StoryInteractableControl_Prince._whoGiveNumber = 1;
                        _countEvent = 0;
                        break;
                }
                break;

            case 25:
                BGM.PlayOneShot(drowning);
                StoryPlayerControl.isSurprised = true;
                StoryNpcAnimator_Prince.isWet = true;
                StoryInteractableControl_Prince._helpChildQian = 4;
                break;

            case 27:
                switch (_countEvent)
                {
                    case 0:
                        BGM.PlayOneShot(shock);
                        StoryNpcAnimator_Prince.isShock_Qian = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryInteractableControl_Prince.isGiveItem = true;
                        StoryInteractableControl_Prince._whoGive = 6;
                        StoryInteractableControl_Prince._whoGiveNumber = 1;
                        _countEvent = 0;
                        break;
                }
                break;

            case 28:
                BGM.PlayOneShot(fail);
                StoryPlayerControl.isSurprised = true;
                StoryNpcAnimator_Prince.isDrown = true;
                break;

            case 29:
                BGM.PlayOneShot(success);
                StoryInteractableControl_Prince._helpChildQian = 5;
                break;

            case 30:
                switch (_countEvent)
                {
                    case 0:
                        StoryInteractableControl_Prince._helpChildQian = 6;
                        _countEvent++;
                        break;

                    case 1:
                        StoryInteractableControl_Prince._helpChildQian = 7;
                        _countEvent = 0;
                        break;
                }
                break;

            case 31:
                switch (_countEvent)
                {
                    case 0:
                        BGM.PlayOneShot(give);
                        StoryInteractableControl_Prince.isGiveItem = true;
                        StoryInteractableControl_Prince._whoGive = 2;
                        StoryInteractableControl_Prince._whoGiveNumber = 0;
                        StoryPlayerControl.isHappy = true;
                        _countEvent++;
                        break;

                    case 1:
                        BGM.PlayOneShot(gainEnergy);
                        StorySkillControl_Prince.isGainEnegry = true;
                        StorySkillControl_Prince._gainEnegryValue = 0.3f;
                        StoryInteractableControl_Prince._helpChildQian = 8;
                        _countEvent = 0;
                        break;
                }
                break;

            case 32:
                switch (_countEvent)
                {
                    case 0:
                        StoryPlayerControl.isHappy = true;
                        StoryInteractableControl_Prince._swallowHunger = 1;
                        _countEvent++;
                        break;

                    case 1:
                        StoryNpcAnimator_Prince.isWalk_Swallow = false;
                        StoryNpcAnimator_Prince.isHungry = true;
                        StoryNpcAnimator_Prince.isComeBack = true;
                        StoryInteractableControl_Prince._swallowHunger = 2;
                        _countEvent++;
                        break;

                    case 2:
                        StoryPlayerControl.isSurprised = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 38:
                StoryPlayerControl.isHappy = true;
                StoryNpcAnimator_Prince.isHungry = false;
                break;

            case 41:
                switch (_countEvent)
                {
                    case 0:
                        BGM.PlayOneShot(give);
                        StoryInteractableControl_Prince.isGiveItem = true;
                        StoryInteractableControl_Prince._whoGive = 2;
                        StoryInteractableControl_Prince._whoGiveNumber = 1;
                        _countEvent++;
                        break;

                    case 1:
                        StoryPlayerControl.isHappy = true;
                        StoryInteractableControl_Prince._swallowHunger = 4;
                        _countEvent = 0;
                        break;
                }
                break;

            case 46:
            case 49:
                BGM.PlayOneShot(give);
                StoryPlayerControl.isHappy = true;
                StoryInteractableControl_Prince.isGiveItem = true;
                if (_textCount == 49)
                    StoryInteractableControl_Prince._whoGive = 9;
                else
                    StoryInteractableControl_Prince._whoGive = 11;
                break;

            case 56:
                StoryNpcAnimator_Prince.isLeave_Bei = true;
                break;
        }
    }
    void DialogurEnd()
    {
        switch (_textCount)
        {
            case 72:
            case 75:
            case 77:
                StoryPlayerControl.isSurprised = true;
                break;

            case 14:
            case 54:
            case 73:
            case 76:
                StoryPlayerControl.isSad = true;
                break;

            case 29:
            case 62:
            case 63:
            case 74:
            case 78:
            case 79:
            case 80:
                StoryPlayerControl.isHappy = true;
                break;

            case 4:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Prince.isGiveItem = true;
                StoryInteractableControl_Prince._whoGive = 1;
                StoryInteractableControl_Prince._whoGiveNumber = 2;
                StoryInteractableControl_Prince.isPlotBanMove = true;
                StoryTeachControl.isTeachActive = true;
                break;

            case 5:
                StoryInteractableControl_Prince.isPlotBanMove = false;
                break;

            case 6:
                StoryGhostControl_Prince.isDisappear = true;
                StoryInteractableControl_Prince.isSwallowFindPrince = true;
                StorySkillControl_Prince.isFirstBackNow = true;
                StorySkillControl_Prince.isDisabledClock = false;
                break;

            case 7:
                StoryNpcAnimator_Prince.isSad_Swallow = false;
                StoryNpcAnimator_Prince.isWalk_Swallow = true;
                StoryNpcAnimator_Prince.isFindFood = true;
                break;

            case 10:
            case 11:
                if (StoryInteractableControl_Prince.isCanHelpPrince)
                {
                    StoryTeachControl.isTeachActive = true;
                    StoryGameControl_Prince.isSuppliesGameEasy = true;
                    StoryNpcAnimator_Prince.isLeaveHelp = true;
                }
                break;

            case 15:
                if (StoryInteractableControl_Prince.isTakeGem)
                {
                    StoryInteractableControl_Prince.isTakeGem = false;
                    StoryGhostControl_Prince.isDisappear = true;
                    BlackScreenControl.isOpenBlackScreen = true;
                    StoryPlayerControl.isSad = true;
                    Invoke("FalseBrokenPrinceStatue", 1f);
                }
                else
                {
                    StoryGhostControl_Prince.isNoGem = true;
                    StoryGhostControl_Prince.isDisappear = true;
                    StoryInteractableControl_Prince.isKangNeedGem = false;
                }
                break;

            case 16:
                StorySkillControl_Prince.isClockActice = true;
                break;

            case 17:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Prince.isGiveItem = true;
                StoryInteractableControl_Prince._whoGive = 7;
                StoryInteractableControl_Prince._whoGiveNumber = 0;
                break;

            case 21:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Prince.isGiveItem = true;
                StoryInteractableControl_Prince._whoGive = 6;
                StoryInteractableControl_Prince._whoGiveNumber = 0;
                break;

            case 22:
                StoryInteractableControl_Prince._helpChildQian = 1;
                StoryNpcAnimator_Prince.isLeave_Kang = true;
                break;

            case 23:
                StoryInteractableControl_Prince._helpChildQian = 2;
                break;

            case 24:
                StoryInteractableControl_Prince._helpChildQian = 3;
                break;

            case 27:
                StoryNpcAnimator_Prince.isShock_Qian = false;
                break;

            case 28:
                BlackScreenControl.isOpenBlackScreen = true;
                StoryPlayerControl.isSad = true;
                Invoke("FalsePrinceDrown", 1f);
                break;

            case 30:
                StoryNpcAnimator_Prince.isLeave_Qian = true;
                break;

            case 31:
                StoryInteractableControl_Prince.isPrinceNoDie = true;
                break;

            case 32:
                StoryInteractableControl_Prince.isSwallowHunger = true;
                break;

            case 38:
                StoryInteractableControl_Prince.isGiveSuppliesBox = true;
                break;

            case 40:
                StoryPlayerControl.isHappy = true;
                StoryInteractableControl_Prince._swallowHunger = 3;
                StoryGameControl_Prince.isSuppliesGameHard = true;
                break;

            case 41:
                StoryInteractableControl_Prince.isNeedSauce = true;
                break;

            case 81:
            case 82:
            case 83:
            case 84:
            case 85:
            case 86:
                BGM.PlayOneShot(gainEnergy);
                StoryPlayerControl.isHappy = true;
                StorySkillControl_Prince.isGainEnegry = true;
                StorySkillControl_Prince._gainEnegryValue = 0.05f;
                break;
        }
    }
    void StoryEnd()
    {
        StoryUIControl_Prince.isStoryEnding = true;
    }

    void FalseBrokenPrinceStatue()
    {
        StoryGameControl_Prince.isBroken = false;
    }
    void FalsePrinceDrown()
    {
        StoryNpcAnimator_Prince.isDrown = false;
    }
}
