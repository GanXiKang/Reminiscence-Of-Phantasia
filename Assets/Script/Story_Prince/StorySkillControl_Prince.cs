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
        // �@ȡ Z �S���D�Ƕȣ��K������ 0 �� 360 �ȹ�����
        float zRotation = pointer.transform.eulerAngles.z % 360;

        // Ӌ�����ٵą^��ÿ���^��� 30 �ȣ�
        int zone = Mathf.FloorToInt(zRotation / 30f) + 1;

        Debug.Log("ָ�ͣ�ڵ� " + zone + " �^��");
    }
}
