using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStepSnow_Girl : MonoBehaviour
{
    GameObject player;
    ColorLerpControl colorLerpControl;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip step;

    //Text
    public static bool isFirstStepSnow = true;

    void Start()
    {
        player = GameObject.Find("Player");
        colorLerpControl = player.GetComponent<ColorLerpControl>();
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
            BGM.PlayOneShot(step);
            StoryPlayerAnimator_Girl.isCold = true;
            colorLerpControl.enabled = true;
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
            colorLerpControl.enabled = false;
        }
    }
}
