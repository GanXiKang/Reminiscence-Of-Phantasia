using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNpcAnimator_Momotaro : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;

    //01Momotaro
    public static bool isHappy_Momo = false;
    public static bool isSad_Momo = true;

    //04Monkey
    public static bool isGold_Monkey = false;
    public static bool isControlled_Monkey = false;

    //06Raccoon
    public static bool isStone = false;

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
                anim.SetBool("isGold", isGold_Monkey);
                anim.SetBool("isControlled", isControlled_Monkey);
                break;

            case 6:
                anim.SetBool("isStone", isStone);
                break;
        }
    }
}
