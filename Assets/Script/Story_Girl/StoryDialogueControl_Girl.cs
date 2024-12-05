using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip normalMusia, spikedMusia;
    public AudioClip gun, bingo, wrong, give;

    [Header("UITransform")]
    public Transform[] target;
    public static int _isAboveWho1 = 0;
    public static int _isAboveWho2 = 0;
    bool isPlayerTalk;
    int _targetNum = 0;

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
                        case 40:
                        case 41:
                            StoryInteractableControl_Girl.isAgreeFind = true;
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
                    switch (_textCount)
                    {
                        case 16:
                            BGM.PlayOneShot(wrong);
                            StoryPlayerControl.isSad = true;
                            break;
                    }
                }
                else if (_chooseNum == 4)
                {
                    JumpToSection("D");
                    switch (_textCount)
                    {
                        case 16:
                            BGM.PlayOneShot(bingo);
                            StoryPlayerControl.isHappy = true;
                            break;
                    }
                }
                else if (_chooseNum == 5)
                {
                    JumpToSection("E");
                    switch (_textCount)
                    {
                        case 16:
                            BGM.PlayOneShot(wrong);
                            StoryPlayerControl.isSad = true;
                            break;
                    }
                }
            }
        }
        if (isCanKeyCode)
        {
            print("yes");
            if (_buttonNum == 2)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChooseButton(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChooseButton(2);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChooseButton(3);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChooseButton(4);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    ChooseButton(5);
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
            case 14:
            case 44:
                StoryPlayerControl.isSurprised = true;
                break;

            case 19:
                isDialogueRotation = true;
                break;

            case 25:
                StoryPlayerControl.isHappy = true;
                isDialogueRotation = true;
                break;

            case 22:
            case 43:
                StoryPlayerControl.isHappy = true;
                isDialogueEvent = true;
                isDialogueRotation = true;
                break;

            case 30:
                StoryPlayerControl.isSurprised = true;
                break;

            case 39:
                isDialogueEvent = true;
                StoryPlayerControl.isHappy = true;
                StoryPileWood_Girl.isFireActice = true;
                break;

            case 45:
                StoryPlayerControl.isHappy = true;
                isDialogueRotation = true;
                break;

            case 47:
                isDialogueRotation = true;
                break;

            case 49:
                StoryNpcAnimator_Girl.isSurprise = true;
                break;

            case 50:
                BGM.PlayOneShot(gun);
                StoryPlayerControl.isSurprised = true;
                StoryNpcAnimator_Girl.isSurprise = true;
                StoryNpcAnimator_Girl.isScared = true;
                StoryNpcAnimator_Girl.isShotRunAway = true;
                break;
        }
    }
    void DialogurEnd()
    {
        switch (_textCount)
        {
            case 2:
                BGM.PlayOneShot(give);
                StoryGameControl_Girl.isWallActive = false;
                StoryInteractableControl_Girl.isGiveItem = true;
                StoryInteractableControl_Girl._whoGive = 1;
                break;

            case 7:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Girl.isGiveItem = true;
                StoryInteractableControl_Girl._whoGive = 1;
                break;

            case 10:
                BlackScreenControl.isOpenBlackScreen = true;
                StoryGameControl_Girl.isResurrection = true;
                break;

            case 11:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Girl.isGiveItem = true;
                StoryInteractableControl_Girl._whoGive = 2;
                break;

            case 13:
                BGM.PlayOneShot(give);
                StoryGameControl_Girl.isTrashcanLidActice = false;
                StoryInteractableControl_Girl.isGiveItem = true;
                StoryInteractableControl_Girl._whoGive = 2;
                break;

            case 16:
                if (_chooseNum == 4)
                {
                    StoryInteractableControl_Girl.isGiveItem = true;
                    StoryInteractableControl_Girl._whoGive = 3;
                }
                break;

            case 19:
                StoryBagControl.isItemNumber[4] = true;
                StoryBagControl._howManyGrids++;
                StoryNpcAnimator_Girl.isLeave = true;
                StoryPlayerControl.isSurprised = true;
                break;

            case 25:
                StoryNpcAnimator_Girl.isLeaveStreet = true;
                break;

            case 26:
                BGM.PlayOneShot(give);
                StoryInteractableControl_Girl.isFinallyMatch = true;
                StoryInteractableControl_Girl.isGiveItem = true;
                StoryInteractableControl_Girl._whoGive = 1;
                break;

            case 30:
                StoryNpcAnimator_Girl.isHide = true;
                break;

            case 45:
                StoryNpcAnimator_Girl._direction = 1;
                StoryNpcAnimator_Girl.isSurprise = false;
                StoryNpcAnimator_Girl.isMoveSeeWolf = true;
                break;

            case 46:
            case 51:
                StoryNpcAnimator_Girl._direction = 0;
                StoryNpcAnimator_Girl.isFind = true;
                Invoke("StoryEnd", 1f);
                break;

            case 47:
                StoryNpcAnimator_Girl.isAttractWolf = true;
                break;

            case 50:
                StoryNpcAnimator_Girl.isFinishLeave = true;
                break;
        }
    }
    void StoryEnd()
    {
        StoryUIControl_Girl.isStoryEnding = true;
    }
}
