using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoryGhostControl_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip warp;

    [Header("Transform")]
    public Transform originPoint;
    public Transform playerLeftPoint;
    public Transform playerRightPoint;

    Animator anim;
    float _directionGhost;
    public static bool isWarp = false;
    public static bool isNoGem = false;
    public static bool isDisappear = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        anim.SetFloat("Direction", _directionGhost);
        anim.SetBool("isWarp", isWarp);
        anim.SetBool("isNoGem", isNoGem);
        anim.SetBool("isDisappear", isDisappear);

        Warp();
        Disappear();
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
                BGM.PlayOneShot(warp);
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
                BGM.PlayOneShot(warp);
                if (_directionGhost == 0)
                    _directionGhost = 1;
                else
                    _directionGhost = 0;
            }
        }
    }
    void Disappear()
    {
        if (!isDisappear) return;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Empty"))
        {
            isDisappear = false;
            isWarpOnce = true;
            gameObject.transform.position = originPoint.position;
        }
    }
}
