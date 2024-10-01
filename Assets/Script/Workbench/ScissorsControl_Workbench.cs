using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsControl_Workbench : MonoBehaviour
{
    //Mouse
    float mouseY, mouseX;
    float _moveSpeed = 200f;
    float newZ, newX;
    float minZ = 1f, maxZ = 11f;
    float minX = 1f, maxX = 7f;

    //Point
    public static bool isUseScissors = false;
    public static int _cutPoint = 0;

    //collider
    bool[] isCollider = new bool[5];

    [Header("Line")]
    public GameObject scissorsLine;
    List<GameObject> line = new List<GameObject>(0);

    [Header("Paper")]
    public Transform paper;

    void Start()
    {
        ClearColliderBool();
    }
    void Update()
    {
        if (WorkbenchControl_House._process == 2)
        {
            ScissorsMove();

            Vector3 scissorsPos = transform.position + new Vector3(0f, 0f, -0.5f);
            if (Input.GetMouseButtonDown(0))
            {
                isUseScissors = true;
                line.Add(Instantiate(scissorsLine, scissorsPos, paper.rotation, paper));
            }
            if (Input.GetMouseButtonUp(0))
            {
                isUseScissors = false;
                Invoke("ClearLine", 0.2f);
            }
            if (isUseScissors)
            {
                line[line.Count - 1].transform.position = scissorsPos;
                line[line.Count - 1].transform.rotation = paper.rotation;
            }
        }
    }

    void ScissorsMove()
    {
        mouseY = Input.GetAxis("Mouse Y") * _moveSpeed * Time.deltaTime;
        mouseX = Input.GetAxis("Mouse X") * _moveSpeed * Time.deltaTime;
        newZ = Mathf.Clamp(transform.position.z + mouseY, minZ, maxZ);
        newX = Mathf.Clamp(transform.position.x + mouseX, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, newZ);
    }
    void ClearLine()
    {
        for (int i = 0; i < line.Count; i++)
        {
            Destroy(line[i]);
        }
        line.Clear();
        ClearColliderBool();
    }
    void ClearColliderBool()
    {
        for (int i = 1; i < isCollider.Length; i++)
        {
            isCollider[i] = false;
        }
    }
    void Cut()
    {
        WorkbenchControl_House.isFinishCut = true;
        isUseScissors = false;
        Invoke("ClearLine", 0.2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "PaperOut")
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
                Invoke("ClearLine", 0.2f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "PaperOut")
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
