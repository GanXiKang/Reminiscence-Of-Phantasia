using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryPlayerControl : MonoBehaviour
{
    CharacterController cc;
    PlayerInput playerInput;

    private Vector3 _storyMoveInput;

    [Header("Movement")]
    public float _moveSpeed = 10f;
    public float _gravity = 20f;

    float _direction = 1;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveSpeed = 15f;
        }
        else
        {
            _moveSpeed = 8f;
        }

        if (!cc.isGrounded)
        {
            _storyMoveInput.y -= _gravity * Time.fixedDeltaTime;
        }

        cc.Move(_storyMoveInput * _moveSpeed * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        
    }
}
