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
        Vector3 desiredPosition = player.position + cameraOffset;

        RaycastHit hit;
        if (Physics.Raycast(player.position, cameraOffset.normalized, out hit, raycastDistance, wallLayers))
        {
            print("Yes");
            float distance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
            desiredPosition = player.position + cameraOffset.normalized * distance;
        }
        else
        {
            print("No");
        }

        transform.position = desiredPosition;
        transform.LookAt(player);
    }
}
