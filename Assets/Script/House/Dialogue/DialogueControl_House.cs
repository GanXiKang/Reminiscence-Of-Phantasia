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
    int PosBNum;

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
            if(InteractableControl_House.isEnding)
                whoDialogue[PosBNum].transform.position = Vector3.MoveTowards(whoDialogue[PosBNum].transform.position, dialoguePos[5].position, _moveSpeed * Time.deltaTime);
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
                if (InteractableControl_House.isEnding)
                    PosBNum = PosANum;
                PosANum = _whoDia;
            }
        }
        else
        {
            if (whoDialogue[_whoDia].transform.position != dialoguePos[4].position)
                whoDialogue[_whoDia].transform.position = Vector3.MoveTowards(whoDialogue[_whoDia].transform.position, dialoguePos[4].position, _moveSpeed * Time.deltaTime);
            else
                isMove = false;
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
            StartCoroutine(AutoPlotRound());

        AvatarControl_House.isTalk = !isTextFinish;
    }

    IEnumerator AutoPlotRound()
    {
        isTextFinish = false;
        yield return new WaitForSeconds(0.8f);
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
                else if (isCatTalk)
                {
                    _whoDia = 2;
                    isMove = true;
                    DialoguePoint();
                }
                _index++;
                break;

            case "Target2":
                _whoDia = 2;
                isMove = true;
                DialoguePoint();
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
            whoDialogue[_whoDia].transform.position = dialoguePos[0].position;
        else
            whoDialogue[_whoDia].transform.position = dialoguePos[3].position;
    }
    void DialogueEvent()
    {
        switch (_textCount)
        {
            case 1:
            case 9:
            case 19:
            case 48:
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

            case 33:
                UIControl_House.isCoinAppear = true;
                CoinUIControl_House._coinTarget = 1500;
                break;
        }
    }
    void DialogueEnd()
    {
        switch (_textCount)
        {
            case 1:
                CameraStartMove_House.isStartMoveCamera = true;
                CameraControl_House.isFreeLook = true;
                break;

            case 17:
            case 30:
            case 46:
                CameraControl_House.isFreeLook = true;
                break;

            case 3:
            case 4:
            case 5:
            case 6:
                isAutoPlot = false;
                break;

            case 9:
            case 19:
                UIAboveObject_House.isAboveDoor = true;
                InteractableControl_House.isColliderActive[2] = true;
                break;

            case 10:
                UIAboveObject_House.isAboveBed = true;
                InteractableControl_House.isColliderActive[3] = true;
                break;

            case 18:
            case 20:
            case 21:
            case 22:
            case 25:
            case 28:
            case 31:
            case 35:
            case 47:
                BlackScreenControl.isOpenBlackScreen = true;
                Invoke("WaitBlackScreenEvent", 1f);
                break;

            case 23:
                InteractableControl_House.isColliderActive[2] = true;
                UIAboveObject_House.isAboveBookcase = false;
                UIAboveObject_House.isStoreHintActive = true;
                CatControl_House._goPointNum = 0;
                break;

            case 27:
            case 34:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookDoorPlot = false;
                InteractableControl_House.isColliderActive[2] = true;
                DoorControl_House.isEntrust = true;
                break;

            case 33:
                InteractableControl_House.isBirdLeave = true;
                InteractableControl_House.isColliderActive[2] = true;
                InteractableControl_House.isColliderActive[5] = true;
                UIAboveObject_House.isAboveDoor = true;
                UIAboveObject_House.isAboveBookcase = false;
                BirdControl_House._goPointNum = 0;
                DoorControl_House.isEntrust = true;
                break;

            case 48:
                InteractableControl_House.isEnding = true;
                InteractableControl_House.isColliderActive[2] = true;
                UIAboveObject_House.isAboveDoor = true;
                UIAboveObject_House.isAboveBookcase = false;
                break;
        }
    }
    void WaitBlackScreenEvent()
    {
        switch (_textCount)
        {
            case 18:
            case 47:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookWorkPlot = false;
                UIAboveObject_House.isAboveBookcase = true;
                break;

            case 20:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookDoorPlot = false;
                InteractableControl_House.isCatSeeWorkbench = true;
                InteractableControl_House.isColliderActive[1] = true;
                UIAboveObject_House.isAboveWorkbench = true;
                UIAboveObject_House.isAboveDoor = false;
                CatControl_House._goPointNum = 1;
                break;

            case 21:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookWorkPlot = false;
                break;

            case 22:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookWorkPlot = false;
                InteractableControl_House.isColliderActive[4] = true;
                UIAboveObject_House.isAboveBookcase = true;
                CatControl_House._goPointNum = 2;
                break;

            case 25:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookDoorPlot = false;
                DoorControl_House.isCat = false;
                InteractableControl_House.isBirdDoorBell = true;
                break;

            case 28:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookDoorPlot = false;
                InteractableControl_House.isColliderActive[3] = true;
                UIAboveObject_House.isAboveDoor = false;
                UIAboveObject_House.isAboveBed = true;
                BirdControl_House._goPointNum = 1;
                break;

            case 31:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookWorkPlot = false;
                InteractableControl_House.isColliderActive[4] = true;
                UIAboveObject_House.isAboveBookcase = true;
                BirdControl_House._goPointNum = 2;
                break;

            case 35:
                CameraControl_House.isFreeLook = true;
                CameraControl_House.isLookDoorPlot = false;
                DoorControl_House.isBird = false;
                InteractableControl_House.isColliderActive[3] = true;
                UIAboveObject_House.isAboveDoor = false;
                UIAboveObject_House.isAboveBed = true;
                break;
        }
    }
}
