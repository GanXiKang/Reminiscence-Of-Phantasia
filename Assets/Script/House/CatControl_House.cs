using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl_House : MonoBehaviour
{
    Animator anim;

    public static bool isSad = false;
    public static bool isHappy = false;
    public static bool isWave = false;
    public static bool isBye = false;
    public static bool isBag = false;
    public static bool isBag_Out = false;

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
        if (isSad)
        {
            anim.SetBool("isSad", true);
            Invoke("FalseAnimation", 0.2f);
            isSad = false;
        }
        if (isHappy)
        {
            anim.SetBool("isHappy", true);
            Invoke("FalseAnimation", 0.2f);
            isHappy = false;
        }
        if (isWave)
        {
            anim.SetBool("isWave", true);
        }
        else
        {
            anim.SetBool("isWave", false);
        }
        if (isBye)
        {
            anim.SetBool("isBye", true);
            Invoke("FalseAnimation", 0.2f);
            isBye = false;
        }
        if (isBag)
        {
            anim.SetBool("isBag", true);
            isBag = false;
        }
        if (isBag_Out)
        {
            anim.SetBool("isBag_Out", true);
            anim.SetBool("isBag", false);
            Invoke("FalseAnimation", 0.2f);
            isBag_Out = false;
        }
    }
    void FalseAnimation()
    {
        anim.SetBool("isSad", false);
        anim.SetBool("isHappy", false);
        anim.SetBool("isBye", false);
        anim.SetBool("isBag_Out", false);
}
