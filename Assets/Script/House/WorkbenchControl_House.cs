using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchControl_House : MonoBehaviour
{
    int _process = 0;

    [Header("UI")]
    public GameObject chooseUI;
    public GameObject panel;
    public GameObject[] content;
    public static bool isAnim;

    void Start()
    {
        isAnim = true;
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
    }

    void Step1_Choose()
    {
        if (isAnim)
        {
            StartCoroutine(GetItemAnimation());
        }
    }
    IEnumerator GetItemAnimation()
    {
        chooseUI.SetActive(true);
        for (int v = 0; v <= 5; v++)
        {
            panel.GetComponent<RectTransform>().localScale = new Vector3(20f, v, 1f);
            yield return new WaitForSeconds(0.04f);
        }
        content[0].SetActive(true);
        isAnim = false;
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
