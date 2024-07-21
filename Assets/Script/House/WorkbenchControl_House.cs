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
    public Transform endPos;
    float _speed = 1f;
    bool isAppaerPaper = false;
    bool isAppaerChooseUI = false;
    int clickButtonNumber;

    void Start()
    {
        _process = 0;
        clickButtonNumber = 0;
        paper.GetComponent<SpriteRenderer>().sprite = pattern[0];
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
        Leave();

        if (Input.GetKeyDown(KeyCode.C)) //úy‘á
        {
            _process = 1;
            isAppaerPaper = true;
        }
    }

    void Step1_Choose()
    {
        if (isAppaerPaper) 
        {
            paper.transform.position = Vector3.MoveTowards(paper.transform.position, endPos.position, _speed * Time.deltaTime);

            if (paper.transform.position == endPos.position)
            {
                isAppaerPaper = false;
                StartCoroutine(AppaerChooseUI());
            }
        }
        //if (!isAppaerChooseUI)
        //{
           
        //}
    }
    IEnumerator AppaerChooseUI()
    {
        yield return new WaitForSeconds(0.2f);
        isAppaerChooseUI = false;
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
    }
    public void ButtonA()
    {
        clickButtonNumber = 1;
        StartCoroutine(DisappaerChooseUI());
    }
    public void ButtonB()
    {
        clickButtonNumber = 2;
        StartCoroutine(DisappaerChooseUI());
    }
    public void ButtonC()
    {
        clickButtonNumber = 3;
        StartCoroutine(DisappaerChooseUI());
    }

    void Step2_Cut()
    {

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
