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
    //04Monkey
    public static bool isWalk_Monkey = false;
    public static bool isWalkGold_Monkey = false;
    public static bool isGold_Monkey = false;
    public static bool isControlled_Monkey = false;
    //06Raccoon
    public static bool isStone = false;
    //09Parrot
    public static bool isPerformance = false;
    public static bool isExcited = false;

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
                break;

            case 6:
                anim.SetBool("isStone", isStone);
                break;

            case 9:
                if (isPerformance)
                {
                    anim.SetBool("isPerformance", true);
                    isPerformance = false;
                }
                else
                {
                    anim.SetBool("isPerformance", false);
                }
                anim.SetBool("isExcited", isExcited);
                break;
        }
    }

    void FalseWalkAnim()
    {
        isWalk_Monkey = false;
        isWalkGold_Monkey = false;
        isOnce = true;
    }
}
