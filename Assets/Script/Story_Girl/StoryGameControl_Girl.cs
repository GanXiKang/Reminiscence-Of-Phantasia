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
    public static bool isWallActive = true;
    bool isMistEffects = true;

    [Header("GarbageCan")]
    public GameObject trashcanLid;
    public static bool isTrashcanLidActice = true;

    [Header("Resurrection")]
    public Transform streetPoint;
    public Transform forestPoint;
    public static bool isInStreet = true;
    public static bool isResurrection = false;
    public static bool isRenewTemperature = false;
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
    }

    void Update()
    {
        trashcanLid.SetActive(isTrashcanLidActice);

        TeachWall();
        PlayerResurrection();
        MouseCursor();

        if (Input.GetKey(KeyCode.LeftShift))  //¿ì½Ý½¨
        {
            if (Input.GetKeyDown(KeyCode.Q)) //ëxé_¹ÊÊÂ
            {
                StoryUIControl_Girl.isStoryEnding = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6)) //‚÷ËÍ½ÖµÀ´æ™nüc
            {
                isInStreet = true;
                StoryLoadingScene_Girl.isLeftOpen = true;
                StartCoroutine(ResurrectionState());
            }
            if (Input.GetKeyDown(KeyCode.Alpha7)) //‚÷ËÍÉ­ÁÖ´æ™nüc
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
        StoryThermometerControl_Girl._matchQuantity = 10;
        yield return new WaitForSeconds(0.5f);
        playerControl.enabled = true;
    }
}
