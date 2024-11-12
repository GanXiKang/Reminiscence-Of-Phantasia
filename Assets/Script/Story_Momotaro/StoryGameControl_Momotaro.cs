using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Exit")]
    public GameObject riverSide;
    public GameObject mountain;
    public static bool isRiverSideActive = false;
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
        riverSide.SetActive(isRiverSideActive);
        mountain.SetActive(isMountainActive);
    }
}
