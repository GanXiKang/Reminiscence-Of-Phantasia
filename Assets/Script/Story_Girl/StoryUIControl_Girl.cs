using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUIControl_Girl : MonoBehaviour
{
    [Header("DialogueUI")]
    public GameObject dialogueUI;
    public static bool isDialogue;

    [Header("TeachUI")]
    public GameObject storyUI;
    public static bool isStory = true;
    public static bool isStoryEnding = false;

    void Update()
    {
        dialogueUI.SetActive(isDialogue);
        storyUI.SetActive(isStory || isStoryEnding);
    }
}
