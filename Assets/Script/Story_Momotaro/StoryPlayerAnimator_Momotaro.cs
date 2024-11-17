using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayerAnimator_Momotaro : MonoBehaviour
{
    Animator anim;

    public static bool isDonkey = false;
    public static bool isRaccoon = false;
    public static bool isStone = false;
    public static bool isParrot = false;

    //Move
    public static bool isFall = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isDonkey", isDonkey);
        anim.SetBool("isRaccoon", isRaccoon);
        anim.SetBool("isStone", isStone);
        anim.SetBool("isParrot", isParrot);
        anim.SetInteger("isDance", StoryPerformancesControl_Momotaro._danceNum);
    }
}
