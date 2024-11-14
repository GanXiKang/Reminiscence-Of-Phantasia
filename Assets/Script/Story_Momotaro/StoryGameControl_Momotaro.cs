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

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ExitActive();
    }

    void ExitActive()
    {
        forest.SetActive(isForestActive);
        mountain.SetActive(isMountainActive);
    }
}
