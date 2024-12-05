using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsControl_Workbench : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip cut;

    [Header("Paper")]
    public Transform paper;
    public GameObject effects;
    public static bool isUseScissors = false;
    public static int _cutPoint = 0;
    bool[] isCollider = new bool[5];
    bool[] isCut = new bool[5];

    void Start()
    {
        ClearColliderBoolStatus();
        ClearCutBoolStatus();
    }

    void ClearColliderBoolStatus()
    {
        for (int i = 1; i < isCollider.Length; i++)
        {
            isCollider[i] = false;
        }
    }
    void ClearCutBoolStatus()
    {
        for (int i = 1; i < isCut.Length; i++)
        {
            isCut[i] = false;
        }
    }

    void Update()
    {
        if (WorkbenchControl_House._process == 2)
        {
            effects.SetActive(isUseScissors);

            if (Input.GetMouseButtonDown(0))
            {
                isUseScissors = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isUseScissors = false;
                ClearColliderBoolStatus();
            }
        }
    }

    void Cut()
    {
        if (isCut[_cutPoint]) return;

        BGM.PlayOneShot(cut);
        isUseScissors = false;
        isCut[_cutPoint] = true;
        WorkbenchControl_House.isFinishCut = true;
        ClearColliderBoolStatus();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "PaperOutCollider")
            {
                if (other.gameObject.name == "Collider1")
                {
                    isCollider[1] = true;
                }
                else if (other.gameObject.name == "Collider2")
                {
                    isCollider[2] = true;
                }
                else if (other.gameObject.name == "Collider3")
                {
                    isCollider[3] = true;
                }
                else if (other.gameObject.name == "Collider4")
                {
                    isCollider[4] = true;
                }
            }
            if (other.tag == "Paper")
            {
                isUseScissors = false;
                ClearColliderBoolStatus();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "PaperOutCollider")
            {
                if (other.gameObject.name == "Collider1")
                {
                    if (isCollider[2])
                    {
                        _cutPoint = 1;
                        Cut();
                    }
                    else if (isCollider[4])
                    {
                        _cutPoint = 4;
                        Cut();
                    }
                }
                else if (other.gameObject.name == "Collider2")
                {
                    if (isCollider[1])
                    {
                        _cutPoint = 1;
                        Cut();
                    }
                    else if (isCollider[3])
                    {
                        _cutPoint = 2;
                        Cut();
                    }
                }
                else if (other.gameObject.name == "Collider3")
                {
                    if (isCollider[2])
                    {
                        _cutPoint = 2;
                        Cut();
                    }
                    else if (isCollider[4])
                    {
                        _cutPoint = 3;
                        Cut();
                    }
                }
                else if (other.gameObject.name == "Collider4")
                {
                    if (isCollider[1])
                    {
                        _cutPoint = 4;
                        Cut();
                    }
                    else if (isCollider[3])
                    {
                        _cutPoint = 3;
                        Cut();
                    }
                }
            }
        }
    }
}
