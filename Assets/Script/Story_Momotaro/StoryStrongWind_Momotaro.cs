using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    [Header("Wind")]
    public GameObject wind;
    public float windDuration = 3f; // 大风持续时间
    public float windCooldown = 5f; // 大风间隔时间
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform originalPoint;
    public static bool isBlownAway = false;

    void Update()
    {
       
    }
}
