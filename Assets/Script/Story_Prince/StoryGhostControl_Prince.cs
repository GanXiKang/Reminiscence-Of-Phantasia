using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoryGhostControl_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip warp;
    bool isPlayOnce = true;

    [Header("Transform")]
    public Transform originPoint;
    public Transform princeStatuePoint;
    public Transform playerLeftPoint;
    public Transform playerRightPoint;
    public static bool isWatchSkill = false;

    Animator anim;
    float _directionGhost;
    public static bool isSmile = false;
    public static bool isWarp = false;
    public static bool isNoGem = false;
    public static bool isDisappear = false;
    public static bool isAscend = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        anim.SetFloat("Direction", _directionGhost);
        anim.SetBool("isSmile", isSmile);
        anim.SetBool("isWarp", isWarp);
        anim.SetBool("isNoGem", isNoGem);
        anim.SetBool("isDisappear", isDisappear);
        anim.SetBool("isAscend", isAscend);

        Warp();
        Disappear();
    }

    void Warp()
    {
        if (!isWarp) return;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (isPlayOnce)
        {
            BGM.PlayOneShot(warp);
            isPlayOnce = false;
        }

        if (isWatchSkill || StoryInteractableControl_Prince.isReallyGoFutureGood)
        {
            _directionGhost = StoryPlayerControl._direction;
            if (_directionGhost == 0)
                gameObject.transform.position = playerLeftPoint.position;
            else
                gameObject.transform.position = playerRightPoint.position;
        }
        else
        {
            _directionGhost = 1;
            gameObject.transform.position = princeStatuePoint.position;
        }

        if (!isNoGem)
        {
            if (stateInfo.IsName("Normal"))
            {
                isWarp = false;
                if (_directionGhost == 0)
                    _directionGhost = 1;
                else
                    _directionGhost = 0;

                if (isWatchSkill)
                {
                    StoryUIControl_Prince.isDialogue = true;
                    StoryDialogueControl_Prince._isAboveWho1 = 1;
                    StoryDialogueControl_Prince._textCount = 16;
                }
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

                if (isWatchSkill)
                {
                    StoryUIControl_Prince.isDialogue = true;
                    StoryDialogueControl_Prince._isAboveWho1 = 1;
                    StoryDialogueControl_Prince._textCount = 16;
                }
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
            isWatchSkill = false;
            isPlayOnce = true;
            gameObject.transform.position = originPoint.position;
        }
    }
}
