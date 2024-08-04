using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchControl_House : MonoBehaviour
{
    public static int _process;
    public GameObject[] processObject;

[Header("Step1")]
    public GameObject chooseUI;
    public GameObject panel;
    public GameObject[] content;
    public GameObject paper;
    public Sprite[] pattern;
    public Transform paperEndPos;
    public GameObject stamp;
    public Transform stampStartPos, stampEndPos;
    float _speed = 12f;
    float _rotateSpeed = 6f;
    bool isAppaerPaper = false;
    bool isAppaerStamp = false;
    bool isStampGo = false;
    int clickButtonNumber;

    [Header("Step2")]
    public GameObject scissors;
    public GameObject paperB;
    float _rotationSpeed = 90f;
    float _rotation = 0;
    bool isPaperRotation = false;

    void Start()
    {
        _process = 0;
        clickButtonNumber = 0;
        //paper.GetComponent<SpriteRenderer>().sprite = pattern[0];
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

        if (Input.GetKeyDown(KeyCode.C)) //測試
        {
            _process++;
            switch (_process)
            {
                case 1:
                    isAppaerPaper = true;
                    break;

                case 2:
                    isPaperRotation = true;
                    break;

                case 3:
                    break;

                case 4:
                    break;
            }
        }
        Leave();
    }

    void Step1_Choose()
    {
        if (isAppaerPaper) 
        {
            paper.transform.position = Vector3.MoveTowards(paper.transform.position, paperEndPos.position, _speed * Time.deltaTime);
            if (paper.transform.position == paperEndPos.position)
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
                paper.GetComponent<SpriteRenderer>().sprite = pattern[clickButtonNumber];
                Invoke("StampStay", 1f);
            }
        }
    }
    void StampStay()
    {
        stamp.transform.position = Vector3.MoveTowards(stamp.transform.position, stampStartPos.position, _speed * Time.deltaTime);
        stamp.transform.rotation = Quaternion.Lerp(stamp.transform.rotation, stampStartPos.rotation, _rotateSpeed * Time.deltaTime);
        if (stamp.transform.position == stampEndPos.position)
        {
            isAppaerStamp = false;
        }
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
        content[0].SetActive(true);
    }
    IEnumerator DisappaerChooseUI()
    {
        content[0].SetActive(false);
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
    public void Button_ChoosePattern(int num)
    {
        clickButtonNumber = num;
        StartCoroutine(DisappaerChooseUI());
    }

    void Step2_Cut()
    {
        PaperRotation();
        CutFinish();
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
                paper.transform.Rotate(Vector3.forward, _rotation);
            }
        }
    }
    void CutFinish()
    {
        if (ScissorsControl_House._cutPoint >= 4)
        {
            paperB.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void Step3_Color()
    {

    }

    void Step4_Install()
    {

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
        LoadingUIControl_House.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookWorkbench = false;
    }
}
