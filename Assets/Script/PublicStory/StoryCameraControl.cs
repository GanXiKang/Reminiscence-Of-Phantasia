using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCameraControl : MonoBehaviour
{
    [Header("CameraMovement")]
    public float _smoothTime = 0.5f;
    public static bool isFollow;
    private Vector3 velocity = Vector3.zero;
    Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        isFollow = true;

        FindObjectOfType<CameraShake>().TriggerShake(0.5f);
    }

    void LateUpdate()
    {
        if (isFollow)
        {
            Vector3 _targetPosition = player.position;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref velocity, _smoothTime);
        }
    }
}
