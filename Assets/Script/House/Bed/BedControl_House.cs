using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedControl_House : MonoBehaviour
{
    GameObject player;

    [Header("Bed")]
    public BoxCollider bed;
    public Transform bedPos;
    public static bool isMovingToBed = false;
    float _moveSpeed = 2f;
    float _rotateSpeed = 5f;

    //StoryBook
    public static bool isGoStoryWorld = false;
    public static int _storyNum;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        bed.isTrigger = PlayerControl_House.isSleep;

        StoryWorld();

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

        if (isMovingToBed)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        Vector3 direction = (bedPos.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        player.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

        player.transform.position += direction * _moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, bedPos.position) < 0.1f)
        {
            isMovingToBed = false;
        }
    }

    void StoryWorld()
    {
        if (isGoStoryWorld)
        {
            isGoStoryWorld = false;
            //TransitionUIControl.isHouse = false;
            //TransitionUIControl.isTransitionUIAnim_In = true;
            Invoke("GoToStoryWorld", 1f);
        }
    }
    void GoToStoryWorld()
    {
        SceneManager.LoadScene(_storyNum);
        CameraControl_House.isLookBed = false;
        PlayerControl_House.isSleep = false;
    }
}
