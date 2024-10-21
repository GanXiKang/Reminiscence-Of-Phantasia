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

    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        print("yes");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isBlownAway = false;
        }
    }
}
