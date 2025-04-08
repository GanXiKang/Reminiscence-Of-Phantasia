using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip use, stick, choose, open, finish;
    public AudioClip openBook, closeBook;
    bool isOnce;

    [Header("ProcessObject")]
    public GameObject[] processObject;
    public static int _process;
    bool isStartProcess = true;

    [Header("Paper")]
    public GameObject[] paper;
    int _paperNum = 0;

    [Header("WorkbenchUI")]
    public GameObject workbenchUI;
    public GameObject[] processNum;
    public GameObject[] buttonUI;
    bool isNext = false;
    bool isFinish = false;

    [Header("Step1")]
    public GameObject blankPaper;
    public GameObject stamp;
    public Transform paperEndPos;
    public Transform stampStartPos, stampEndPos;
    float _speed = 2f;
    float _rotateSpeed = 6f;
    bool isAppaerPaper = false;
    bool isAppaerStamp = false;
    bool isStampGo = false;

    [Header("ChooseUI")]
    public GameObject chooseUI;
    public GameObject chooseBG;
    public GameObject selectImage;
    public GameObject[] contentImage;
    public Sprite[] contentSprite;
    
    [Header("Step2")]
    public GameObject scissors;
    public GameObject teachHint;
    public GameObject[] paperOut;
    public static bool isFinishCut = false;
    bool isTeachHint = false;
    bool isPaperRotation = false;
    float _rotationSpeed = 90f;
    float _rotation = 0;
    int _cutPaperFinish = 0;

    [Header("ColorUI")]
    public GameObject colorUI;
    public GameObject[] colorLock;
    public Sprite singleBox;
    public static bool isFinishClickColor = false;
    public static bool isFinishColor = false;
    public static bool[] isChangeColor = new bool[13];
    public static bool[] isColorUnlock = new bool[10];
    public static bool isRenewColorLock = false;

    [Header("Step4")]
    public Transform storyBookPoint;
    public Transform originalPoint;
    public GameObject[] storyBook;
    public GameObject naniEF;
    public static bool isFinishStoryBook = false;
    bool isPaperAdjustScale;

    void Start()
    {
        _process = 0;
        Process();
    }

    void Update()
    {
        if (CameraControl_House.isLookWorkbench && isStartProcess)
        {
            BGM.PlayOneShot(use);
            isStartProcess = false;
            Invoke("StartProcess", 0.8f);
        }

        WorkbenchUI();
        switch (_process)
        {
            case 1:
                Step1_Choose();
                break;
            case 2:
                Step2_Cut();
                break;
            case 3:
                Step3_Color();
                break;
            case 4:
                Step4_Install();
                break;
        }
    }

    void StartProcess()
    {
        workbenchUI.SetActive(true);
        _process = 1;
        Process();
    }

    void Process()
    {
        for (int i = 1; i < processObject.Length; i++)
        {
            if (i == _process)
                processObject[i].SetActive(true);
            else
                processObject[i].SetActive(false);
        }
        switch (_process)
        {
            case 0:
                isPaperAdjustScale = false;
                for (int c = 1; c < isChangeColor.Length; c++)
                    isChangeColor[c] = false;
                break;

            case 1:
                isOnce = true;
                isAppaerPaper = true;
                break;

            case 2:
                isTeachHint = true;
                isPaperRotation = true;
                if (GameControl_House._storyNum == 0)
                {
                    UIControl_House.isDialogue = true;
                    DialogueControl_House.isAutoPlot = true;
                    DialogueControl_House._textCount = 4;
                }
                break;

            case 3:
                isTeachHint = false;
                colorUI.SetActive(true);
                if (GameControl_House._storyNum == 0)
                {
                    UIControl_House.isDialogue = true;
                    DialogueControl_House.isAutoPlot = true;
                    DialogueControl_House._textCount = 5;
                }
                break;

            case 4:
                isOnce = true;
                storyBook[GameControl_House._storyNum].transform.position = storyBookPoint.position;
                storyBook[GameControl_House._storyNum].transform.rotation = storyBookPoint.rotation;
                BGM.PlayOneShot(openBook);
                storyBook[GameControl_House._storyNum].GetComponent<Animator>().SetBool("isOpen", true);
                colorUI.SetActive(false);
                if (GameControl_House._storyNum == 0)
                {
                    UIControl_House.isDialogue = true;
                    DialogueControl_House.isAutoPlot = true;
                    DialogueControl_House._textCount = 6;
                }
                break;
        }
    }
    void Paper()
    {
        for (int i = 0; i < paper.Length; i++)
        {
            if (i == _paperNum)
                paper[i].SetActive(true);
            else
                paper[i].SetActive(false);
        }
    }
    void WorkbenchUI()
    {
        buttonUI[1].SetActive(isNext);
        buttonUI[2].SetActive(isFinish);
        teachHint.SetActive(isTeachHint);

        for (int p = 1; p < processNum.Length; p++)
        {
            if (p == _process)
            {
                processNum[p].transform.localScale = new Vector3(0.9f, 0.9f, 1f);
            } 
            else
            {
                processNum[p].transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            }
        }

        if (!UIControl_House.isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Y) && isNext)
            {
                Button_Next();
            }
            if (isFinish)
            {
                if (isOnce)
                {
                    BGM.PlayOneShot(finish);
                    isOnce = false;
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    Button_Finish();
                }
            }
        }
    }

    public void Button_Next()
    {
        isNext = false;
        switch (_process)
        {
            case 1:
                StartCoroutine(DisappaerChooseUI());
                break;

            case 2:
                isFinishColor = false;
                _process = 3;
                Process();
                break;

            case 3:
                SavePaperColor_Workbench.isSave = true;
                _process = 4;
                Process();
                break;
        }
    }
    public void Button_Finish()
    {
        isFinish = false;
        StartCoroutine(LeaveWorkbench());
    }

    void Step1_Choose()
    {
        if (isAppaerPaper) 
        {
            blankPaper.transform.position = Vector3.MoveTowards(blankPaper.transform.position, paperEndPos.position, _speed * Time.deltaTime);
            if (blankPaper.transform.position == paperEndPos.position)
            {
                isAppaerPaper = false;
                StartCoroutine(AppaerChooseUI());
            }
        }
        if (isAppaerStamp)
        {
            if (isStampGo)
            {
                stamp.transform.position = Vector3.MoveTowards(stamp.transform.position, stampEndPos.position, _speed * Time.deltaTime);
                stamp.transform.rotation = Quaternion.Lerp(stamp.transform.rotation, stampEndPos.rotation, _rotateSpeed * Time.deltaTime);
            }
            if (Vector3.Distance(stamp.transform.position, stampEndPos.position) < 0.01f)
            {
                if (isOnce)
                {
                    BGM.PlayOneShot(stick);
                    isOnce = false;
                }
                isStampGo = false;
                Paper();
                blankPaper.SetActive(false);
                Invoke("StampStay", 1f);
            }
        }

        ChooseButtonUI();
    }
    void ChooseButtonUI()
    {
        switch (GameControl_House._storyNum)
        {
            case 0:
                contentImage[1].GetComponent<Image>().sprite = contentSprite[12];
                contentImage[2].GetComponent<Image>().sprite = contentSprite[13];
                contentImage[2].GetComponent<Button>().interactable = false;
                contentImage[3].GetComponent<Image>().sprite = contentSprite[14];
                contentImage[3].GetComponent<Button>().interactable = false;
                break;

            case 1:
                contentImage[1].GetComponent<Image>().sprite = contentSprite[1];
                contentImage[2].GetComponent<Image>().sprite = contentSprite[2];
                contentImage[3].GetComponent<Image>().sprite = contentSprite[3];
                break;

            case 2:
                contentImage[1].GetComponent<Image>().sprite = contentSprite[4];
                contentImage[2].GetComponent<Image>().sprite = contentSprite[5];
                contentImage[3].GetComponent<Image>().sprite = contentSprite[6];
                break;

            case 3:
                contentImage[1].GetComponent<Image>().sprite = contentSprite[7];
                contentImage[2].GetComponent<Image>().sprite = contentSprite[0];
                contentImage[2].GetComponent<Button>().interactable = false;
                contentImage[3].GetComponent<Image>().sprite = contentSprite[0];
                contentImage[3].GetComponent<Button>().interactable = false;
                break;
        }
    }
    void StampStay()
    {
        stamp.transform.position = Vector3.MoveTowards(stamp.transform.position, stampStartPos.position, _speed * Time.deltaTime);
        stamp.transform.rotation = Quaternion.Lerp(stamp.transform.rotation, stampStartPos.rotation, _rotateSpeed * Time.deltaTime);
        Invoke("StampNextProcess", 0.7f);
    }
    void StampNextProcess()
    {
        isAppaerStamp = false;
        _process = 2;
        Process();
    }
    public void Button_ChoosePattern(int num)
    {
        BGM.PlayOneShot(choose);
        isNext = true;
        selectImage.SetActive(true);
        selectImage.transform.position = contentImage[num].transform.position;
        switch (GameControl_House._storyNum)
        {
            case 0:
                _paperNum = 0;
                break;

            case 1:
                switch (num)
                {
                    case 1:
                        _paperNum = 1;
                        break;

                    case 2:
                        _paperNum = 2;
                        break;

                    case 3:
                        _paperNum = 3;
                        break;
                }
                break;

            case 2:
                switch (num)
                {
                    case 1:
                        _paperNum = 4;
                        break;

                    case 2:
                        _paperNum = 5;
                        break;

                    case 3:
                        _paperNum = 6;
                        break;
                }
                break;

            case 3:
                switch (num)
                {
                    case 1:
                        _paperNum = 7;
                        break;

                    case 2:
                        _paperNum = 8;
                        break;

                    case 3:
                        _paperNum = 9;
                        break;
                }
                break;
        }
        Showcase_House._storyBookPaperNum[GameControl_House._storyNum] = _paperNum;
    }
    IEnumerator AppaerChooseUI()
    {
        yield return new WaitForSeconds(0.2f);
        chooseUI.SetActive(true);
        for (int v = 0; v <= 7; v++)
        {
            chooseBG.GetComponent<RectTransform>().localScale = new Vector3(20f, v, 1f);
            yield return new WaitForSeconds(0.03f);
        }
        contentImage[0].SetActive(true);
    }
    IEnumerator DisappaerChooseUI()
    {
        for (int v = 7; v <= 1; v--)
        {
            chooseBG.GetComponent<RectTransform>().localScale = new Vector3(20f, v, 1f);
            yield return new WaitForSeconds(0.02f);
        }
        contentImage[0].SetActive(false);
        selectImage.SetActive(false); 
        chooseUI.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        isAppaerStamp = true;
        isStampGo = true;
    }
    
    void Step2_Cut()
    {
        PaperRotation();
        CutPaperOutFinish();
    }
    void PaperRotation()
    {
        if (isPaperRotation)
        {
            _rotation = 0;
            if (Input.GetKey(KeyCode.A))
            {
                _rotation = _rotationSpeed * Time.deltaTime;  // 向左旋转
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rotation = -_rotationSpeed * Time.deltaTime;  // 向右旋转
            }
            if (_rotation != 0)
            {
                paper[_paperNum].transform.Rotate(0f, 0f, _rotation);
            }
        }
    }
    void CutPaperOutFinish()
    {
        if (!isFinishCut) return;
        if (ScissorsControl_Workbench._cutPoint == 0) return;

        isFinishCut = false;
        switch(_paperNum)
        {
            case 0:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint], 2f);
                break;

            case 1:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 4].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 4], 2f);
                break;

            case 2:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 8].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 8], 2f);
                break;

            case 3:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 12].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 12], 2f);
                break;

            case 4:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 16].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 16], 2f);
                break;

            case 5:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 20].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 20], 2f);
                break;

            case 6:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 24].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 24], 2f);
                break;

            case 7:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 28].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 28], 2f);
                break;

            case 8:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 32].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 32], 2f);
                break;

            case 9:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 36].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 36], 2f);
                break;

            case 10:
                _cutPaperFinish++;
                paperOut[ScissorsControl_Workbench._cutPoint + 40].GetComponent<Rigidbody>().isKinematic = false;
                Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 40], 2f);
                break;
        }
        if (_cutPaperFinish == 4)
        {
            isNext = true;
            isPaperRotation = false;
            _cutPaperFinish = 0;
            paper[_paperNum].transform.rotation = Quaternion.Euler(90f, 0f, 90f);
        }
    }

    void Step3_Color()
    {
        ColorUnLock();

        isFinishClickColor = FinishedColoring();
        isNext = isFinishColor;
    }
    void ColorUnLock()
    {
        if (!isRenewColorLock) return;

        for (int i = 1; i < colorLock.Length; i++)
        {
            if (isColorUnlock[i])
            {
                colorLock[i].GetComponent<Image>().sprite = singleBox;
                colorLock[i].GetComponent<Button>().interactable = true;
            }
        }
        isRenewColorLock = false;
    }
    bool FinishedColoring()
    {
        switch (_paperNum)
        {
            case 0:
            case 5:
            case 8:
            case 9:
                return isChangeColor[1] &&isChangeColor[2] &&isChangeColor[3] &&isChangeColor[4];

            case 1:
            case 6:
            case 10:
                return isChangeColor[1] && isChangeColor[2] && isChangeColor[3] && isChangeColor[4]
                    && isChangeColor[5] && isChangeColor[6] && isChangeColor[7] && isChangeColor[8];

            case 2:
            case 4:
                return isChangeColor[1] && isChangeColor[2] && isChangeColor[3] && isChangeColor[4]
                    && isChangeColor[5];

            case 3:
            case 7:
                return isChangeColor[1] && isChangeColor[2] && isChangeColor[3] && isChangeColor[4]
                    && isChangeColor[5] && isChangeColor[6] && isChangeColor[7] && isChangeColor[8]
                    && isChangeColor[9] && isChangeColor[10] && isChangeColor[11] && isChangeColor[12];

            default:
                return false;
        }
    }

    void Step4_Install()
    {
        if (!isPaperAdjustScale)
            isPaperAdjustScale = true;

        if (isFinishStoryBook)
        {
            isFinishStoryBook = false;
            CameraControl_House.isLookStorkBook = true;
            Invoke("FinishpPocess", 1.5f);
        }
    }
    void FinishpPocess()
    {
        isFinish = true;
    }

    IEnumerator LeaveWorkbench()
    {
        BGM.PlayOneShot(closeBook);
        buttonUI[0].SetActive(false);
        storyBook[GameControl_House._storyNum].GetComponent<Animator>().SetBool("isOpen", false);
        yield return new WaitForSeconds(0.5f);

        paper[_paperNum].SetActive(false);
        yield return new WaitForSeconds(0.3f);

        naniEF.SetActive(true);
        CameraControl_House.isLookPlayer = true;
        PlayerControl_House.isHappy = true;
        yield return new WaitForSeconds(3.5f);

        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);

        _process = 0;
        workbenchUI.SetActive(false);
        CameraControl_House.isLookPlayer = false;
        CameraControl_House.isLookStorkBook = false;
        CameraControl_House.isLookWorkbench = false;
        PlayerControl_House.isHappy = false;
        InteractableControl_House.isColliderActive[1] = false;
        storyBook[GameControl_House._storyNum].transform.position = originalPoint.position;
        storyBook[GameControl_House._storyNum].transform.rotation = originalPoint.rotation;
        yield return new WaitForSeconds(0.5f);

        buttonUI[0].SetActive(true);
        UIAboveObject_House.isAboveWorkbench = false;
        InteractableControl_House.isBookcasePlotOnce = true;
        switch (GameControl_House._storyNum)
        {
            case 0:
                CameraControl_House.isLookWorkPlot = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House.isCatTalk = true;
                DialogueControl_House._textCount = 22;
                break;

            case 1:
                CameraControl_House.isLookWorkPlot = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House.isBirdTalk = true;
                DialogueControl_House._textCount = 31;
                break;

            case 2:
                CameraControl_House.isLookWorkPlot = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 18;
                break;

            case 3:
                CameraControl_House.isLookWorkPlot = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 47;
                break;
        }
    }
}
