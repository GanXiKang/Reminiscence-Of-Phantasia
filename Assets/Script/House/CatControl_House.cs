using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl_House : MonoBehaviour
{
    Animator anim;

    public static bool isIdle = false;
    public static bool isHappy = false;
    public static bool isBye = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
