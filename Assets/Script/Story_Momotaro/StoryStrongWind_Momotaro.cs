using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    public float pushSpeed = 5f; // ��ұ��ƿ��ٶ�
    private Transform playerTransform;
    private Vector3 pushDirection;

    public static bool isBlownAway = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isBlownAway && playerTransform != null)
        {
            // �����ƶ�����뿪����
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, playerTransform.position + pushDirection, pushSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isBlownAway = true;
            playerTransform = other.transform;

            // �����������ڴ��������ƿ�����
            pushDirection = (playerTransform.position - transform.position).normalized;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isBlownAway = false;
        }
    }
}
