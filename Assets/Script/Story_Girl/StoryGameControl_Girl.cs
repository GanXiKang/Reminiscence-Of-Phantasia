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
    //public static bool isResurrection;

    void Start()
    {
        player = GameObject.Find("Player");
        //isResurrection = false;
    }

    void Update()
    {
        teachWall.SetActive(isWallActive);
        trashcanLid.SetActive(isTrashcanLidActice);

        PlayerResurrection();
    }

    void PlayerResurrection()
    {
        if (!StoryThermometerControl_Girl.isDead) return;

        StoryThermometerControl_Girl.isDead = false;
        BlackScreenControl.isOpenBlackScreen = true;
        Invoke("ResurrectionState", 3f);
    }
    void ResurrectionState()
    {
        if (isInStreet)
        {
            player.transform.position = streetPoint.position;
        }
        else
        {
            player.transform.position = forestPoint.position;
        }
        StoryThermometerControl_Girl._matchQuantity += 10;
    }
}
