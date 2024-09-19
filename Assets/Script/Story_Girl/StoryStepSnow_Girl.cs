using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStepSnow_Girl : MonoBehaviour
{
    public static bool isFirstStepSnow = true;

    void StepOnSnow()
    {
        StoryThermometerControl_Girl.isStepOnSnow = false;
    }

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
            StoryThermometerControl_Girl.isStepOnSnow = true;
            Invoke("StepOnSnow", 2f);
        }
    }
}
