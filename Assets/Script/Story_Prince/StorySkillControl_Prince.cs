using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySkillControl_Prince : MonoBehaviour
{
    public GameObject pointer;
    public float rotationSpeed = 10f;
    public bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            pointer.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotating = !isRotating;
            CheckCurrentZone();
        }
    }

    private void CheckCurrentZone()
    {
        float zRotation = pointer.transform.eulerAngles.z % 360;

        zRotation = (zRotation + 90) % 360;

        int zone = Mathf.FloorToInt(zRotation / 30f) + 1;
        Debug.Log("Ö¸á˜Í£ÔÚµÚ " + zone + " …^Óò");
    }
}
