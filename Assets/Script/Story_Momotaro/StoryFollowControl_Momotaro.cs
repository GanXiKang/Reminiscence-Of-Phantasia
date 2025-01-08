using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFollowControl_Momotaro : MonoBehaviour
{
    GameObject player;
    Animator anim;

    float _followSpeed = 20f;
    float _followDistance = 12f;

    public int _who;
    public GameObject mountainScene;
    public GameObject plazaScene;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);

        switch (_who)
        {
            case 12:
                if (stateInfo.IsName("GoMountain") && stateInfo.normalizedTime >= 1f && mountainScene.activeSelf)
                    FollowPlayer();
                break;

            case 13:
                if (stateInfo.IsName("GoPlaza") && stateInfo.normalizedTime >= 1f && plazaScene.activeSelf)
                    FollowPlayer();
                break;
        }
    }

    void FollowPlayer()
    {
        //    Vector3 targetPosition = player.transform.position + player.transform.forward * -_followDistance;
        //    print("Target Position: " + targetPosition);
        //    print("Current Position: " + transform.position);

        //    targetPosition.y = transform.position.y;

        //    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _followSpeed);
        //    print("New Position: " + transform.position);
    }
}
