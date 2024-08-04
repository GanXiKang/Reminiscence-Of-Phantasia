using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsControl_House : MonoBehaviour
{
    float mouseY;
    float mouseX;
    float _moveSpeed = 60f;
    float newY;
    float newX;
    float minY = -3f, maxY = 4f;
    float minX = 1f, maxX = 3f;
    public static bool isUseScissors = false;
    public static int _cutPoint = 0;

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
        //mouseY = Input.GetAxis("Mouse Y") * _moveSpeed * Time.deltaTime;
        //mouseX = Input.GetAxis("Mouse X") * _moveSpeed * Time.deltaTime;
        //newY = transform.position.y + mouseY;
        //newX = transform.position.x + mouseX;
        //newY = Mathf.Clamp(newY, minY, maxY);
        //newX = Mathf.Clamp(newX, minX, maxX);
        //transform.position = new Vector3(newX, newY, 0f);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;
        //worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        //worldPosition.y = Mathf.Clamp(worldPosition.y, minY, maxY);
        transform.position = worldPosition;
    }
    void ClearLine()
    {
        for (int i = 0; i < line.Count; i++)
        {
            Destroy(line[i]);
        }
        line.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "Point")
            {
                _cutPoint++;
                print(_cutPoint);
            }
            if (other.tag == "Paper")
            {
                print("No");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "Paper")
            {
                print("No");
            }
        }
    }
}
