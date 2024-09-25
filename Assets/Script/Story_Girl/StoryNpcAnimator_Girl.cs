using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNpcAnimator_Girl : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;

    //01Irls
    public static float _direction;
    public static bool isHappy_Irls;
    public static bool isSurprise;
    public static bool isLeaveStreet;
    public static bool isGoGrandmom;
    //02GarbageCan
    public static bool isOpen;
    //04Child_Yan
    public static bool isAngry;
    public static bool isLeave;
    //05Child_Cri
    public static bool isHappy_Cri;
    //09Camping
    public static bool isNormal;
    //11Wolf
    public static bool isScared;
    public static bool isRunAway;

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
                break;

            case 9:
                anim.SetBool("isNormal", isNormal);
                break;

            case 11:
                anim.SetBool("isScared", isScared);
                anim.SetBool("isRunAway", isRunAway);
                break;
        }
    }
}
