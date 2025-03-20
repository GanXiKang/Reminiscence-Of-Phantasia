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
    public static bool isPlotNpcActive = false;

    [Header("Scene")]
    public GameObject now;
    public GameObject past;
    public GameObject future;

    [Header("SuppliesGame")]
    public GameObject supplies_Now;
    public GameObject supplies_Past;
    public static bool isSuppliesGameEasy = false;
    public static bool isSuppliesGameHard = false;

    void Start()
    {
        PlotNpcActive();
    }

    void Update()
    {
        MouseCursor();

        if (isPlotNpcActive)
            PlotNpcActive();

        npc[2].SetActive(past.activeSelf);
        npc[7].SetActive(past.activeSelf);
        supplies_Now.SetActive(isSuppliesGameHard && !StoryUIControl_Prince.isSuppliesActive);
        supplies_Past.SetActive(isSuppliesGameEasy && !StoryUIControl_Prince.isSuppliesActive);

        //if (//救王子的時候)
        //    plank.SetActive(true);
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
    void PlotNpcActive()
    {
        for (int i = 3; i < npc.Length; i++)
        {
            npc[i].SetActive(isPlotNpcActive);
        }

        if (isPlotNpcActive)
            isPlotNpcActive = false;
    }
}
