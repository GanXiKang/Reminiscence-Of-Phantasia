using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    [Header("Wind")]
    public GameObject wind;
    public float windDuration = 3f; // ������ʱ��
    public float windCooldown = 5f; // �����ʱ��
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform originalPoint;
    public static bool isBlownAway = false;

    void Start()
    {
        StartCoroutine(WindCycle());
    }

    IEnumerator WindCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(windCooldown);
            isWindActive = true; // ��翪ʼ
            Debug.Log("��翪ʼ��");
            yield return new WaitForSeconds(windDuration);
            isWindActive = false; // ������
            Debug.Log("��������");
        }
    }
}
