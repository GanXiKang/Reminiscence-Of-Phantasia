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

    void FixedUpdate()
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
        _storyMoveInput = new Vector3(input.y, 0f, input.x);
    }

    void StoryPlayerMove()
    {
        Quaternion rotation = transform.rotation;
        print(rotation);
        //Vector3 adjustedMoveInput = rotation * _storyMoveInput;
        //cc.Move((_storyVelocity + adjustedMoveInput * _moveSpeed) * Time.fixedDeltaTime);
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
}
