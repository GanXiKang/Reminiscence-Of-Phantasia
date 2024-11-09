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
    public float _gravity = 20f;
    private Vector3 _moveInput;
    private Vector3 _velocity;

    //Animation
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
            transform.rotation = Quaternion.LookRotation(movement);
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
        {
            _velocity.y -= _gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y = 0;
        }
    }
    void Animation()
    {
        anim.SetBool("isHappy", isHappy);
        anim.SetBool("isSleep", isSleep);
    }

    bool isCanMove()
    {
        return CameraControl_House.isFreeLook;
    }
}
