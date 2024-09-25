using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("TeachWall")]
    public GameObject teachWall;
    public static bool isWallActive = true; 

    [Header("GarbageCan")]
    public GameObject trashcanLid;
    public static bool isTrashcanLidActice = true;

    [Header("Resurrection")]
    public Transform streetPoint;
    public Transform forestPoint;
    public static bool isInStreet = true;
    public static bool isResurrection = false;
    public static bool isRenewTemperature = false;
    StoryPlayerControl playerControl;

    //Animation
    public static float _direction_Irls;

    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<StoryPlayerControl>();
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

        isResurrection = false;
        StartCoroutine(ResurrectionState());
    }
    IEnumerator ResurrectionState()
    {
        playerControl.enabled = false;
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
        StoryThermometerControl_Girl.isDead = false;
        StoryThermometerControl_Girl._matchQuantity += 10;
        yield return new WaitForSeconds(0.5f);
        playerControl.enabled = true;
    }
}
