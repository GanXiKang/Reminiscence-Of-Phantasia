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
    public static bool isStoryStart = false;
    public static bool isStoryEnding = false;

    void Update()
    {
        dialogueUI.SetActive(isDialogue);
        storyUI.SetActive(isStoryStart || isStoryEnding);
    }
}
