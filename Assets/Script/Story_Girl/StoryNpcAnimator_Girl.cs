using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNpcAnimator_Girl : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;

    //01Irls
    public static float _direction = 0;
    public static bool isHappy_Irls;
    public static bool isSurprise;
    public static bool isLeaveStreet;
    public static bool isGoGrandmom;
    public static bool isHide;
    //02GarbageCan
    public static bool isOpen;
    //04Child_Yan
    public static bool isAngry;
    public static bool isLeave;
    //05Child_Cri
    public static bool isHappy_Cri;
    //06Hunter
    public static bool isMoveSeeWolf;
    public static bool isFinishLeave;
    //09Camping
    public static bool isNormal;
    //11Wolf
    public static bool isScared;
    public static bool isShotRunAway;
    public static bool isFightRunAway;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Animation();
    }

    void Animation()
    {
        switch (_who)
        {
            case 1:
                IrlsDirection();
                anim.SetFloat("Direction", _direction);
                if (isHappy_Irls)
                {
                    anim.SetBool("isHappy", true);
                    isHappy_Irls = false;
                }
                else
                {
                    anim.SetBool("isHappy", false);
                }
                if (isSurprise)
                {
                    anim.SetBool("isSurprise", true);
                    isSurprise = false;
                }
                else
                {
                    anim.SetBool("isSurprise", false);
                }
                anim.SetBool("isTrashCanLid", StoryInteractableControl_Girl.isTrashCanLid);
                anim.SetBool("isWearingRedHood", StoryInteractableControl_Girl.isWearingLittleRedHood);
                anim.SetBool("isLeaveStreet", isLeaveStreet);
                anim.SetBool("isGoGrandmom", isGoGrandmom);
                anim.SetBool("isHide", isHide);
                break;

            case 2:
                anim.SetBool("isOpen", isOpen);
                break;

            case 4:
                anim.SetBool("isAngry", isAngry);
                anim.SetBool("isLeave", isLeave);
                break;

            case 5:
                anim.SetBool("isHappy", isHappy_Cri);
                break;

            case 6:
                anim.SetBool("isNormal", StoryInteractableControl_Girl.isCanKillWolf);
                anim.SetBool("isMove", isMoveSeeWolf);
                anim.SetBool("isFinish", isFinishLeave);
                break;

            case 9:
                anim.SetBool("isNormal", isNormal);
                break;

            case 11:
                anim.SetBool("isScared", isScared);
                anim.SetBool("isShotRunAway", isShotRunAway);
                anim.SetBool("isFightRunAway", isFightRunAway);
                break;
        }
    }
    void IrlsDirection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _direction = 0;
            print("0 : " + _direction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _direction = 1;
            print("1 : " + _direction);
        }
    }
}
