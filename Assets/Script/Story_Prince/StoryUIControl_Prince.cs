﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUIControl_Prince : MonoBehaviour
{
    [Header("DialogueUI")]
    public GameObject dialogueUI;
    public static bool isDialogue;

    [Header("StoryUI")]
    public GameObject storyUI;
    public static bool isStoryStart = true; 
    public static bool isStoryEnding = false;

    [Header("BagUI")]
    public GameObject bagUI;

    [Header("SkillUI")]
    public GameObject skillUI;
    public static bool isSkillActive = false;

    [Header("SuppliesUI")]
    public GameObject suppliesUI;
    public static bool isSuppliesActive = false;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Update()
    {
        dialogueUI.SetActive(isDialogue);
        storyUI.SetActive(isStoryStart || isStoryEnding);
        bagUI.SetActive(!isSuppliesActive);
        skillUI.SetActive(isSkillActive && !isSuppliesActive);
        suppliesUI.SetActive(isSuppliesActive);
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
    }
}
