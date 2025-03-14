using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGhostControl_Prince : MonoBehaviour
{
    public Transform playerLeftPoint;
    public Transform playerRightPoint;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Direction", 1);
    }

    
    void Update()
    {
        
    }
}
