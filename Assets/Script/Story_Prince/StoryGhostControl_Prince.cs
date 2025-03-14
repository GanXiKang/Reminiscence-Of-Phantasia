using System.Collections;
using System.Collections.Generic;
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
        StoryPlayerControl._direction = _directionGhost;

        anim.SetFloat("Direction", _directionGhost);
        anim.SetBool("isWarp", isWarp);
        anim.SetBool("isNoGem", isNoGem);
    }
}
