using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    public float pushSpeed = 5f; // ��ұ��ƿ��ٶ�
    private bool isInsideTrigger = false;
    private Transform playerTransform;
    private Vector3 pushDirection;

    void Start()
    {
        
    }

    void Update()
    {
        if (isInsideTrigger && playerTransform != null)
        {
            // �����ƶ�����뿪����
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, playerTransform.position + pushDirection, pushSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideTrigger = true;
            playerTransform = other.transform;

            // �����������ڴ��������ƿ�����
            pushDirection = (playerTransform.position - transform.position).normalized;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = false;
        }
    }
}
