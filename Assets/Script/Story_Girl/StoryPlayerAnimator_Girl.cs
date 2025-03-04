using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayerAnimator_Girl : MonoBehaviour
{
    Animator anim;
    public static bool isCold;
    public static bool isIronRod;
    public static bool isMatch;
    public static bool isStrongWind;

    void Start()
    {
        anim = GetComponent<Animator>();

        //isCold;
        //isIronRod;
        //isMatch;
        //isStrongWind;
    }

    void Update()
    {
        anim.SetBool("isCold", isCold);
        anim.SetBool("isIronRod", isIronRod);
        anim.SetBool("isMatch", isMatch);
        anim.SetBool("isStrongWind", isStrongWind);
    }
}
