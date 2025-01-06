using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFollowControl_Momotaro : MonoBehaviour
{
    GameObject player;
    Animator anim;

    float _followSpeed = 3f;
    float _followDistance = 2f;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);

        if (stateInfo.IsName("GoMountain") && stateInfo.normalizedTime < 1f) return;

        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position - player.transform.forward * _followDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _followSpeed);
    }
}
