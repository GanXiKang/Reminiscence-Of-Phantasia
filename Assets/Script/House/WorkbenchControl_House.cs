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
    public GameObject chooseUI;
    public GameObject panel;
    public GameObject[] contentImage;
    public Sprite[] contentSprite;
    public Transform paperEndPos;
    public Transform stampStartPos, stampEndPos;
    float _speed = 12f;
    float _rotateSpeed = 6f;
    bool isAppaerPaper = false;
    bool isAppaerStamp = false;
    bool isStampGo = false;
    int clickButtonNumber;

    [Header("Step2")]
    public GameObject scissors;
    public GameObject paper2;
    public GameObject[] paperOut;
    public static bool isFinishCut = false;
    bool isPaperRotation = false;
    float _rotationSpeed = 90f;
    float _rotation = 0;
    int _cutPaperFinish = 0;

    [Header("Step3")]
    public GameObject colorUI;
    public GameObject saveButton;
    public static bool isClickSaveButton = false;
    public static bool[] isChangeColor = new bool[10];

    [Header("Step4")]
    public GameObject pointObject;
    public static bool isFinishStoryBook = false;

    void Start()
    {
        clickButtonNumber = 0;
        _process = 1;

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
                pointObject.SetActive(true);
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
            if (stamp.transform.position == stampEndPos.position)
            {
                isStampGo = false;
                _paperNum = clickButtonNumber;
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
        if (stamp.transform.position == stampStartPos.position)
        {
            isAppaerStamp = false;
            _process = 2;
            Process();
        }
    }
    public void Button_ChoosePattern(int num)
    {
        clickButtonNumber = num;
        StartCoroutine(DisappaerChooseUI());
    }
    IEnumerator AppaerChooseUI()
    {
        yield return new WaitForSeconds(0.2f);
        chooseUI.SetActive(true);
        for (int v = 0; v <= 7; v++)
        {
            panel.GetComponent<RectTransform>().localScale = new Vector3(20f, v, 1f);
            yield return new WaitForSeconds(0.04f);
        }
        contentImage[0].SetActive(true);
    }
    IEnumerator DisappaerChooseUI()
    {
        contentImage[0].SetActive(false);
        for (int v = 7; v <= 1; v--)
        {
            panel.GetComponent<RectTransform>().localScale = new Vector3(20f, v, 1f);
            yield return new WaitForSeconds(0.04f);
        }
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
                paper2.transform.Rotate(0, 0, _rotation);
            }
        }
    }
    void CutPaperOutFinish()
    {
        if (!isFinishCut) return;
        if (ScissorsControl_Workbench._cutPoint == 0) return;

        isFinishCut = false;
        if (_paperNum == 1)
        {
            _cutPaperFinish++;
            paperOut[ScissorsControl_Workbench._cutPoint].GetComponent<Rigidbody>().isKinematic = false;
            Destroy(paperOut[ScissorsControl_Workbench._cutPoint], 2f);
        }
        else
        {
            paperOut[ScissorsControl_Workbench._cutPoint + 4].GetComponent<Rigidbody>().isKinematic = false;
            Destroy(paperOut[ScissorsControl_Workbench._cutPoint + 4], 2f);
        }
        if (_cutPaperFinish == 4)
        {
            _cutPaperFinish = 0;
            _process = 3;
            Invoke("Process", 1f);
        }
    }

    void Step3_Color()
    {
        saveButton.SetActive(FinishedColoring(1));
        if (isClickSaveButton)
        {
            isClickSaveButton = false;
            _process = 4;
            Process();
        }
    }
    bool FinishedColoring(int num)
    {
        switch (num)
        {
            case 1:
                return isChangeColor[1] && isChangeColor[2];

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
            isFinishStoryBook = false;
            _process = 0;
        }
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
