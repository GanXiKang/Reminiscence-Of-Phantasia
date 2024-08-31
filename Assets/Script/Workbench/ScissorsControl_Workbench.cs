using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsControl_Workbench : MonoBehaviour
{
    //Mouse
    float mouseY, mouseX;
    float _moveSpeed = 100f;
    float newY, newX;
    float minY = -3f, maxY = 5f;
    float minX = 1f, maxX = 8f;

    //Point
    public static bool isUseScissors = false;
    public static int _cutPoint = 0;

    //collider
    bool isColliderA = false;
    bool isColliderB = false;

    [Header("Line")]
    public GameObject scissorsLine;
    List<GameObject> line = new List<GameObject>(0);

    void Update()
    {
        if (WorkbenchControl_House._process == 2)
        {
            ScissorsMove();

            Vector3 scissorsPos = transform.position + new Vector3(0f, 0f, -0.5f);
            if (Input.GetMouseButtonDown(0))
            {
                isUseScissors = true;
                line.Add(Instantiate(scissorsLine, scissorsPos, transform.rotation, this.transform));
            }
            if (Input.GetMouseButtonUp(0))
            {
                isUseScissors = false;
                Invoke("ClearLine", 0.2f);
            }
            if (isUseScissors)
            {
                line[line.Count - 1].transform.position = scissorsPos;
            }
        }
    }
    void ScissorsMove()
    {
        mouseY = Input.GetAxis("Mouse Y") * _moveSpeed * Time.deltaTime;
        mouseX = Input.GetAxis("Mouse X") * _moveSpeed * Time.deltaTime;
        newY = Mathf.Clamp(transform.position.y + mouseY, minY, maxY);
        newX = Mathf.Clamp(transform.position.x + mouseX, minX, maxX);
        transform.position = new Vector3(newX, newY, 0f);
    }
    void ClearLine()
    {
        for (int i = 0; i < line.Count; i++)
        {
            Destroy(line[i]);
        }
        line.Clear();
        isColliderA = false;
        isColliderB = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "PaperOut")
            {
                if (!isColliderA && !isColliderB)
                {
                    if (other.gameObject.name == "ColliderA_1")
                    {
                        _cutPoint = 1;
                        isColliderA = true;
                    }
                    else if (other.gameObject.name == "ColliderB_1")
                    {
                        _cutPoint = 1;
                        isColliderB = true;
                    }
                    else if (other.gameObject.name == "ColliderA_2")
                    {
                        _cutPoint = 2;
                        isColliderA = true;
                    }
                    else if (other.gameObject.name == "ColliderB_2")
                    {
                        _cutPoint = 2;
                        isColliderB = true;
                    }
                    else if (other.gameObject.name == "ColliderA_3")
                    {
                        _cutPoint = 3;
                        isColliderA = true;
                    }
                    else if (other.gameObject.name == "ColliderB_3")
                    {
                        _cutPoint = 3;
                        isColliderB = true;
                    }
                    else if (other.gameObject.name == "ColliderA_4")
                    {
                        _cutPoint = 4;
                        isColliderA = true;
                    }
                    else if (other.gameObject.name == "ColliderB_4")
                    {
                        _cutPoint = 4;
                        isColliderB = true;
                    }
                }
            }
            if (other.tag == "Paper")
            {
                isUseScissors = false;
                Invoke("ClearLine", 0.2f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isUseScissors)
        {
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
                switch (_cutPoint)
                {
                    case 1:
                        if (other.gameObject.name == "ColliderA_1" && isColliderB)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderB = false;
                        }
                        else if (other.gameObject.name == "ColliderB_1" && isColliderA)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderA = false;
                        }
                        break;

                    case 2:
                        if (other.gameObject.name == "ColliderA_2" && isColliderB)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderB = false;
                        }
                        else if (other.gameObject.name == "ColliderB_2" && isColliderA)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderA = false;
                        }
                        break;

                    case 3:
                        if (other.gameObject.name == "ColliderA_3" && isColliderB)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderB = false;
                        }
                        else if (other.gameObject.name == "ColliderB_3" && isColliderA)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderA = false;
                        }
                        break;

                    case 4:
                        if (other.gameObject.name == "ColliderA_4" && isColliderB)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderB = false;
                        }
                        else if (other.gameObject.name == "ColliderB_4" && isColliderA)
                        {
                            WorkbenchControl_House.isFinishCut = true;
                            isColliderA = false;
                        }
                        break;
                }
            }
        }
    }
}
