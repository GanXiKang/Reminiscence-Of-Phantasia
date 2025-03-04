using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUIControl_Girl : MonoBehaviour
{
    [Header("DialogueUI")]
    public GameObject dialogueUI;
    public static bool isDialogue;

    [Header("StoryUI")]
    public GameObject storyUI;
    public static bool isStoryStart;
    public static bool isStoryEnding;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Update()
    {
        dialogueUI.SetActive(isDialogue);
        storyUI.SetActive(isStoryStart || isStoryEnding);
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
    }
}
