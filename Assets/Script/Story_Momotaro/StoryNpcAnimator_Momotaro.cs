using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNpcAnimator_Momotaro : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;

    //01Momotaro
    public static float _direction_Momo = 1;
    public static bool isHappy_Momo = false;
    public static bool isSad_Momo = true;

    //06Raccoon
    public static float _direction_Raccoon = 1;
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
                anim.SetFloat("Direction", _direction_Momo);
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

            case 6:
                anim.SetFloat("Direction", _direction_Raccoon);
                anim.SetBool("isStone", isStone);
                break;
        }
    }
}
