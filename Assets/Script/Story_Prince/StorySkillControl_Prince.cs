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
        // 獲取 Z 軸旋轉角度，並限制在 0 到 360 度範圍內
        float zRotation = pointer.transform.eulerAngles.z % 360;

        // 計算所屬的區域（每個區域為 30 度）
        int zone = Mathf.FloorToInt(zRotation / 30f) + 1;

        Debug.Log("指針停在第 " + zone + " 區域");
    }
}
