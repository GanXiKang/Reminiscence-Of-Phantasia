using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUIControl_Momotaro : MonoBehaviour
{
    [Header("BagUI")]
    public GameObject bagUI;

    [Header("DialogueUI")]
    public GameObject dialogueUI;
    public static bool isDialogue;

    [Header("PerformancesUI")]
    public GameObject performancuesUI;
    public static bool isPerformances = false;

    [Header("StoryUI")]
    public GameObject storyUI;
    public static bool isStoryStart = true;
    public static bool isStoryEnding = false;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Update()
    {
        bagUI.SetActive(isBagUIActive());
        dialogueUI.SetActive(isDialogue);
        performancuesUI.SetActive(isPerformances);
        storyUI.SetActive(isStoryStart || isStoryEnding);
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);

        if (Input.GetKeyDown(KeyCode.H))
        {
            isStoryEnding = true;
        }
    }

    bool isBagUIActive()
    {
        return !StoryPlayerAnimator_Momotaro.isDonkey &&
               !StoryPlayerAnimator_Momotaro.isRaccoon &&
               !StoryPlayerAnimator_Momotaro.isParrot;
    }
}
