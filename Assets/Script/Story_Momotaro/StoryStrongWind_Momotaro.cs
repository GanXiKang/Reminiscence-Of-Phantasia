using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    [Header("Wind")]
    public GameObject wind;
    public float windCooldown = 3f; // 大风间隔时间
    public float windDuration = 2f; // 大风持续时间
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform originalPoint;
    public static bool isBlownAway = false;

    void OnEnable()
    {
        StartCoroutine(WindCycle());
    }

    void Update()
    {
        wind.SetActive(isWindActive);
    }

    IEnumerator WindCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(windCooldown);
            isWindActive = true; // 大风开始
            Debug.Log("大风开始！");
            yield return new WaitForSeconds(windDuration);
            isWindActive = false; // 大风结束
            Debug.Log("大风结束！");
        }
    }

    void OnDisable()
    {
        StopCoroutine(WindCycle());
    }
}
