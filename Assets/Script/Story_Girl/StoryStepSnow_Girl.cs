using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStepSnow_Girl : MonoBehaviour
{
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
        }
    }
}
