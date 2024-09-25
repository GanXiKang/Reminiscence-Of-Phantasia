using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("TeachWall")]
    public GameObject teachWall;
    public static bool isWallActive = false; //Test

    [Header("GarbageCan")]
    public GameObject trashcanLid;
    public static bool isTrashcanLidActice = true;

    [Header("Resurrection")]
    public Transform streetPoint;
    public Transform forestPoint;
    public static bool isInStreet = true;
    public static bool isResurrection = false;
    public static bool isRenewTemperature = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        teachWall.SetActive(isWallActive);
        trashcanLid.SetActive(isTrashcanLidActice);

        PlayerResurrection();
    }

    void PlayerResurrection()
    {
        if (!isResurrection) return;

        isResurrection= false;
        Invoke("ResurrectionState", 3f);
    }
    void ResurrectionState()
    {
        StoryThermometerControl_Girl.isDead = false;
        StoryThermometerControl_Girl._matchQuantity += 10;
        isRenewTemperature = true;
        if (isInStreet)
        {
            player.transform.position = streetPoint.position;
        }
        else
        {
            player.transform.position = forestPoint.position;
        }
    }
}
