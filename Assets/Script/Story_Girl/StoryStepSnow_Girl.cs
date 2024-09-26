using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStepSnow_Girl : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip step;

    public static bool isFirstStepSnow = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isFirstStepSnow)
            {
                StoryUIControl_Girl.isDialogue = true;
                StoryDialogueControl_Girl._textCount = 9;
                isFirstStepSnow = false;
            }
            BGM.PlayOneShot(step);
            StoryPlayerAnimator_Girl.isCold = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            StoryThermometerControl_Girl.isStepOnSnow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StoryThermometerControl_Girl.isStepOnSnow = false;
            StoryPlayerAnimator_Girl.isCold = false;
        }
    }
}
