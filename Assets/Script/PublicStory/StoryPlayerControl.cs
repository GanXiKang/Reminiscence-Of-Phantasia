using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryPlayerControl : MonoBehaviour
{
    CharacterController cc;
    PlayerInput playerInput;

    [Header("Movement")]
    public float _moveSpeed = 12f;
    public float _gravity = 20f;
    private Vector3 _storyMoveInput;
    private Vector3 _storyVelocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        StoryPlayerMove();
        StoryPlayerOnTheGround();

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    playerInput.enabled = !playerInput.enabled;
        //}
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _storyMoveInput = new Vector3(input.x, 0f, input.y);
    }

    void StoryPlayerMove()
    {
        Quaternion rotation = transform.rotation;

        // 将输入向量转换到角色的局部空间
        Vector3 adjustedMoveInput = rotation * _storyMoveInput;

        // 移动角色
        cc.Move((_storyVelocity + adjustedMoveInput * _moveSpeed) * Time.deltaTime);
        //cc.Move((_storyVelocity + _storyMoveInput * _moveSpeed) * Time.deltaTime);
    }
    void StoryPlayerOnTheGround()
    {
        if (!cc.isGrounded)
        {
            _storyVelocity.y -= _gravity * Time.deltaTime;
        }
        else
        {
            _storyVelocity.y = 0;
        }
    }
}
