using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNpcAnimator_Girl : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;

    //01Irls
    public static bool isHappy;
    public static bool isSurprise;
    public static bool isTrashCanLid;
    public static bool isWearingRedHood;
    //02GarbageCan

    //03SantaClaus

    //04Child_XiaoYan

    //05Child_XiaoXin

    //06Hunter
    public static bool isNormal;

    //09Camping

    //11Wolf
    public static bool isScared;

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
                if (isHappy)
                {
                    anim.SetBool("isHappy", true);
                    isHappy = false;
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
                anim.SetBool("isTrashCanLid", isTrashCanLid);
                anim.SetBool("isWearingRedHood", isWearingRedHood);
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;

            case 6:
                anim.SetBool("isNormal", isNormal);
                break;

            case 9:
                break;

            case 11:
                anim.SetBool("isScared", isScared);
                break;
        }
    }
}
