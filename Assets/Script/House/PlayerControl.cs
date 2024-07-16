using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    CharacterController cc;
    PlayerInput playerInput;

    [Header("Camera")]
    public Camera playerCamera;

    [Header("Move")]
    public float _moveSpeed = 7f;
    public static bool isPlayerInput;
    private Vector3 _moveInput;
    

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        isPlayerInput = true;
    }

    void Update()
    {
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Vector3 movement = cameraForward * _moveInput.z + playerCamera.transform.right * _moveInput.x;
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            cc.Move(movement * _moveSpeed * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            isPlayerInput = !isPlayerInput;
            CameraControl.isFreeLook = isPlayerInput;

            playerInput.enabled = isPlayerInput;
        }
    } 

    void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _moveInput = new Vector3(input.x, 0f, input.y);
    }
}
