using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl_House : MonoBehaviour
{
    [Header("TransitionUI")]
    public GameObject transitionUI;

    [Header("DialogueUI")]
    public GameObject dialogueUI;
    public static bool isDialogue = false;

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
        dialogueUI.SetActive(isDialogue);
    }
}
