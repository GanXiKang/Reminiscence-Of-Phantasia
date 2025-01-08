using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoryFollowControl_Momotaro : MonoBehaviour
{
    GameObject player;
    Animator anim;
    NavMeshAgent agent;

    public int _who;
    public GameObject mountainScene;
    public GameObject plazaScene;

    float _followDistance = 11f;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);

        switch (_who)
        {
            case 12:
                if (stateInfo.IsName("GoMountain") && stateInfo.normalizedTime >= 1f && mountainScene.activeSelf)
                    FollowPlayer();
                else
                    anim.applyRootMotion = false;
                break;

            case 13:
                if (stateInfo.IsName("GoPlaza") && stateInfo.normalizedTime >= 1f && plazaScene.activeSelf)
                    FollowPlayer();
                else
                    anim.applyRootMotion = false;
                break;
        }
    }

    void FollowPlayer()
    {
        anim.applyRootMotion = true;
        agent.updateRotation = false;

        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToTarget > _followDistance)
        {
            Vector3 directionToTarget = (player.transform.position - transform.position).normalized;
            Vector3 newTargetPosition = player.transform.position - directionToTarget * _followDistance;

            agent.SetDestination(newTargetPosition);
        }
        else
        {
            agent.ResetPath();
        }
    }
}
