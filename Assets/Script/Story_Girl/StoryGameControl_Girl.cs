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
    public static bool isChangeTransform = false;
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
        isChangeTransform = true;
        StartCoroutine(ResurrectionState());
    }
    IEnumerator ResurrectionState()
    {
        isChangeTransform = true;
        yield return new WaitForSeconds(0.7f);
        if (isInStreet)
        {
            player.transform.position = streetPoint.position;
        }
        else
        {
            player.transform.position = forestPoint.position;
        }
        isRenewTemperature = true;
        StoryThermometerControl_Girl._matchQuantity += 10;
        StoryThermometerControl_Girl.isDead = false;
        yield return new WaitForSeconds(0.3f);
        isChangeTransform = false;
    }
}
