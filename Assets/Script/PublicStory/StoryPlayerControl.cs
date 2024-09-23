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
    float _direction = 1;

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
        Animation();
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _storyMoveInput = new Vector3(input.x, 0f, input.y);

        if (input.x != 0)
        {
            _direction = input.x;
            if (_direction < 0f)
            {
                _direction = 0f;
            }
        }

        if (_storyMoveInput.x == 0 && _storyMoveInput.z > 0) // 仅按下 W 键
        {
            _storyMoveInput = new Vector3(-0.71f, 0f, 0.71f);
        }
        else if (_storyMoveInput.x == 0 && _storyMoveInput.z < 0) // 仅按下 S 键
        {
            _storyMoveInput = new Vector3(0.71f, 0f, -0.71f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z == 0) // 仅按下 A 键
        {
            _storyMoveInput = new Vector3(-0.71f, 0f, -0.71f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z == 0) // 仅按下 D 键
        {
            _storyMoveInput = new Vector3(0.71f, 0f, 0.71f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z > 0) // 同时按下 W 和 A 键
        {
            _storyMoveInput = new Vector3(-1f, 0f, 0f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z > 0) // 同时按下 W 和 D 键
        {
            _storyMoveInput = new Vector3(0f, 0f, 1f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z < 0) // 同时按下 A 和 S 键
        {
            _storyMoveInput = new Vector3(0f, 0f, -1f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z < 0) // 同时按下 S 和 D 键
        {
            _storyMoveInput = new Vector3(1f, 0f, 0f);
        }
    }

    void StoryPlayerMove()
    {
        cc.Move((_storyVelocity + _storyMoveInput * _moveSpeed) * Time.fixedDeltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_moveSpeed == 12)
            {
                _moveSpeed = 20;
            }
            else
            {
                _moveSpeed = 12;
            }
        }
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
    void Animation()
    {
        anim.SetFloat("Direction", _direction);
    }

    bool isCanMove()
    {
        return StoryInteractableControl_Girl.isPlayerMove &&
               !StoryLoadingScene_Girl.isLoading &&
               !StoryUIControl_Girl.isDialogue &&
               !StoryTeachControl.isTeachActive;
    }
}
