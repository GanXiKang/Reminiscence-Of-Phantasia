using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayerAnimator_Girl : MonoBehaviour
{
    Animator anim;
    public static bool isCold;
    public static bool isIronRod;
    public static bool isMatch;

    void Start()
    {
        anim = GetComponent<Animator>();

        isCold = false;
        isIronRod = false;
        isMatch = false;
    }

    void Update()
    {
        anim.SetBool("isCold", isCold);
        anim.SetBool("isIronRod", isIronRod);
        anim.SetBool("isMatch", isMatch);
    }
}
