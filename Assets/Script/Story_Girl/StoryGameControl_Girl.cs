using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("TeachWall")]
    public GameObject teachWall;
    public GameObject mistLeft;
    public GameObject mistRight;
    public static bool isWallActive;
    bool isMistEffects = true;

    [Header("GarbageCan")]
    public GameObject trashcanLid;
    public static bool isTrashcanLidActice;

    [Header("Resurrection")]
    public Transform streetPoint;
    public Transform forestPoint;
    public static bool isInStreet;
    public static bool isResurrection;
    public static bool isRenewTemperature;
    StoryPlayerControl playerControl;

    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<StoryPlayerControl>();

        GameStatic_Start();
    }

    void GameStatic_Start()
    {
        isWallActive = true;
        isTrashcanLidActice = true;
        isInStreet = true;
        isResurrection = false;
        isRenewTemperature = false;
        Debug.Log("Yes");
        StoryInteractableControl_Girl.isInteractableUI = false;
        StoryInteractableControl_Girl.isGiveItem = false;
        StoryInteractableControl_Girl.isBagGetItem = false;
        StoryInteractableControl_Girl.isPlayerMove = true;
        StoryInteractableControl_Girl.isFinallyMatch = false;
        StoryInteractableControl_Girl.isWearingLittleRedHood = false;
        StoryInteractableControl_Girl.isTrashCanLid = false;
        StoryInteractableControl_Girl.isNeedHelp = false;
        StoryInteractableControl_Girl.isGetGift = false;
        StoryInteractableControl_Girl.isCanKillWolf = false;
        StoryInteractableControl_Girl.isFirstAskFind = false;
        StoryInteractableControl_Girl.isAgreeFind = false;
    }

    void Update()
    {
        trashcanLid.SetActive(isTrashcanLidActice);

        TeachWall();
        PlayerResurrection();
        MouseCursor();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StoryUIControl_Girl.isStoryEnding = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6)) 
            {
                isInStreet = true;
                StoryLoadingScene_Girl.isLeftOpen = true;
                StartCoroutine(ResurrectionState());
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                isInStreet = false;
                StoryLoadingScene_Girl.isRightOpen = true;
                StartCoroutine(ResurrectionState());
            }
        }
    }

    void TeachWall()
    {
        teachWall.SetActive(isWallActive);

        if (isWallActive) return;
        if (!isMistEffects) return;

        Invoke("Mist", 5f);
        mistLeft.transform.Translate(0f, 0.05f, 0f);
        mistRight.transform.Translate(0f, -0.05f, 0f);
    }
    void Mist()
    {
        isMistEffects = false;
        Destroy(mistLeft);
        Destroy(mistRight);
    }
    void PlayerResurrection()
    {
        if (!isResurrection) return;

        isResurrection = false;
        StartCoroutine(ResurrectionState());
    }
    void MouseCursor()
    {
        if (isClick)
        {
            Cursor.SetCursor(mouse2, hotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(mouse1, hotSpot, CursorMode.Auto);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            Invoke("FalseisClick", 0.5f);
        }
    }
    void FalseisClick()
    {
        isClick = false;
    }

    IEnumerator ResurrectionState()
    {
        playerControl.enabled = false;
        yield return new WaitForSeconds(0.7f);
        if (isInStreet)
        {
            player.transform.position = streetPoint.position;
        }
        else
        {
            player.transform.position = forestPoint.position;
        }
        isRenewTemperature = true;
        StoryThermometerControl_Girl.isDead = false;
        if (StoryThermometerControl_Girl._matchQuantity < 10)
        {
            StoryThermometerControl_Girl._matchQuantity = 10;
        }
        yield return new WaitForSeconds(0.5f);
        playerControl.enabled = true;
    }
}
