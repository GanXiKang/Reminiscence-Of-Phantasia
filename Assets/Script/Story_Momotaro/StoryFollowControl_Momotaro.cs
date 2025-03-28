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

        agent.updateRotation = false;  
        agent.updateUpAxis = false; 
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
                    StoryNpcAnimator_Momotaro.isWalk_GoldMomo = false;
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
                    StoryNpcAnimator_Momotaro.isWalk_SliverMomo = false;
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

            StoryNpcAnimator_Momotaro.isWalk_GoldMomo = mountainScene.activeSelf;
            StoryNpcAnimator_Momotaro.isWalk_SliverMomo = plazaScene.activeSelf;
            agent.SetDestination(newTargetPosition);
        }
        else
        {
            StoryNpcAnimator_Momotaro.isWalk_GoldMomo = false;
            StoryNpcAnimator_Momotaro.isWalk_SliverMomo = false;
            agent.ResetPath();
        }
    }
}
