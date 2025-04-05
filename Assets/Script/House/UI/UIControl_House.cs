using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl_House : MonoBehaviour
{
    [Header("TransitionUI")]
    public GameObject transitionUI;

    [Header("DialogueUI")]
    public GameObject dialogueUI;
    public static bool isDialogue = false;

    [Header("CoinUI")]
    public GameObject coinUI;
    public static bool isCoinAppear = false;

    [Header("EndingUI")]
    public GameObject endingUI;
    public static bool isEndingAppear = false;

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
        dialogueUI.SetActive(isDialogue);
        coinUI.SetActive(isCoinAppear);
        endingUI.SetActive(isEndingAppear);
    }
}
