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
    public GameObject effects;
    public static bool isTransform = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
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

        if (isTransform)
        {
            effects.SetActive(true);
            isTransform = false;
        }
    }
}
