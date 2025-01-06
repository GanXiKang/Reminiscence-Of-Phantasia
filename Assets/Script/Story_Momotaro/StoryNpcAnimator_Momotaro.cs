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
    public static int _direction_Momo = 0;
    public static bool isHappy_Momo = false;
    public static bool isSad_Momo = true;
    public static bool isWalk_Momo = false;
    public static int _movePlot = 0;
    //02Goddess
    public static bool isAngry = false;
    public static bool isOutLake = false;
    public static bool isBackLake = false;
    //03Donkey
    public static bool isShy = false;
    public static bool isGift = false;
    //04Monkey
    public static bool isWalk_Monkey = false;
    public static bool isWalkGold_Monkey = false;
    public static bool isGold_Monkey = false;
    public static bool isControlled_Monkey = false;
    public static bool isGoMountain = false;
    public static bool isLeave_Monkey = false;
    public static bool isFindPlayer = false;
    public static bool isFindMomotaro_Monkey = false;
    //05Cat
    public static bool isMeet = false;
    public static bool isCloseEyes = false;
    public static int _dating = 0;
    //06Raccoon
    public static bool isStone = false;
    //07Dog
    public static int _performancesNum = 0;
    public static bool isFindMomotaro = false;
    public static bool isSliver_Dog = false;
    public static bool isControlled_Dog = false;
    //08Chicken
    public static bool isGold_Chicken = false;
    public static bool isControlled_Chicken = false;
    //09Parrot
    public static bool isWalk_Parrot = false;
    public static bool isPerformance = false;
    public static bool isExcited = false;
    public static bool isLeave_Parrot = false;
    //12Gold13SilverMomo
    public static bool isOutLake_GSMomo = false;
    public static bool isGoTarget_GSMomo = false;

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
                anim.SetInteger("Direction", _direction_Momo);
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
                anim.SetBool("isWalk", isWalk_Momo);
                anim.SetInteger("MovePlot", _movePlot);
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

            case 3:
                anim.SetInteger("Dating", _dating);
                anim.SetBool("isShy", isShy);
                anim.SetBool("isGift", isGift);
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
                anim.SetBool("isLeave", isLeave_Monkey);
                anim.SetBool("isFindPlayer", isFindPlayer);
                anim.SetBool("isFindMomotaro", isFindMomotaro_Monkey);
                break;

            case 5:
                anim.SetBool("isCloseEyes", isCloseEyes);
                anim.SetBool("isFinishWork", isMeet);
                anim.SetInteger("Dating", _dating);
                break;

            case 6:
                anim.SetBool("isStone", isStone);
                break;

            case 7:
                anim.SetInteger("Performances", _performancesNum);
                anim.SetBool("isPerpare", StoryGameControl_Momotaro.isParrotActive);
                anim.SetBool("isFind", isFindMomotaro);
                anim.SetBool("isSliverFlute", isSliver_Dog);
                anim.SetBool("isControlled", isControlled_Dog);
                break;

            case 8:
                anim.SetInteger("Performances", _performancesNum);
                anim.SetBool("isPerpare", StoryGameControl_Momotaro.isParrotActive);
                anim.SetBool("isFind", isFindMomotaro);
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
                anim.SetBool("isLeave", isLeave_Parrot);
                break;

            case 12:
                anim.SetBool("isOutLake", isOutLake_GSMomo);
                anim.SetBool("isGoMountain", isGoTarget_GSMomo);
                break;

            case 13:
                anim.SetBool("isOutLake", isOutLake_GSMomo);
                anim.SetBool("isGoPlaza", isGoTarget_GSMomo);
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
