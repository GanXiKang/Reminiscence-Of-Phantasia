using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl_House : MonoBehaviour
{
    Animator anim;

    public static bool isHappy = false;
    public static bool isWave = false;
    public static bool isBye = false;
    public static bool isBag = false;
    public static bool isBag_On = false;

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
        if (isHappy)
        {
            anim.SetBool("isHappy", true);
            Invoke("FalseByAnimation", 0.2f);
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
            Invoke("FalseByAnimation", 0.2f);
            isBye = false;
        }
        if (isBag)
        {
            anim.SetBool("isBag", true);
            isBag = false;
        }
        if (isBag_On)
        {
            anim.SetBool("isBag_On", true);
            anim.SetBool("isBag", false);
            Invoke("FalseByAnimation", 0.2f);
            isBag_On = false;
        }
    }
    void FalseByAnimation()
    {
        anim.SetBool("isHappy", false);
        anim.SetBool("isBye", false);
        anim.SetBool("isBag_On", false);
    }
}
