using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryGameControl_Prince : MonoBehaviour
{
    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    [Header("Plank")]
    public GameObject plank;

    [Header("Npc")]
    public GameObject[] npc;
    public static bool isNpcActive = false;
    bool isNpcActiveTrueOnce = true;

    void Start()
    {
        StartNoNpc();
        //StartCoroutine(GoToMenu());
    }

    void StartNoNpc()
    {
        if (!isNpcActiveTrueOnce) return;

        for (int i = 1; i < npc.Length; i++)
        {
            npc[i].SetActive(isNpcActive);
            if (isNpcActiveTrueOnce && isNpcActive)
                isNpcActiveTrueOnce = false;
        }
    }

    void Update()
    {
        MouseCursor();
        PlankActive();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StoryGhostControl_Prince.isWarp = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StoryGhostControl_Prince.isDisappear = true;
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
    void PlankActive()
    {
        //if (//救王子的時候)
        //    plank.SetActive(true);
    }

    //IEnumerator GoToMenu()
    //{
    //    yield return new WaitForSeconds(5f);
    //    TransitionUIControl.isTransitionUIAnim_In = true;
    //    yield return new WaitForSeconds(1f);
    //    SceneManager.LoadScene(0);
    //}
}
