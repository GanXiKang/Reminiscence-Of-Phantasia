using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl_House : MonoBehaviour
{
    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
    }
}
