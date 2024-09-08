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
        }
        if (isHappy)
        {
            anim.SetBool("isHappy", true);
        }
        if (isWave)
        {
            anim.SetBool("isWave", true);
        }
        if (isBye)
        {
            anim.SetBool("isBye", true);
        }
        if (isBag)
        {
            anim.SetBool("isBag", true);
        }
        if (isBag_Out)
        {
            anim.SetBool("isBag_Out", true);
        }
    }
}
