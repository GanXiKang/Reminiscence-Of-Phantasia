using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Exit")]
    public GameObject forest;
    public GameObject mountain;
    public static bool isForestActive = false;
    public static bool isMountainActive = false;

    [Header("Statue")]
    public GameObject catLow;
    public GameObject catFinish;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ExitActive();
        StatueActive();
    }

    void ExitActive()
    {
        forest.SetActive(isForestActive);
        mountain.SetActive(isMountainActive);
    }
    void StatueActive()
    {
        catLow.SetActive(!StoryInteractableControl_Momotaro.isFinishWork);
        catFinish.SetActive(StoryInteractableControl_Momotaro.isFinishWork);
    }
}
