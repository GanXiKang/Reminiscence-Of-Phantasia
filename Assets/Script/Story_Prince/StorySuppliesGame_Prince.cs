using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySuppliesGame_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip gainEnergy;

    void OnEnable()
    {
        GameEnd();
    }

    void Update()
    {
        if (StoryGameControl_Prince.isSuppliesGameEasy)
        {
            StoryGameControl_Prince.isPassGameEasy = true;
        }
        else if (StoryGameControl_Prince.isSuppliesGameHard)
        {
            StoryGameControl_Prince.isPassGameHard = true;
        }
    }

    void GameEnd()
    {
        StoryUIControl_Prince.isSuppliesActive = false;
        if (StoryGameControl_Prince.isPassGameEasy)
        { 
            StoryNpcAnimator_Prince.isFindGem = true;
            StoryGameControl_Prince.isSuppliesGameEasy = false;
        }
        else if (StoryGameControl_Prince.isPassGameHard)
        {
            StoryUIControl_Prince.isDialogue = true;
            StoryDialogueControl_Prince._isAboveWho1 = 2;
            StoryDialogueControl_Prince._isAboveWho2 = 4;
            StoryDialogueControl_Prince._textCount = 41;
            StoryGameControl_Prince.isSuppliesGameHard = false;
        }
    }

    void OnDisable()
    {
        BGM.PlayOneShot(gainEnergy);
        StorySkillControl_Prince.isGainEnegry = true;
        StorySkillControl_Prince._gainEnegryValue = 0.2f;
    }
}
