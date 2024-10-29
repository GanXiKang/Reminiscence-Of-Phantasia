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
        }
    }
}
