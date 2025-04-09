using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl_House : MonoBehaviour
{
    CharacterController cc;
    PlayerInput playerInput;
    Animator anim;

    [Header("Camera")]
    public Camera playerCamera;

    [Header("Movement")]
    public float _moveSpeed = 7f;
    public float _rotationSpeed = 5f;
    public float _gravity = 20f;
    private Vector3 _moveInput;
    private Vector3 _velocity;

    [Header("Point")]
    public Transform endingPoint;
    public Transform doorPoint;
    public Transform workbenchPoint;
    public static bool isPlayerEndPoint = false;

    //Animation
    public static bool isWave = false;
    public static bool isHappy = false;
    public static bool isSleep = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        playerInput.enabled = isCanMove();

        PlayerMove();
        PlayerOnTheGround();
        Animation();
        PlayerPoint();
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _moveInput = new Vector3(input.x, 0f, input.y);
    }

    void PlayerMove()
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 movement = cameraForward * _moveInput.z + playerCamera.transform.right * _moveInput.x;
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
        cc.Move((_velocity + movement * _moveSpeed) * Time.deltaTime);
    }
    void PlayerOnTheGround()
    {
        if (!cc.isGrounded)
            _velocity.y -= _gravity * Time.deltaTime;
        else
            _velocity.y = 0;
    }
    void Animation()
    {
        anim.SetBool("isWave", isWave);
        anim.SetBool("isHappy", isHappy);
        anim.SetBool("isSleep", isSleep);
    }
    void PlayerPoint()
    {
        if (CameraControl_House.isLookDoorPlot)
        {
            transform.position = doorPoint.position;
            transform.rotation = doorPoint.rotation;
        }

        if (CameraControl_House.isLookWorkPlot)
        {
            transform.position = workbenchPoint.position;
            transform.rotation = workbenchPoint.rotation;
        }

        if (isPlayerEndPoint)
        {
            transform.position = endingPoint.position;
            transform.rotation = endingPoint.rotation;
        }
    }

    bool isCanMove()
    {
        return CameraControl_House.isFreeLook &&
               !CameraStartMove_House.isStartMoveCamera &&
               !SettingControl.isSettingActive &&
               !UIControl_House.isDialogue &&
               !isPlayerEndPoint;
    }
}
