using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    [Header("TeachWall")]
    public GameObject teachWall;
    public static bool isWallActive = false; //Test

    [Header("GarbageCan")]
    public GameObject trashcanLid;
    public static bool isTrashcanLidActice = true;

    [Header("Resurrection")]
    public Transform streetPoint;
    public Transform forestPoint;

    void Update()
    {
        teachWall.SetActive(isWallActive);
        trashcanLid.SetActive(isTrashcanLidActice);


    }
}
