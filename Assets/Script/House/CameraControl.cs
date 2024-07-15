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
            // �侀�����ˠ���
            float newDistance = hit.distance - minDistance;
            transform.position = player.position - directionToPlayer * newDistance;

            // �{ԇ�侀
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.DrawLine(hit.point, player.position, Color.yellow);
        }
        else
        {
            // �]���������ڣ�����Ĭ�Jλ��
            Vector3 desiredPosition = player.position - directionToPlayer * defaultDistance;
            transform.position = desiredPosition;

            // �{ԇ�侀
            Debug.DrawLine(transform.position, player.position, Color.green);
        }

        // �_�����Cʼ�K�������
        transform.LookAt(player);
    }
}
