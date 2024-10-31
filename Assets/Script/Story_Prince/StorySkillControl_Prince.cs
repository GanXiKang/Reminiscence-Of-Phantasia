using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySkillControl_Prince : MonoBehaviour
{
    [Header("ClockUI")]
    public GameObject clockUI;
    public GameObject pointer;
    bool isClockActice = true;
    bool isRotating = false;
    float _rotationSpeed = 90f;


    void Update()
    {
        clockUI.SetActive(isClockActice);

        ClockRotating();
    }

    void ClockRotating()
    {
        if (isRotating)
        {
            pointer.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotating = !isRotating;
            CheckCurrentZone();
        }
    }
    void CheckCurrentZone()
    {
        float zRotation = pointer.transform.eulerAngles.z % 360;
        int zone = Mathf.FloorToInt(zRotation / 30f) + 1;
        Debug.Log("Ö¸á˜Í£ÔÚµÚ " + zone + " …^Óò");
    }

    public void Button_ClockActive()
    {
       isClockActice = !isClockActice;
    }
}
