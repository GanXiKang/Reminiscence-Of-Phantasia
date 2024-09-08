using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl_House : MonoBehaviour
{
    Animator anim;

    public static bool isIdle = false;
    public static bool isDeliver = false;
    public static bool isDeliver_Close = false;
    public static bool isHappy = false;
    public static bool isBye = false;

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
        if (isIdle)
        {
            anim.SetBool("isIdle", true);
            isIdle = false;
        }
        if (isDeliver)
        {
            anim.SetBool("isDeliver", true);
            anim.SetBool("isDeliver_Close", false);
            isDeliver = false;
        }
        if (isDeliver_Close)
        {
            anim.SetBool("isDeliver_Close", true);
            anim.SetBool("isDeliver", false);
            isDeliver_Close = false;
        }
        if (isHappy)
        {
            anim.SetBool("isHappy", true);
            Invoke("FalseByAnimation", 0.2f);
            isHappy = false;
        }
        if (isBye)
        {
            anim.SetBool("isBye", true);
            anim.SetBool("isIdle_Out", true);
            anim.SetBool("isIdle", false);
            Invoke("FalseByAnimation", 1f);
            isBye = false;
        }
    }

    void FalseByAnimation()
    {
        anim.SetBool("isHappy", false);
        anim.SetBool("isBye", false);
    }
}
