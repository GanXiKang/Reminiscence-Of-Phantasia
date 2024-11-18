using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNpcAnimator_Momotaro : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;
    bool isOnce = true;

    //01Momotaro
    public static bool isHappy_Momo = false;
    public static bool isSad_Momo = true;
    //02Goddess
    public static bool isAngry = false;
    public static bool isOutLake = false;
    public static bool isBackLake = false;
    //04Monkey
    public static bool isWalk_Monkey = false;
    public static bool isWalkGold_Monkey = false;
    public static bool isGold_Monkey = false;
    public static bool isControlled_Monkey = false;
    public static bool isGoMountain = false;
    //06Raccoon
    public static bool isStone = false;
    //07Dog
    public static bool isSliver_Dog = false;
    public static bool isControlled_Dog = false;
    //08Chicken
    public static bool isGold_Chicken = false;
    public static bool isControlled_Chicken = false;
    //09Parrot
    public static bool isWalk_Parrot = false;
    public static bool isPerformance = false;
    public static bool isExcited = false;
    public static bool isLeave = false;

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
                if (isHappy_Momo)
                {
                    anim.SetBool("isHappy", true);
                    isHappy_Momo = false;
                }
                else
                {
                    anim.SetBool("isHappy", false);
                }
                anim.SetBool("isSad", isSad_Momo);
                break;

            case 2:
                anim.SetBool("isAngry", isAngry);
                anim.SetBool("isOut", isOutLake);
                if (isBackLake && isOutLake)
                {
                    isOutLake = false;
                    anim.SetBool("isBack", true);
                }
                else 
                {
                    isBackLake = false;
                    anim.SetBool("isBack", false);
                }
                break;

            case 4:
                if (isWalk_Monkey)
                {
                    if (isOnce)
                    {
                        isOnce = false;
                        anim.SetBool("isWalk", true);
                        Invoke("FalseWalkAnim", 10f);
                    }
                }
                else
                {
                    anim.SetBool("isWalk", false);
                }
                if (isWalkGold_Monkey)
                {
                    if (isOnce)
                    {
                        isOnce = false;
                        anim.SetBool("isWalkGold", true);
                        Invoke("FalseWalkAnim", 10f);
                    }
                }
                else
                {
                    anim.SetBool("isWalkGold", false);
                }
                anim.SetBool("isGold", isGold_Monkey);
                anim.SetBool("isControlled", isControlled_Monkey);
                anim.SetBool("isGoMountain", isGoMountain);
                break;

            case 6:
                anim.SetBool("isStone", isStone);
                break;

            case 7:
                anim.SetBool("isSliverFlute", isSliver_Dog);
                anim.SetBool("isControlled", isControlled_Dog);
                break;

            case 8:
                anim.SetBool("isGoldFlute", isGold_Chicken);
                anim.SetBool("isControlled", isControlled_Chicken);
                break;

            case 9:
                if (isWalk_Parrot)
                {
                    if (isOnce)
                    {
                        isOnce = false;
                        anim.SetBool("isWalk", true);
                        Invoke("FalseWalkAnim", 10f);
                    }
                }
                else
                {
                    anim.SetBool("isWalk", false);
                }
                if (isPerformance)
                {
                    int d = Random.Range(1, 4);
                    anim.SetInteger("Dance", d);
                    anim.SetBool("isPerformance", true);
                    isPerformance = false;
                }
                else
                {
                    anim.SetBool("isPerformance", false);
                }
                anim.SetBool("isExcited", isExcited);
                anim.SetBool("isLeave", isLeave);
                break;
        }
    }

    void FalseWalkAnim()
    {
        isWalk_Monkey = false;
        isWalkGold_Monkey = false;
        isWalk_Parrot = false;
        isOnce = true;
    }
}
