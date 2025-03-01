using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryPlayerControl : MonoBehaviour
{
    CharacterController cc;
    PlayerInput playerInput;
    Animator anim;

    public static bool isSad;
    public static bool isHappy;
    public static bool isSurprised;

    [Header("Movement")]
    public float _moveSpeed = 12f;
    public float _gravity = 20f;
    private Vector3 _storyMoveInput;
    private Vector3 _storyVelocity;
    public static float _direction = 1;

    [Header("WalkEffects")]
    public GameObject walkEffects;
    bool isWalkEffects;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    void Update()
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
            isWalkEffects = true;
            _direction = input.x;
            if (_direction < 0f)
            {
                _direction = 0f;
            }
        }
        else
        {
            isWalkEffects = false;
        }

        if (_storyMoveInput.x == 0 && _storyMoveInput.z > 0)
        {
            _storyMoveInput = new Vector3(-0.71f, 0f, 0.71f);
        }
        else if (_storyMoveInput.x == 0 && _storyMoveInput.z < 0)
        {
            _storyMoveInput = new Vector3(0.71f, 0f, -0.71f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z == 0)
        {
            _storyMoveInput = new Vector3(-0.71f, 0f, -0.71f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z == 0)
        {
            _storyMoveInput = new Vector3(0.71f, 0f, 0.71f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z > 0)
        {
            _storyMoveInput = new Vector3(-1f, 0f, 0f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z > 0)
        {
            _storyMoveInput = new Vector3(0f, 0f, 1f);
        }
        else if (_storyMoveInput.x < 0 && _storyMoveInput.z < 0)
        {
            _storyMoveInput = new Vector3(0f, 0f, -1f);
        }
        else if (_storyMoveInput.x > 0 && _storyMoveInput.z < 0)
        {
            _storyMoveInput = new Vector3(1f, 0f, 0f);
        }
    }

    void StoryPlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_moveSpeed == 12)
                _moveSpeed = 20;
            else
                _moveSpeed = 12;
        }

        walkEffects.SetActive(isWalkEffects);
        cc.Move((_storyVelocity + _storyMoveInput * _moveSpeed) * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Z))
                _moveSpeed = 50;
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
        anim.SetBool("isSad", isSad);
        anim.SetBool("isHappy", isHappy);
        anim.SetBool("isSurprised", isSurprised);

        if (_storyMoveInput != Vector3.zero)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }

        if (isSad || isHappy || isSurprised)
            Invoke("FalseAnimation", 0.3f);
    }
    void FalseAnimation()
    {
        isSad = false;
        isHappy = false;
        isSurprised = false;
    }

    bool isCanMove()
    {
        return !SettingControl.isSettingActive &&
               !StoryTeachControl.isTeachActive &&

               StoryInteractableControl_Girl.isPlayerMove &&
               !StoryLoadingScene_Girl.isLoading &&
               !StoryUIControl_Girl.isDialogue &&
               !StoryThermometerControl_Girl.isDead &&

               StoryInteractableControl_Momotaro.isPlayerMove &&
               !StoryLoadingScene_Momotaro.isLoading &&
               !StoryUIControl_Momotaro.isDialogue &&
               !StoryRiceDumpling_Momotaro.isEat &&
               !StoryPlayerAnimator_Momotaro.isStone &&
               !StoryUIControl_Momotaro.isPerformances && 
               !StoryStrongWind_Momotaro.isBlownAway &&
               !StoryLoadingScene_Momotaro.isPlotAnimator &&

               StoryInteractableControl_Prince.isPlayerMove &&
               !StoryLoadingScene_Prince.isLoading &&
               !StoryUIControl_Prince.isDialogue;
    }
}
