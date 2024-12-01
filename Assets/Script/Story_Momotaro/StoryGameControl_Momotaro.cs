using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryGameControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Exit")]
    public GameObject forest;
    public GameObject mountain;
    public GameObject plaza;
    public static bool isForestActive = false;
    public static bool isMountainActive = false;
    public static bool isPlazaActive = false;
    public static bool isMeetDonkey = false;
    bool isOnce = true;

    [Header("Statue")]
    public GameObject catLow;
    public GameObject catFinish;
    public GameObject performancesPoint;
    public static bool isPerformancesPointActive = false;
    public static bool isReadly = false;

    [Header("SceneActive")]
    public GameObject sceneRiver;
    public GameObject sceneForest;
    public GameObject scenePlaza;

    [Header("Npc")]
    public SpriteRenderer momotaro;
    public SpriteRenderer cat;
    public GameObject donkey;
    public GameObject parrot;
    public static bool isParrotActive = false;

    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ExitActive();
        StatueActive();
        NpcActive();
        MouseCursor();
    }

    void ExitActive()
    {
        forest.SetActive(isForestActive);
        mountain.SetActive(isMountainActive);
        plaza.SetActive(isPlazaActive);

        if (isMountainActive && isMeetDonkey && isOnce)
        {
            isOnce = false;
            isPlazaActive = true;
            StoryUIControl_Momotaro.isDialogue = true;
            StoryDialogueControl_Momotaro._textCount = 3;
        }
    }
    void StatueActive()
    {
        catLow.SetActive(!StoryInteractableControl_Momotaro.isFinishWork);
        catFinish.SetActive(StoryInteractableControl_Momotaro.isFinishWork);
        performancesPoint.SetActive(isPerformancesPointActive);
    }
    void NpcActive()
    {
        momotaro.enabled = sceneRiver.activeSelf;
        cat.enabled = scenePlaza.activeSelf;
        parrot.SetActive(isParrotActive);
        if (StoryNpcAnimator_Momotaro._dating == 0)
        {
            donkey.SetActive(sceneForest.activeSelf);
        }
        else
        {
            donkey.SetActive(true);
        }
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
}
