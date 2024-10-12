using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedControl_House : MonoBehaviour
{
    public static bool isGoStoryWorld = false;
    public static int _storyNum = 0;

    void Start()
    {
        
    }

    void Update()
    {
        StoryWorld();
        Leave();
    }

    void StoryWorld()
    {
        if (isGoStoryWorld)
        {
            isGoStoryWorld = false;
            TransitionUIControl.isTransitionUIAnim_In = true;
            Invoke("GoToStoryWorld", 2f);
        }
    }
    void GoToStoryWorld()
    {
        SceneManager.LoadScene(_storyNum);
    }
    void Leave()
    {
        if (CameraControl_House.isLookBed)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveBed());
            }
        }
    }

    IEnumerator LeaveBed()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookBed = false;
    }
}
