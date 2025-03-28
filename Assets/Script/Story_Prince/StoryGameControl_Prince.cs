using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryGameControl_Prince : MonoBehaviour
{
    [Header("Texture")]
    public Texture2D[] mouse;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    [Header("Plank")]
    public GameObject plank;

    [Header("Npc")]
    public GameObject[] npc;
    public GameObject[] npcBC;
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

    [Header("WoodFence")]
    public GameObject woodFence;
    public GameObject woodFence_Bad;

    [Header("PrinceState_Now")]
    public GameObject princeStatue;
    public GameObject brokenPrinceStatue;
    public GameObject notPrinceStatue;
    public GameObject smokEF;
    public static bool isBroken = false;

    [Header("SceneFuture")]
    public GameObject futureBad;
    public GameObject futureGood;
    public static bool isFutureGood = false;

    [Header("SceneSound")]
    public AudioSource[] fountain;
    public AudioSource[] river;

    void Start()
    {
        PlotNpcActive();
    }

    void Update()
    {
        MouseCursor();

        MainNpcActive();
        PlotObjectActive();
        PrinceState_Now();
        SceneFuture();
        SceneSound();

        if (isPlotNpcActive)
            PlotNpcActive();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StoryBagControl.isGet = true;
            StoryBagControl.isItemNumber[12] = true;
            StoryBagControl.isItemNumber[13] = true;

            StoryInteractableControl_Prince._helpChildQian = 8;
            StoryInteractableControl_Prince.isPrinceNoDie = true;

            StoryNpcAnimator_Prince.isLeaveHelp = true;
            StoryNpcAnimator_Prince.isFindFood = true;
        }
    }

    void MouseCursor()
    {
        if (StoryBagControl.isItemFollow)
        {
            Cursor.SetCursor(mouse[2], hotSpot, CursorMode.Auto);
        }
        else
        {
            if (isClick)
                Cursor.SetCursor(mouse[1], hotSpot, CursorMode.Auto);
            else
                Cursor.SetCursor(mouse[0], hotSpot, CursorMode.Auto);
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
    
    void MainNpcActive()
    {
        if (StoryInteractableControl_Prince.isPrinceInNow)
        {
            npcBC[0].SetActive(now.activeSelf);
            npc[2].GetComponent<SpriteRenderer>().enabled = now.activeSelf;
        }
        else
        {
            npcBC[0].SetActive(past.activeSelf);
            npc[2].GetComponent<SpriteRenderer>().enabled = past.activeSelf;
        }

        npcBC[1].SetActive(now.activeSelf);
        npc[3].GetComponent<SpriteRenderer>().enabled = now.activeSelf;
        npcBC[2].SetActive(past.activeSelf);
        npc[4].GetComponent<SpriteRenderer>().enabled = past.activeSelf;
        npcBC[3].SetActive(past.activeSelf);
        npc[6].GetComponent<SpriteRenderer>().enabled = past.activeSelf;
        npcBC[5].SetActive(now.activeSelf);
        npc[9].GetComponent<SpriteRenderer>().enabled = now.activeSelf;

        npc[7].SetActive(StoryInteractableControl_Prince.isNeedSauce);
        npc[8].SetActive(StoryInteractableControl_Prince.isNeedSauce);
    }
    void PlotObjectActive()
    {
        supplies_Now.SetActive(isSuppliesGameHard && !StoryUIControl_Prince.isSuppliesActive);
        supplies_Past.SetActive(isSuppliesGameEasy && !StoryUIControl_Prince.isSuppliesActive);

        switch (StoryInteractableControl_Prince._helpChildQian)
        {
            case 2:
                woodFence.SetActive(false);
                woodFence_Bad.SetActive(true);
                break;

            case 4:
                plank.SetActive(true);
                break;

            case 5:
                if(!StoryInteractableControl_Prince.isGivePlank)
                    plank.SetActive(false);
                break;
        }
    }
    void PlotNpcActive()
    {
        for (int i = 2; i < npc.Length; i++)
        {
            npc[i].SetActive(isPlotNpcActive);
        }

        if (isPlotNpcActive)
            isPlotNpcActive = false;
    }
    void PrinceState_Now()
    {
        if (StoryInteractableControl_Prince.isPrinceNoDie)
        {
            princeStatue.SetActive(false);
            notPrinceStatue.SetActive(true);
        }
        else
        {
            if (isBroken)
            {
                smokEF.SetActive(true);
                Invoke("Broken", 0.5f);
            }
            else
            {
                smokEF.SetActive(false);
                princeStatue.SetActive(true);
                brokenPrinceStatue.SetActive(false);
            }
        }
    }
    void Broken()
    {
        princeStatue.SetActive(false);
        brokenPrinceStatue.SetActive(true);
    }
    void SceneFuture()
    {
        futureBad.SetActive(!isFutureGood);
        futureGood.SetActive(isFutureGood);
    }
    void SceneSound()
    {
        fountain[0].volume = SettingControl.volumeBGM;
        fountain[1].volume = SettingControl.volumeBGM;
        fountain[2].volume = SettingControl.volumeBGM;
        river[0].volume = SettingControl.volumeBGM;
        river[1].volume = SettingControl.volumeBGM;
        river[2].volume = SettingControl.volumeBGM;
    }
}
