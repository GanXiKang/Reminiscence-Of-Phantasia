using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayerAnimator_Momotaro : MonoBehaviour
{
    Animator anim;

    public static bool isHuman = false;
    public static bool isDonkey = false;
    public static bool isRaccoon = false;
    public static bool isStone = false;
    public static bool isParrot = false;
    public static bool isFall = false;

    [Header("Effects")]
    public GameObject smoke;
    public GameObject mist;
    public GameObject nervous;
    public static bool isSmokeEF = false;
    public static bool isMistEF = false;
    public static bool isNervousEF = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Animation();
        Effects();
    }

    void Animation()
    {
        isHuman = !isDonkey && !isRaccoon && !isParrot;

        anim.SetBool("isDonkey", isDonkey);
        anim.SetBool("isRaccoon", isRaccoon);
        anim.SetBool("isStone", isStone);
        anim.SetBool("isParrot", isParrot);
        anim.SetInteger("isDance", StoryPerformancesControl_Momotaro._danceNum);

        if (isFall)
        {
            anim.SetBool("isFall", true);
            isFall = false;
        }
        else
        {
            anim.SetBool("isFall", false);
        }
    }
    void Effects()
    {
        if (isSmokeEF)
        {
            smoke.SetActive(true);
            isSmokeEF = false;
        }
        if (isMistEF)
        {
            mist.SetActive(true);
            isMistEF = false;
        }
        if (isNervousEF)
        {
            nervous.SetActive(true);
            isNervousEF = false;
        }
    }
}
