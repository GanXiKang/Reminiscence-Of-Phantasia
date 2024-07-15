using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float defaultDistance = 5f;
    public float minDistance = 2f;
    public float raycastDistance = 10f;
    public LayerMask wallLayers;

    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, distanceToPlayer, wallLayers))
        {
            // 射線碰到了牆壁
            float newDistance = hit.distance - minDistance;
            transform.position = player.position - directionToPlayer * newDistance;

            // 調試射線
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawLine(hit.point, player.position, Color.yellow);
        }
        else
        {
            // 沒有碰到牆壁，保持默認位置
            Vector3 desiredPosition = player.position - directionToPlayer * defaultDistance;
            transform.position = desiredPosition;

            // 調試射線
            Debug.DrawLine(transform.position, player.position, Color.green);
        }

        // 確保相機始終看向玩家
        transform.LookAt(player);
    }
}
