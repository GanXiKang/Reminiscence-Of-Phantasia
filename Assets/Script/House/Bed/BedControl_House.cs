using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedControl_House : MonoBehaviour
{
    GameObject player;
    PlayerControl_House PlayerControl;

    [Header("Bed")]
    public BoxCollider bed;
    public Transform sleepPoint;

    public static bool isGoStoryWorld = false;
    public static int _storyNum;

    void Start()
    {
        player = GameObject.Find("Player");
        PlayerControl = player.GetComponent<PlayerControl_House>();
    }

    void Update()
    {
        Sleep();
        StoryWorld();
        Leave();

        if (Input.GetKeyDown(KeyCode.Alpha7)) //úy‘á
        {
            _storyNum = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) //úy‘á
        {
            _storyNum = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) //úy‘á
        {
            _storyNum = 4;
        }
    }

    void Sleep()
    {
        bed.isTrigger = PlayerControl_House.isSleep;

        if (PlayerControl_House.isSleep)
        {
            player.transform.position = sleepPoint.position;
            PlayerControl.enabled = false;
        }
    }
    void StoryWorld()
    {
        if (isGoStoryWorld)
        {
            isGoStoryWorld = false;
            TransitionUIControl.isHouse = false;
            TransitionUIControl.isTransitionUIAnim_In = true;
            Invoke("GoToStoryWorld", 3f);
        }
    }
    void GoToStoryWorld()
    {
        SceneManager.LoadScene(_storyNum);
        CameraControl_House.isLookBed = false;
        PlayerControl_House.isSleep = false;
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
