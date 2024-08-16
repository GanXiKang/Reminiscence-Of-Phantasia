using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUIControl_Girl : MonoBehaviour
{
    [Header("DialogueUI")]
    public GameObject dialogueUI;
    public static bool isDialogue;

    void Update()
    {
        dialogueUI.SetActive(isDialogue);
    }
}
