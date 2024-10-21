using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    [Header("Wind")]
    public GameObject wind;
    public float windCooldown = 3f; // �����ʱ��
    public float windDuration = 2f; // ������ʱ��
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform originalPoint;
    public static bool isBlownAway = false;

    void OnEnable()
    {
        StartCoroutine(WindCycle());
    }

    void Update()
    {
        wind.SetActive(isWindActive);
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

    void OnDisable()
    {
        StopCoroutine(WindCycle());
    }
}
