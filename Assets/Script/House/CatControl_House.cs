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
        if()

    }
}
