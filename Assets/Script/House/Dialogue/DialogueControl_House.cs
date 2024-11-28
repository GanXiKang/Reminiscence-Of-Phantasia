using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip bell;

    [Header("UI")]
    public GameObject[] whoDialogue;
    public Text[] whoContent;
    public static int _whoDia;
    public static bool isBirdTalk = false;
    public static bool isCatTalk = false;

    [Header("Position")]
    public Transform[] dialoguePos;
    float _moveSpeed = 300f;
    bool isMove;
    bool isPosA;
    int PosANum;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int _index;
    public float _textSpend;
    public static int _textCount = 1;
    bool isTextFinish;
    int _countEvent = 0;

    List<string> textList = new List<string>();

    //Entrust and Store
    public static int _paragraph = 0;
    public static bool isAutoNext = false;
    //Plot
    public static bool isAutoPlot = false;

    void OnEnable()
    {
        GetTextFormFile(textFile[_textCount]);
        StartCoroutine(SetTextLabelIndexUI());
    }

    void Update()
    {
        TextController();
        DialogueMove();
        AutoNext();
        AutoPlot();
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
        if (isAutoPlot) return;
        if (SettingControl.isSettingActive) return;
        if (DoorControl_House.isEntrust || DoorControl_House.isStore) return;
        if (BlackScreenControl.isOpenBlackScreen || BlackScreenControl.isCloseBlackScreen) return;

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
                if (!isMove)
                {
                    _textSpend = 0f;
                }
            }
        }
        AvatarControl_House.isTalk = !isTextFinish;
    }
    void DialogueMove()
    {
        if (!isMove) return;

        if (isPosA)
        {
            whoDialogue[PosANum].transform.position = Vector3.MoveTowards(whoDialogue[PosANum].transform.position, dialoguePos[2].position, _moveSpeed * Time.deltaTime);
        }

        if (!DoorControl_House.isEntrust && !DoorControl_House.isStore)
        {
            if (whoDialogue[_whoDia].transform.position != dialoguePos[1].position)
            {
                whoDialogue[_whoDia].transform.position = Vector3.MoveTowards(whoDialogue[_whoDia].transform.position, dialoguePos[1].position, _moveSpeed * Time.deltaTime);
            }
            else
            {
                isMove = false;
                isPosA = true;
                PosANum = _whoDia;
            }
        }
        else
        {
            if (whoDialogue[_whoDia].transform.position != dialoguePos[4].position)
            {
                whoDialogue[_whoDia].transform.position = Vector3.MoveTowards(whoDialogue[_whoDia].transform.position, dialoguePos[4].position, _moveSpeed * Time.deltaTime);
            }
            else
            {
                isMove = false;
            }
        }
    }
    void AutoNext()
    {
        if (isAutoNext)
        {
            isAutoNext = false;
            _index = _paragraph;
            StartCoroutine(SetTextLabelIndexUI());
            AvatarControl_House.isTalk = !isTextFinish;
        }
    }
    void AutoPlot()
    {
        if (!isAutoPlot) return;

        if (isTextFinish)
        {
            StartCoroutine(AutoPlotRound());
        }
        AvatarControl_House.isTalk = !isTextFinish;
    }

    IEnumerator AutoPlotRound()
    {
        isTextFinish = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SetTextLabelIndexUI());
    }
    IEnumerator SetTextLabelIndexUI()
    {
        isTextFinish = false;
        switch (textList[_index].Trim())
        {
            case "Event":
                DialogueEvent();
                _index++;
                break;
        }
        switch (textList[_index].Trim())
        {
            case "Player":
                _whoDia = 0;
                isMove = true;
                DialoguePoint();
                _index++;
                break;

            case "Target":
                if (isBirdTalk)
                {
                    _whoDia = 1;
                    isMove = true;
                    DialoguePoint();
                }
               else if(isCatTalk)
               {
                    _whoDia = 2;
                    isMove = true;
                    DialoguePoint();
                }
                _index++;
                break;

            case "End":
                DialogueEnd();
                UIControl_House.isDialogue = false;
                isPosA = false;
                isBirdTalk = false;
                isCatTalk = false;
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

    void DialoguePoint()
    {
        if (!DoorControl_House.isEntrust && !DoorControl_House.isStore)
        {
            whoDialogue[_whoDia].transform.position = dialoguePos[0].position;
        }
        else
        {
            whoDialogue[_whoDia].transform.position = dialoguePos[3].position;
        }
    }
    void DialogueEvent()
    {
        switch (_textCount)
        {
            case 1:
            case 9:
            case 19:
                BGM.PlayOneShot(bell);
                break;

            case 20:
                switch (_countEvent)
                {
                    case 0:
                        _countEvent++;
                        CatControl_House.isWave = true;
                        PlayerControl_House.isWave = true;
                        break;

                    case 1:
                        _countEvent = 0;
                        CatControl_House.isWave = false;
                        PlayerControl_House.isWave = false;
                        break;
                }
                break;

            case 25:
                CatControl_House.isBye = true;
                break;

            case 27:
                switch (_countEvent)
                {
                    case 0:
                        _countEvent++;
                        PlayerControl_House.isWave = true;
                        break;

                    case 1:
                        _countEvent = 0;
                        PlayerControl_House.isWave = false;
                        break;
                }
                break;
        }
    }
    void DialogueEnd()
    {
        switch (_textCount)
        {
            case 3:
            case 4:
            case 5:
            case 6:
                isAutoPlot = false;
                break;

            case 9:
            case 19:
                InteractableControl_House.isColliderActive[2] = true;
                break;

            case 10:
                UIAboveObject_House.isAboveBed = true;
                InteractableControl_House.isColliderActive[3] = true;
                InteractableControl_House.isColliderActive[5] = true;
                break;

            case 20:
            case 22:
            case 25:
            case 28:
            case 31:
                BlackScreenControl.isOpenBlackScreen = true;
                Invoke("WaitBlackScreenEvent", 1f);
                break;

            case 23:
                InteractableControl_House.isColliderActive[2] = true;
                InteractableControl_House.isColliderActive[4] = false;
                UIAboveObject_House.isAboveDoor = true;
                UIAboveObject_House.isAboveBookcase = false;
                CatControl_House._goPointNum = 0;
                DoorControl_House.isStore = true;
                break;

            case 27:
            case 34:
                InteractableControl_House.isColliderActive[2] = true;
                DoorControl_House.isEntrust = true;
                break;

            case 33:
                InteractableControl_House.isBirdLeave = true;
                InteractableControl_House.isColliderActive[2] = true;
                UIAboveObject_House.isAboveDoor = true;
                UIAboveObject_House.isAboveBookcase = false;
                BirdControl_House._goPointNum = 0;
                DoorControl_House.isEntrust = true;
                break;
        }
    }
    void WaitBlackScreenEvent()
    {
        switch (_textCount)
        {
            case 20:
                InteractableControl_House.isCatSeeWorkbench = true;
                InteractableControl_House.isColliderActive[1] = true;
                UIAboveObject_House.isAboveWorkbench = true;
                UIAboveObject_House.isAboveDoor = false;
                CatControl_House._goPointNum = 1;
                break;

            case 22:
                InteractableControl_House.isColliderActive[4] = true;
                UIAboveObject_House.isAboveBookcase = true;
                CatControl_House._goPointNum = 2;
                break;

            case 25:
                DoorControl_House.isCat = false;
                InteractableControl_House.isBirdDoorBell = true;
                break;

            case 28:
                InteractableControl_House.isColliderActive[3] = true;
                UIAboveObject_House.isAboveDoor = false;
                UIAboveObject_House.isAboveBed = true;
                BirdControl_House._goPointNum = 1;
                break;

            case 31:
                InteractableControl_House.isColliderActive[4] = true;
                UIAboveObject_House.isAboveBookcase = true;
                BirdControl_House._goPointNum = 2;
                break;
        }
    }
}
