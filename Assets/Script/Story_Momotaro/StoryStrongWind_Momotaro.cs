using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    public float pushSpeed = 5f; // 玩家被推开速度
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
            // 持续推动玩家离开物体
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, playerTransform.position + pushDirection, pushSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isBlownAway = true;
            playerTransform = other.transform;

            // 计算玩家相对于触发器的推开方向
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
