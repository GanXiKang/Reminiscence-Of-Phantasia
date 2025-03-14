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

    [Header("StoryBook")]
    public GameObject[] storyBook;
    public Transform bedcasePoint;
    public static bool isPutBedcase = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        bed.isTrigger = PlayerControl_House.isSleep;

        MoveToTarget();
        PutBedcase();
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
            StartCoroutine(GoToStoryWorld());
            Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, finalRotation, _rotateSpeed * Time.deltaTime);
            playerTransform.rotation = finalRotation;
        }
    }
    void PutBedcase()
    {
        if (!isPutBedcase) return;

        isPutBedcase = false;
        storyBook[GameControl_House._storyNum].transform.position = bedcasePoint.position;
        storyBook[GameControl_House._storyNum].transform.rotation = bedcasePoint.rotation;
    }

    IEnumerator GoToStoryWorld()
    {
        yield return new WaitForSeconds(1.2f);
        CameraControl_House.isLookBedcase = true;
        storyBook[GameControl_House._storyNum].GetComponent<Animator>().SetBool("isOpen", true);
        yield return new WaitForSeconds(3f);
        TransitionUIControl.isHouse = false;
        TransitionUIControl.isTransitionUIAnim_In = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(GameControl_House._storyNum + 1);
        CameraControl_House.isLookBed = false;
        CameraControl_House.isLookBedcase = false;
        PlayerControl_House.isSleep = false;
    }
}
