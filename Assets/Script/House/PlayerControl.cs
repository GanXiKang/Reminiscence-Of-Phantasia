using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    CharacterController cc;

    private Vector3 _moveInput;

    public float _moveSpeed = 7f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Vector3 movement = cameraForward * _moveInput.y + Camera.main.transform.right * _moveInput.x;
        print(movement);

        cc.Move(movement * _moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _moveInput = new Vector3(input.x, 0f, input.y);
    }
}
