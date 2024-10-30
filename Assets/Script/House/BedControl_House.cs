using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedControl_House : MonoBehaviour
{
    public static bool isGoStoryWorld = false;
    public static int _storyNum;

    void Update()
    {
        StoryWorld();
        Leave();

        if (Input.GetKeyDown(KeyCode.Alpha7)) //�yԇ
        {
            _storyNum = 2;
            print(_storyNum);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) //�yԇ
        {
            _storyNum = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) //�yԇ
        {
            _storyNum = 4;
        }
    }

    void StoryWorld()
    {
        if (isGoStoryWorld)
        {
            isGoStoryWorld = false;
            TransitionUIControl.isHouse = false;
            TransitionUIControl.isTransitionUIAnim_In = true;
            Invoke("GoToStoryWorld", 1f);
        }
    }
    void GoToStoryWorld()
    {
        SceneManager.LoadScene(_storyNum);
        CameraControl_House.isLookBed = false;
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
