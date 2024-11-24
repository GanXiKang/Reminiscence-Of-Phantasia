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
    float _moveSpeed = 3f;
    float _rotateSpeed = 10f;

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

        MoveToTarget();
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
    }

    void MoveToTarget()
    {
        if (!isMovingToBed) return;

        Transform playerTransform = player.transform;
        CharacterController cc = player.GetComponent<CharacterController>();

        Vector3 direction = (bedPos.position - playerTransform.position).normalized;
        if (direction.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }

        Vector3 move = direction * _moveSpeed * Time.deltaTime;
        cc.Move(move);

        if (Vector3.Distance(playerTransform.position, bedPos.position) < 0.5f)
        {
            isMovingToBed = false;

            Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, finalRotation, _rotateSpeed * Time.deltaTime);
            playerTransform.rotation = finalRotation;
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
