using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl_House : MonoBehaviour
{
    Animator anim;

    public static bool isIdle = false;
    public static bool isIdle_Out = false;
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
        
    }
}
