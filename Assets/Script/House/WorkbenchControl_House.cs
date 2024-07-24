using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchControl_House : MonoBehaviour
{
    int _process;

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
    //public GameObject scissors;
    public Transform[] point;
    public LineRenderer tipLine;
    Vector3 direction;
    Vector2 minBounds = new Vector2(-3, -3);
    Vector2 maxBounds = new Vector2(3, 3);
    Color lineColor = Color.white;
    float _lineWidth = 0.2f;
    float _moveSpeed = 5f;
    float _rotationSpeed = 90f;
    float _rotation = 0;
    bool isPaperMove = false;
    bool isUseScissors = false;

    void Start()
    {
        _process = 0;
        clickButtonNumber = 0;
        //paper.GetComponent<SpriteRenderer>().sprite = pattern[0];
        TipLineSetting();
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
            _process = 2;
            isPaperMove = true;
            //isAppaerPaper = true;
        }
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
        PaperMove();
        DrawTipLine();
        Scissors();
    }
    void PaperMove()
    {
        if (isPaperMove)
        {
            _rotation = 0;

            if (Input.GetKey(KeyCode.Q))
            {
                _rotation = _rotationSpeed * Time.deltaTime;  // 向左旋转
            }
            else if (Input.GetKey(KeyCode.E))
            {
                _rotation = -_rotationSpeed * Time.deltaTime;  // 向右旋转
            }
            if (_rotation != 0)
            {
                paper.transform.Rotate(Vector3.forward, _rotation);
            }

            direction = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direction += transform.up; // 向上移动
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction -= transform.up; // 向下移动
            }

            if (direction != Vector3.zero)
            {
                Vector3 newPosition = paper.transform.position + direction * _moveSpeed * Time.deltaTime;

                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

                paper.transform.position = newPosition;
            }
        }
    }
    void TipLineSetting()
    {
        tipLine = gameObject.AddComponent<LineRenderer>();

        tipLine.startColor = lineColor;
        tipLine.endColor = lineColor;
        tipLine.startWidth = _lineWidth;
        tipLine.endWidth = _lineWidth;
        tipLine.positionCount = point.Length;
        tipLine.loop = true;

        tipLine.material = new Material(Shader.Find("Sprites/Default"));
    }
    void DrawTipLine()
    {
        for (int i = 0; i < point.Length; i++)
        {
            tipLine.SetPosition(i, point[i].position);
        }
    }
    void Scissors()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isUseScissors = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isUseScissors = false;
        }
        if (isUseScissors)
        {
            print("Cut!");
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
