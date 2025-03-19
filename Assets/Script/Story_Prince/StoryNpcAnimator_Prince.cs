using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNpcAnimator_Prince : MonoBehaviour
{
    Animator anim;

    [Header("Npc")]
    public int _who;

    //07Kang
    public static bool isFindGem = false;

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
        switch (_who)
        {
            case 7:
                anim.SetBool("isFindGem", isFindGem);
                break;

            case 8:
                anim.SetBool("isNormal", StoryInteractableControl_Prince.isPrinceNoDie);
                break;

            case 10:
                anim.SetBool("isHappy", StoryInteractableControl_Prince.isPrinceNoDie);
                break;
        }
    }
}
