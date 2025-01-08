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

        agent.updateRotation = false;  //Y ÝS
        agent.updateUpAxis = false;    //X & Z ÝS
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);
        
        switch (_who)
        {
            case 12:
                if (stateInfo.IsName("GoMountain") && stateInfo.normalizedTime >= 1f && mountainScene.activeSelf)
                {
                    FollowPlayer();
                }
                else
                {
                    anim.applyRootMotion = false;
                    StoryNpcAnimator_Momotaro.isWalk_GSMomo = false;
                }
                break;

            case 13:
                if (stateInfo.IsName("GoPlaza") && stateInfo.normalizedTime >= 1f && plazaScene.activeSelf)
                {
                    FollowPlayer();
                }
                else
                {
                    anim.applyRootMotion = false;
                    StoryNpcAnimator_Momotaro.isWalk_GSMomo = false;
                }
                break;
        }
    }

    void FollowPlayer()
    {
        anim.applyRootMotion = true;

        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToTarget > _followDistance)
        {
            Vector3 directionToTarget = (player.transform.position - transform.position).normalized;
            Vector3 newTargetPosition = player.transform.position - directionToTarget * _followDistance;

            StoryNpcAnimator_Momotaro.isWalk_GSMomo = true;
            agent.SetDestination(newTargetPosition);
            CheckDirection();
        }
        else
        {
            StoryNpcAnimator_Momotaro.isWalk_GSMomo = false;
            agent.ResetPath();
        }
    }
    void CheckDirection()
    {
        Vector3 forward = transform.forward;
        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        Vector3 crossProduct = Vector3.Cross(forward, toPlayer);

        if (crossProduct.y > 0)
            StoryNpcAnimator_Momotaro._direction_GSMomo = 1;
        else
            StoryNpcAnimator_Momotaro._direction_GSMomo = 0;
    }
}
