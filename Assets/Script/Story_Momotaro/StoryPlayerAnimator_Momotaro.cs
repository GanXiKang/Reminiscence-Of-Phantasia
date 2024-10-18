using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayerAnimator_Momotaro : MonoBehaviour
{
    Animator anim;

    public static bool isDonkey = false;
    public static bool isRaccoon = false;
    public static bool isParrot = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("isDonkey", isDonkey);
        anim.SetBool("isRaccoon", isRaccoon);
        anim.SetBool("isParrot", isParrot);
    }
}
