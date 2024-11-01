using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchControl_House : MonoBehaviour
{
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
    public GameObject nextImage;
    public GameObject FinishImage;
    bool isNext = false;
    bool isFinish = false;

    [Header("ToolBox")]
    public GameObject toolBoxUI;
    public GameObject toolBoxBG;
    public GameObject buttonUI;
    public GameObject up, down;
    public Transform openPoint, closePoint;
    bool isOpenBox = false;

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
    public GameObject[] paperOut;
    public static bool isFinishCut = false;
    bool isPaperRotation = false;
    float _rotationSpeed = 90f;
    float _rotation = 0;
    int _cutPaperFinish = 0;

    [Header("ColorUI")]
    public GameObject colorUI;
    public static bool isClickSaveButton = false;
    public static bool[] isChangeColor = new bool[13];

    [Header("Step4")]
    public Transform storyBookPoint;
    public GameObject[] storyBook;
    public static int _storyBookNum = 0;
    public static bool isFinishStoryBook = false;
    bool isPaperAdjustScale;

    void Start()
    {
        Process();
    }

    void Update()
    {
        if (CameraControl_House.isLookWorkbench && isStartProcess)
        {
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

        //�yԇ
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _storyBookNum = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _storyBookNum = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _storyBookNum = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _storyBookNum = 3;
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
            {
                processObject[i].SetActive(true);
            }
            else
            {
                processObject[i].SetActive(false);
            }
        }
        switch (_process)
        {
            case 0:
                toolBoxBG.SetActive(true);
                buttonUI.SetActive(true);
                isPaperAdjustScale = false;
                for (int c = 1; c < isChangeColor.Length; c++)
                {
                    isChangeColor[c] = false;
                }
                break;

            case 1:
                isAppaerPaper = true;
                break;

            case 2:
                isPaperRotation = true;
                break;

            case 3:
                colorUI.SetActive(true);
                break;

            case 4:
                storyBook[_storyBookNum].transform.position = storyBookPoint.position;
                storyBook[_storyBookNum].transform.rotation = storyBookPoint.rotation;
                storyBook[_storyBookNum].GetComponent<Animator>().SetBool("isOpen", true);
                colorUI.SetActive(false);
                break;
        }
    }
    void Paper()
    {
        for (int i = 0; i < paper.Length; i++)
        {
            if (i == _paperNum)
            {
                paper[i].SetActive(true);
            }
            else
            {
                paper[i].SetActive(false);
            }
        }
    }
    void WorkbenchUI()
    {
        nextImage.SetActive(isNext);
        FinishImage.SetActive(isFinish);

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

        if (Input.GetKeyDown(KeyCode.Y) && isNext)
        {
            isNext = false;
            switch (_process)
            {
                case 1:
                    StartCoroutine(DisappaerChooseUI());
                    break;

                case 2:
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
        if (Input.GetKeyDown(KeyCode.T) && isFinish)
        {
            isFinish = false;
            StartCoroutine(LeaveWorkbench());
        }

        if (isOpenBox)
        {
            up.SetActive(false);
            down.SetActive(true);
            toolBoxBG.transform.position = Vector3.MoveTowards(toolBoxBG.transform.position, openPoint.position, 1f);
        }
        else
        {
            up.SetActive(true);
            down.SetActive(false);
            toolBoxBG.transform.position = Vector3.MoveTowards(toolBoxBG.transform.position, closePoint.position, 1f);
        }
    }
    public void Button_ToolBox()
    {
        isOpenBox = !isOpenBox;
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
        switch (_storyBookNum)
        {
            case 0:
                contentImage[1].GetComponent<Image>().sprite = contentSprite[12];
                contentImage[2].GetComponent<Image>().sprite = contentSprite[13];
                contentImage[3].GetComponent<Image>().sprite = contentSprite[14];
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
                contentImage[2].GetComponent<Image>().sprite = contentSprite[8];
                contentImage[3].GetComponent<Image>().sprite = contentSprite[9];
                break;
        }
    }
    void StampStay()
    {
        stamp.transform.position = Vector3.MoveTowards(stamp.transform.position, stampStartPos.position, _speed * Time.deltaTime);
        stamp.transform.rotation = Quaternion.Lerp(stamp.transform.rotation, stampStartPos.rotation, _rotateSpeed * Time.deltaTime);

        if (Vector3.Distance(stamp.transform.position, stampStartPos.position) < 0.05f)
        {
            isAppaerStamp = false;
            _process = 2;
            Process();
        }
    }
    public void Button_ChoosePattern(int num)
    {
        isNext = true;
        selectImage.SetActive(true);
        selectImage.transform.position = contentImage[num].transform.position;
        switch (_storyBookNum)
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
                _rotation = _rotationSpeed * Time.deltaTime;  // ������ת
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rotation = -_rotationSpeed * Time.deltaTime;  // ������ת
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
        isNext = FinishedColoring();
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
        {
            isPaperAdjustScale = true;
        }
        if (isFinishStoryBook)
        {
            toolBoxBG.SetActive(false);
            buttonUI.SetActive(false);
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
        storyBook[_storyBookNum].GetComponent<Animator>().SetBool("isOpen", false);
        yield return new WaitForSeconds(0.7f);
        paper[_paperNum].SetActive(false);
        yield return new WaitForSeconds(0.3f);
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        _process = 0;
        workbenchUI.SetActive(false);
        storyBook[_storyBookNum].SetActive(false);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookStorkBook = false;
        CameraControl_House.isLookWorkbench = false;
    }
}
