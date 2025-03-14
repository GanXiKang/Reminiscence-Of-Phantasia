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
        anim.SetFloat("Direction", _directionGhost);
        anim.SetBool("isWarp", isWarp);
        anim.SetBool("isNoGem", isNoGem);

        Warp();
    }

    void Warp()
    {
        if (!isWarp) return;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        _directionGhost = StoryPlayerControl._direction;
        if (_directionGhost == 0)
            gameObject.transform.position = playerLeftPoint.position;
        else
            gameObject.transform.position = playerRightPoint.position;

        if (!isNoGem)
        {
            if (stateInfo.IsName("Normal"))
            {
                isWarp = false;
                if (_directionGhost == 0)
                    _directionGhost = 1;
                else
                    _directionGhost = 0;
            }
        }
        else
        {
            if (stateInfo.IsName("NoGem"))
            {
                isWarp = false;
                if (_directionGhost == 0)
                    _directionGhost = 1;
                else
                    _directionGhost = 0;
            }
        }
    }
}
