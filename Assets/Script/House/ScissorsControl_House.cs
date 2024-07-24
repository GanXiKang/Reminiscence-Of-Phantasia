using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsControl_House : MonoBehaviour
{
    float mouseY;
    float mouseX;
    float _moveSpeed = 12f;
    float minY = -3f, maxY = 4f;
    float minX = 1f, maxX = 3f;
    public static bool isUseScissors = false;
    public static int _cutPoint = 0;

    void Update()
    {
        if (WorkbenchControl_House._process == 2)
        {
            ScissorsMove();
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
               
            }
        }
    }
    void ScissorsMove()
    {
        mouseY = Input.GetAxis("Mouse Y") * _moveSpeed * Time.deltaTime;
        mouseX = Input.GetAxis("Mouse X") * _moveSpeed * Time.deltaTime;
        float newY = transform.position.y + mouseY;
        float newX = transform.position.x + mouseX;
        newY = Mathf.Clamp(newY, minY, maxY);
        newX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(1.5f, newY, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUseScissors)
        {
            if (other.tag == "Point")
            {
                print("Yes");
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
