using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoryNpcAnimator_Prince : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;

    //02Prince
    public static bool isWalk_Prince = false;
    public static bool isLeaveHelp = false;
    public static bool isWet = false;
    public static bool isSmiling_Prince = false;
    public static bool isDrown = false;
    public static bool isPrinceAppear = false;
    //04Swallow
    public static bool isSad_Swallow = false;
    public static bool isSurprise_Swallow = false;
    public static bool isHungry = false;
    public static bool isWalk_Swallow = false;
    public static bool isSmiling_Swallow = false;
    public static bool isFindFood = false;
    public static bool isComeBack = false;
    //05Qian_Child
    public static bool isLeave_Qian = false;
    //06Qian_Adult
    public static bool isShock_Qian = false;
    public static bool isNearPrince = false;
    //07Kang
    public static bool isFindGem = false;
    public static bool isLeave_Kang = false;
    //12Bei
    public static bool isLeave_Bei = false;

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
            case 2:
                anim.SetBool("isWalk", isWalk_Prince);
                anim.SetBool("isLeaveHelp", isLeaveHelp);
                anim.SetInteger("HelpChildQian", StoryInteractableControl_Prince._helpChildQian);
                anim.SetBool("isWet", isWet);
                anim.SetBool("isSmiling", isSmiling_Prince);
                anim.SetBool("isDrown", isDrown);
                anim.SetInteger("SwallowHunger", StoryInteractableControl_Prince._swallowHunger);
                anim.SetBool("isAppear", isPrinceAppear);
                break;

            case 4:
                anim.SetBool("isSad", isSad_Swallow);
                anim.SetBool("isSurprise", isSurprise_Swallow);
                anim.SetBool("isHungry", isHungry);
                anim.SetBool("isWalk", isWalk_Swallow);
                anim.SetBool("isSmiling", isSmiling_Swallow);
                anim.SetBool("isFindFood", isFindFood);
                anim.SetBool("isComeBack", isComeBack);
                anim.SetInteger("SuppliesGame", StoryInteractableControl_Prince._swallowHunger);
                anim.SetBool("isAppear", isPrinceAppear);
                break;

            case 5:
                anim.SetInteger("PrinceHelp", StoryInteractableControl_Prince._helpChildQian);
                anim.SetBool("isLeave", isLeave_Qian);
                break;

            case 6:
                anim.SetBool("isShock", isShock_Qian);
                anim.SetBool("isSmiling", StoryGameControl_Prince.isFutureGood);
                anim.SetBool("isGoodFuture", StoryGameControl_Prince.isFutureGood);
                anim.SetBool("isNearPrince", isNearPrince);
                break;

            case 7:
                anim.SetBool("isFindGem", isFindGem);
                anim.SetBool("isLeave", isLeave_Kang);
                anim.SetBool("isGoodFuture", StoryGameControl_Prince.isFutureGood);
                break;

            case 8:
                anim.SetBool("isNormal", StoryInteractableControl_Prince.isPrinceNoDie);
                if (StoryUIControl_Prince.isSuppliesActive)
                    anim.SetFloat("Direction", 0);
                else
                    anim.SetFloat("Direction", 1);
                break;

            case 9:
                anim.SetBool("isGoodFuture", StoryGameControl_Prince.isFutureGood);
                break;

            case 10:
                anim.SetBool("isHappy", StoryInteractableControl_Prince.isPrinceNoDie);
                anim.SetBool("isGoodFuture", StoryGameControl_Prince.isFutureGood);
                break;

            case 12:
                anim.SetBool("isNeedSauce", StoryInteractableControl_Prince.isNeedSauce);
                anim.SetBool("isLeave", isLeave_Bei);
                if (StoryUIControl_Prince.isSuppliesActive)
                    anim.SetFloat("Direction", 0);
                else
                    anim.SetFloat("Direction", 1);
                break;

            case 11:
            case 18:
            case 20:
            case 23:
                if (StoryUIControl_Prince.isSuppliesActive)
                    anim.SetFloat("Direction", 0);
                else
                    anim.SetFloat("Direction", 1);
                break;
        }
    }
}
