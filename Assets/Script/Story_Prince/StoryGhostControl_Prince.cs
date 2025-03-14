using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoryGhostControl_Prince : MonoBehaviour
{
    public Transform playerLeftPoint;
    public Transform playerRightPoint;

    Animator anim;
    float _directionGhost;
    public static bool isWarp = false;
    public static bool isNoGem = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Warp();
        
        anim.SetBool("isNoGem", isNoGem);
    }

    void Warp()
    {
        if (isWarp)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            StoryPlayerControl._direction = _directionGhost;
            anim.SetFloat("Direction", _directionGhost);
            anim.SetBool("isWarp", isWarp);

            if (stateInfo.IsName("Warp") && stateInfo.normalizedTime >= 1f)
            {
                
            }
        }
    }
}
