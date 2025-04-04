using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip normalMusia, spikedMusia;
    public AudioClip give, engrav, honest, dishonest, magic, controlled, bingo, wrong;

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
        if (StoryTeachControl.isTeachActive) return;

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
                    switch (_textCount)
                    {
                        case 20:
                            StoryInteractableControl_Momotaro.isAnswerLie = false;
                            BGM.PlayOneShot(honest);
                            JumpToSection("C");
                            break;

                        case 74:
                            StoryGameControl_Momotaro.isReadly = true;
                            JumpToSection("A");
                            break;

                        default:
                            JumpToSection("A");
                            break;
                    }
                }
                else if (_chooseNum == 2)
                {
                    switch (_textCount)
                    {
                        case 20:
                            StoryInteractableControl_Momotaro.isAnswerLie = true;
                            BGM.PlayOneShot(dishonest);
                            JumpToSection("D");
                            break;

                        case 40:
                            StoryNpcAnimator_Momotaro._dating++;
                            JumpToSection("B");
                            break;

                        case 47:
                        case 74:
                            JumpToSection("A");
                            break;

                        default:
                            JumpToSection("B");
                            break;
                    }
                }
                else if (_chooseNum == 3)
                {
                    switch (_textCount)
                    {
                        case 17:
                        case 18:
                        case 19:
                        case 22:
                            StoryInteractableControl_Momotaro.isAnswerLie = true;
                            BGM.PlayOneShot(dishonest);
                            JumpToSection("D");
                            break;

                        case 20:
                            StoryInteractableControl_Momotaro.isAnswerLie = false;
                            BGM.PlayOneShot(honest);
                            JumpToSection("C");
                            break;

                        case 68:
                        case 71:
                            BGM.PlayOneShot(wrong);
                            JumpToSection("C");
                            break;

                        default:
                            JumpToSection("C");
                            break;
                    }
                }
                else if (_chooseNum == 4)
                {
                    switch (_textCount)
                    {
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 22:
                            StoryInteractableControl_Momotaro.isAnswerLie = true;
                            BGM.PlayOneShot(dishonest);
                            JumpToSection("D");
                            break;

                        case 68:
                        case 71:
                            BGM.PlayOneShot(wrong);
                            JumpToSection("C");
                            break;

                        default:
                            JumpToSection("D");
                            break;

                    }
                }
                else if (_chooseNum == 5)
                {
                    JumpToSection("E");
                    switch (_textCount)
                    {
                        case 17:
                        case 18:
                        case 19:
                        case 22:
                            StoryInteractableControl_Momotaro.isAnswerLie = false;
                            BGM.PlayOneShot(honest);
                            break;

                        case 20:
                            StoryInteractableControl_Momotaro.isAnswerGold = true;
                            break;

                        case 68:
                        case 71:
                            BGM.PlayOneShot(bingo);
                            StoryInteractableControl_Momotaro.isAnswerCorrect = true;
                            break;
                    
                    }
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
            case 2:
                StoryPlayerControl.isSad = true;
                break;

            case 4:
            case 11:
            case 14:
            case 28:
            case 51:
            case 53:
                StoryPlayerControl.isHappy = true;
                break;

            case 9:
                switch (_countEvent)
                {
                    case 0:
                        StoryPlayerControl.isHappy = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryNpcAnimator_Momotaro.isSad_Momo = false;
                        StoryNpcAnimator_Momotaro.isHappy_Momo = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 12:
                switch (_countEvent)
                {
                    case 0:
                        StoryNpcAnimator_Momotaro.isSad_Momo = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryNpcAnimator_Momotaro.isSad_Momo = false;
                        _countEvent++;
                        break;

                    case 2:
                        StoryPlayerControl.isSurprised = true;
                        StoryNpcAnimator_Momotaro.isWalk_Momo = true;
                        StoryNpcAnimator_Momotaro._movePlot = 1;
                        _countEvent = 0;
                        break;
                }
                break;

            case 13:
                switch (_countEvent)
                {
                    case 0:
                        StoryNpcAnimator_Momotaro.isSad_Momo = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryNpcAnimator_Momotaro.isSad_Momo = false;
                        _countEvent = 0;
                        break;
                }
                break;

            case 15:
                switch (_countEvent)
                {
                    case 0:
                        StoryNpcAnimator_Momotaro.isWalk_Momo = false;
                        _countEvent++;
                        break;

                    case 1:
                        StoryPlayerControl.isHappy = true;
                        StoryNpcAnimator_Momotaro.isOutLake = true;
                        _countEvent++;
                        break;

                    case 2:
                        StoryPlayerControl.isSurprised = true;
                        StoryNpcAnimator_Momotaro.isWalk_Momo = true;
                        StoryNpcAnimator_Momotaro._movePlot = 3;
                        _countEvent++;
                        break;

                    case 3:
                        StoryNpcAnimator_Momotaro.isWalk_Momo = false;
                        StoryNpcAnimator_Momotaro.isOutLake_GSMomo = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 16:
                switch (_countEvent)
                {
                    case 0:
                        StoryPlayerControl.isHappy = true;
                        StoryNpcAnimator_Momotaro.isOutLake = true;
                        StoryNpcAnimator_Momotaro.isBackLake_GSMomo = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryPlayerControl.isHappy = true;
                        StoryNpcAnimator_Momotaro.isBackLake = true;
                        StoryNpcAnimator_Momotaro._movePlot = 4;
                        _countEvent = 0;
                        break;
                }
                break;

            case 17:
            case 18:
            case 19:
            case 22:
                if (StoryInteractableControl_Momotaro.isAnswerLie)
                {
                    StoryInteractableControl_Momotaro.isGiveItem = true;
                    StoryInteractableControl_Momotaro._whoGive = 2;
                    StoryNpcAnimator_Momotaro.isAngry = true;
                }
                else
                {
                    BGM.PlayOneShot(give);
                    StoryInteractableControl_Momotaro.isGiveItem = true;
                    StoryInteractableControl_Momotaro._whoGive = 2;
                    StoryPlayerControl.isHappy = true;
                }
                break;

            case 20:
                if (!StoryInteractableControl_Momotaro.isAnswerGold)
                {
                    if (StoryInteractableControl_Momotaro.isAnswerLie)
                    {
                        switch (_countEvent)
                        {
                            case 0:
                                StoryNpcAnimator_Momotaro.isAngry = true;
                                _countEvent++;
                                break;

                            case 1:
                                BGM.PlayOneShot(give);
                                StoryInteractableControl_Momotaro.isGiveItem = true;
                                StoryInteractableControl_Momotaro._whoGive = 2;
                                StoryPlayerControl.isHappy = true;
                                _countEvent = 0;
                                break;
                        }
                    }
                    else
                    {
                        StoryInteractableControl_Momotaro.isGiveItem = true;
                        StoryInteractableControl_Momotaro._whoGive = 2;
                    }
                }
                else 
                {
                    StoryPlayerControl.isSurprised = true;
                    StoryInteractableControl_Momotaro.isAnswerGold = false;
                }
                break;

            case 24:
            case 30:
            case 33:
            case 42:
                StoryPlayerControl.isSurprised = true;
                break;

            case 26:
                BGM.PlayOneShot(magic);
                StoryPlayerAnimator_Momotaro.isSmokeEF = true;
                StoryPlayerAnimator_Momotaro.isDonkey = true;
                StoryRiceDumpling_Momotaro.isChangeRoles = true;
                break;

            case 31:
                switch (_countEvent)
                {
                    case 0:
                        _countEvent++;
                        StoryNpcAnimator_Momotaro.isCloseEyes = false;
                        break;

                    case 1:
                        StoryPlayerControl.isHappy = true;
                        StoryNpcAnimator_Momotaro.isShy = true;
                        _countEvent = 0;
                        break;
                }
                StoryNpcAnimator_Momotaro.isCloseEyes = false;
                break;

            case 44:
                BGM.PlayOneShot(engrav);
                StoryInteractableControl_Momotaro.isFinishWork = true;
                break;

            case 40:
                StoryNpcAnimator_Momotaro.isCloseEyes = true;
                break;

            case 45:
                switch (_countEvent)
                {
                    case 0:
                        _countEvent++;
                        StoryNpcAnimator_Momotaro.isStone = true;
                        break;

                    case 1:
                        StoryPlayerControl.isSurprised = true;
                        StoryNpcAnimator_Momotaro.isStone = false;
                        _countEvent = 0;
                        break;
                }
                break;

            case 49:
            case 52:
                switch (_countEvent)
                {
                    case 0:
                        _countEvent++;
                        StoryNpcAnimator_Momotaro.isGold_Monkey = true;
                        break;

                    case 1:
                        StoryPlayerControl.isHappy = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 50:
            case 58:
                switch (_countEvent)
                {
                    case 0:
                        BGM.PlayOneShot(controlled);
                        StoryNpcAnimator_Momotaro.isControlled_Monkey = true;
                        StoryNpcAnimator_Momotaro.isControlled_Dog = true;
                        StoryNpcAnimator_Momotaro.isControlled_Chicken = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryPlayerControl.isSurprised = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 55:
                StoryNpcAnimator_Momotaro.isSliver_Dog = true;
                break;

            case 56:
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

            case 57:
            case 59:
                switch (_countEvent)
                {
                    case 0:
                        StoryPlayerControl.isHappy = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryNpcAnimator_Momotaro.isFindMomotaro = true;
                        _countEvent = 0;
                        break;
                }
                break;

            case 63:
                StoryPerformancesControl_Momotaro._danceNum = 1;
                break;

            case 67:
                StoryNpcAnimator_Momotaro.isGold_Chicken = true;
                break;

            case 68:
            case 71:
                switch (_countEvent)
                {
                    case 0:
                        StoryNpcAnimator_Momotaro.isPerformance = true;
                        StoryPlayerControl.isSurprised = true;
                        _countEvent++;
                        break;

                    case 1:
                        if (StoryInteractableControl_Momotaro.isAnswerCorrect)
                        {
                            BGM.PlayOneShot(give);
                            StoryInteractableControl_Momotaro.isGiveItem = true;
                            StoryInteractableControl_Momotaro._whoGive = 9;
                        }
                        _countEvent = 0;
                        break;
                }
                break;

            case 70:
                switch (_countEvent)
                {
                    case 0:
                        StoryNpcAnimator_Momotaro.isExcited = true;
                        _countEvent++;
                        break;

                    case 1:
                        StoryNpcAnimator_Momotaro.isLeave_Parrot = true;
                        StoryNpcAnimator_Momotaro.isWalk_Parrot = true;
                        StoryNpcAnimator_Momotaro.isExcited = false;
                        _countEvent = 0;
                        break;
                }
                break;
        }
    }
    void DialogurEnd()
    {
        switch (_textCount)
        {
            case 2:
                StoryLoadingScene_Momotaro.isPlotAnimator = false;
                break;

            case 9:
                BGM.PlayOneShot(give);
                StoryGameControl_Momotaro.isForestActive = true;
                StoryInteractableControl_Momotaro.isGiveItem = true;
                StoryInteractableControl_Momotaro._whoGive = 1;
                break;

            case 11:
            case 14:
                StoryUIControl_Momotaro.isStoryEnding = true;
                if (StoryInteractableControl_Momotaro.isSpecialEnding)
                {
                    StoryNpcAnimator_Momotaro.isWalk_Momo = true;
                    StoryNpcAnimator_Momotaro._movePlot = 6;
                }
                break;

            case 13:
                StoryInteractableControl_Momotaro.isMomoFindGoddess = true;
                break;

            case 15:
                StoryNpcAnimator_Momotaro.isGoTarget_GSMomo = true;
                StoryNpcAnimator_Momotaro.isBackLake = true;
                break;

            case 16:
                StoryNpcAnimator_Momotaro.isWalk_Momo = true;
                StoryNpcAnimator_Momotaro._movePlot = 5;
                StoryInteractableControl_Momotaro.isMeetPartner = true;
                break;

            case 17:
            case 18:
                StoryNpcAnimator_Momotaro.isBackLake = true;
                if (StoryNpcAnimator_Momotaro.isAngry)
                {
                    Invoke("FalseGoddessAngry", 2f);
                }
                else
                {
                    StoryTeachControl.isTeachActive = true;
                }
                break;

            case 19:
            case 20:
            case 22:
                StoryNpcAnimator_Momotaro.isBackLake = true;
                if (StoryNpcAnimator_Momotaro.isAngry)
                {
                    Invoke("FalseGoddessAngry", 2f);
                }
                break;

            case 26:
                StoryTeachControl.isTeachActive = true;
                StoryGameControl_Momotaro.isMeetDonkey = true;
                break;

            case 28:
                StoryNpcAnimator_Momotaro.isGift = true;      
                break;

            case 30:
            case 31:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Momotaro.isGiveItem = true;
                StoryInteractableControl_Momotaro._whoGive = 3;
                break;

            case 36:
            case 38:
            case 39:
                StoryNpcAnimator_Momotaro._dating++;
                break;

            case 44:
                StoryNpcAnimator_Momotaro.isMeet = true;
                break;

            case 45:
                StoryTeachControl.isTeachActive = true;
                break;

            case 47:
                StoryNpcAnimator_Momotaro.isGoMountain = true;
                StoryNpcAnimator_Momotaro.isWalk_Monkey = true;
                StoryGameControl_Momotaro.isMountainActive = true;
                break;

            case 49:
            case 52:
                StoryNpcAnimator_Momotaro.isLeave_Monkey = true;
                StoryNpcAnimator_Momotaro.isWalkGold_Monkey = true;
                StoryNpcAnimator_Momotaro.isWalk_GoldMomo = true;
                StoryStrongWind_Momotaro._respawnNum = 1;
                StoryStrongWind_Momotaro.isBlownAway = true;
                BlackScreenControl.isOpenBlackScreen = true;
                StoryLoadingScene_Momotaro.isHintGoPlaza = true;
                break;

            case 50:
            case 58:
                BlackScreenControl.isOpenBlackScreen = true;
                Invoke("FalseControlled", 1f);
                break;

            case 51:
            case 53:
                StoryInteractableControl_Momotaro.isBackLake = true;
                StoryNpcAnimator_Momotaro.isFindMomotaro_Monkey = true;
                break;

            case 56:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Momotaro.isGiveItem = true;
                StoryInteractableControl_Momotaro._whoGive = 7;
                break;

            case 63:
                StoryTeachControl.isTeachActive = true;
                StoryPerformancesControl_Momotaro._danceNum = 0;
                StoryNpcAnimator_Momotaro._performancesNum = 1;
                StoryGameControl_Momotaro.isPerformancesPointActive = true;
                break;

            case 74:
                if (StoryGameControl_Momotaro.isReadly)
                {
                    StoryUIControl_Momotaro.isPerformances = true;
                }
                break;
        }
    }

    void FalseGoddessAngry()
    {
        StoryNpcAnimator_Momotaro.isAngry = false;
    }
    void FalseControlled()
    {
        StoryNpcAnimator_Momotaro.isControlled_Monkey = false;
        StoryNpcAnimator_Momotaro.isControlled_Dog = false;
        StoryNpcAnimator_Momotaro.isControlled_Chicken = false;
    }
}
