using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    public float pushSpeed = 5f; // 玩家被推开速度
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
            // 持续推动玩家离开物体
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, playerTransform.position + pushDirection, pushSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInsideTrigger = true;
            playerTransform = other.transform;

            // 计算玩家相对于触发器的推开方向
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
