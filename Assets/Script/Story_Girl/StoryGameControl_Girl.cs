using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("TeachWall")]
    public GameObject teachWall;
    public GameObject mistLeft;
    public GameObject mistRight;
    public static bool isWallActive = true;
    bool isMistEffects = true;

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

    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<StoryPlayerControl>();
    }

    void Update()
    {
        trashcanLid.SetActive(isTrashcanLidActice);

        TeachWall();
        PlayerResurrection();
    }

    void TeachWall()
    {
        teachWall.SetActive(isWallActive);

        if (isWallActive) return;
        if (!isMistEffects) return;

        Invoke("Mist", 5f);
        mistLeft.transform.Translate(0f, 0.05f, 0f);
        mistRight.transform.Translate(0f, -0.05f, 0f);
    }
    void Mist()
    {
        isMistEffects = false;
        Destroy(mistLeft);
        Destroy(mistRight);
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
        StoryThermometerControl_Girl._matchQuantity = 10;
        yield return new WaitForSeconds(0.5f);
        playerControl.enabled = true;
    }
}
