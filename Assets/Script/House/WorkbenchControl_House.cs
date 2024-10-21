using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchControl_House : MonoBehaviour
{
    [Header("ProcessObject")]
    public GameObject[] processObject;
    public static int _process;
    public static int _storyBookNum = 0;

    [Header("Paper")]
    public GameObject[] paper;
    int _paperNum = 0;

    [Header("Step1")]
    public GameObject blankPaper;
    public GameObject stamp;
    public Transform paperEndPos;
    public Transform stampStartPos, stampEndPos;
    float _speed = 12f;
    float _rotateSpeed = 6f;
    bool isAppaerPaper = false;
    bool isAppaerStamp = false;
    bool isStampGo = false;

    [Header("ChooseUI")]
    public GameObject chooseUI;
    public GameObject chooseBG;
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
    public Animator testStoryBook;
    public static bool isFinishStoryBook = false;
    bool isPaperAdjustScale;

    void Start()
    {
        Process();
    }

    void Update()
    {
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

        //測試
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            _process++;
            Process();
        }
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
        Leave();
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
                isAppaerPaper = false;
                break;

            case 3:
                colorUI.SetActive(true);
                isPaperRotation = false;
                break;

            case 4:
                testStoryBook.SetBool("isOpenTest", true);
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

        if (Vector3.Distance(stamp.transform.position, stampStartPos.position) < 0.01f)
        {
            isAppaerStamp = false;
            _process = 2;
            Process();
        }
    }
    public void Button_ChoosePattern(int num)
    {
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
        StartCoroutine(DisappaerChooseUI());
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
                paper[_paperNum].transform.Rotate(0, 0, _rotation);
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
            _cutPaperFinish = 0;
            _process = 3;
            paper[_paperNum].transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            Invoke("Process", 1f);
        }
    }

    void Step3_Color()
    {
        if (isClickSaveButton)
        {
            isClickSaveButton = false;
            _process = 4;
            Process();
        }
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
    public void Button_SaveTexture()
    {
        isClickSaveButton = true;
        SavePaperColor_Workbench.isSave = true;
    }

    void Step4_Install()
    {
        if (isFinishStoryBook)
        {
            _process = 0;
            isFinishStoryBook = false;
            testStoryBook.SetBool("isOpenTest", false);
            Invoke("PaperClosebyBook", 1.2f);
        }
        if (!isPaperAdjustScale)
        {
            isPaperAdjustScale = true;
            paper[_paperNum].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
    void PaperClosebyBook()
    {
        paper[_paperNum].SetActive(false);
    }

    void Leave()
    {
        if (CameraControl_House.isLookWorkbench)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveWorkbench());
            }
        }
    }
    IEnumerator LeaveWorkbench()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookWorkbench = false;
    }
}
