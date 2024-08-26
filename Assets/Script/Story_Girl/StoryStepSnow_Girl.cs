using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStepSnow_Girl : MonoBehaviour
{
    void StepOnSnow()
    {
        StoryThermometerControl_Girl.isStepOnSnow = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StoryThermometerControl_Girl.isStepOnSnow = true;
            Invoke("StepOnSnow", 2f);
        }
    }
}
