using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_StartMovie : MonoBehaviour
{
    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
    }

    public void Skip_Button()
    {
        
    }
}
