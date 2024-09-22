using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryPlayerControl : MonoBehaviour
{
    CharacterController cc;
    PlayerInput playerInput;
    Animator anim;

    [Header("Movement")]
    public float _moveSpeed = 12f;
    public float _gravity = 20f;
    private Vector3 _storyMoveInput;
    private Vector3 _storyVelocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playerInput.enabled = isCanMove();

        StoryPlayerMove();
        StoryPlayerOnTheGround();
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _storyMoveInput = new Vector3(input.x, 0f, input.y);

        if (_storyMoveInput.x == 0 && _storyMoveInput.z > 0) // ������ W ��
        {
            _storyMoveInput = new Vector3(-0.71f, 0f, 0.71f);
        }
        else if (_storyMoveInput.x == 0 && _storyMoveInput.z < 0) // ������ S ��
        {
            _storyMoveInput = new Vector3(0.71f, 0f, -0.71f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z == 0) // ������ A ��
        {
            _storyMoveInput = new Vector3(-0.71f, 0f, -0.71f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z == 0) // ������ D ��
        {
            _storyMoveInput = new Vector3(0.71f, 0f, 0.71f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z > 0) // ͬʱ���� W �� A ��
        {
            _storyMoveInput = new Vector3(-1f, 0f, 0f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z > 0) // ͬʱ���� W �� D ��
        {
            _storyMoveInput = new Vector3(0f, 0f, 1f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z < 0) // ͬʱ���� A �� S ��
        {
            _storyMoveInput = new Vector3(0f, 0f, -1f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z < 0) // ͬʱ���� S �� D ��
        {
            _storyMoveInput = new Vector3(1f, 0f, 0f);
        }
    }

    void StoryPlayerMove()
    {
        cc.Move((_storyVelocity + _storyMoveInput * _moveSpeed) * Time.fixedDeltaTime);
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

    bool isCanMove()
    {
        return StoryInteractableControl_Girl.isPlayerMove &&
               !StoryLoadingScene_Girl.isLoading &&
               !StoryUIControl_Girl.isDialogue;
    }
}
