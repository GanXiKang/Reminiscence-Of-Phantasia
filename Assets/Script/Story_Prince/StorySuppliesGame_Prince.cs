using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySuppliesGame_Prince : MonoBehaviour
{
    void OnEnable()
    {
        GameEnd();
    }

    void Update()
    {
        
    }

    void GameEnd()
    {
        StoryUIControl_Prince.isSuppliesActive = false;
    }

    void OnDisable()
    {
        if (StoryGameControl_Prince.isSuppliesGameEasy)
        {
            StoryNpcAnimator_Prince.isFindGem = true;
            StoryGameControl_Prince.isSuppliesGameEasy = false;
        }
        else if(StoryGameControl_Prince.isSuppliesGameHard)
        {
            StoryGameControl_Prince.isSuppliesGameHard = false;
        }
    }
}
