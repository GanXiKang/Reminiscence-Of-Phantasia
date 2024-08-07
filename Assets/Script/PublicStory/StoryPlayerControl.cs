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
        playerInput.enabled = isCanMove;
    }

    void FixedUpdate()
    {
        StoryPlayerMove();
        StoryPlayerOnTheGround();
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _storyMoveInput = new Vector3(-input.y, 0f, input.x);
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
        return StoryInteractableControl.isPickedUp;
    }
}
