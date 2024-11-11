using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Wind")]
    public GameObject wind;
    public float windCooldown; // 大风间隔时间
    public float windDuration; // 大风持续时间
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform respawnPointA, respawnPointB;
    public static bool isBlownAway = false;
    StoryPlayerControl playerControl;

    [Header("HintUI")]
    public GameObject hintUI;

    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<StoryPlayerControl>();
    }

    void OnEnable()
    {
        StartCoroutine(WindCycle());
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

    void Update()
    {
        wind.SetActive(isWindActive);

        BlownAway();
    }

    void BlownAway()
    {
        if (!isBlownAway) return;

        isBlownAway = false;
        StartCoroutine(BlownAwayRenewPos());
    }

    IEnumerator BlownAwayRenewPos()
    {
        playerControl.enabled = false;
        yield return new WaitForSeconds(0.8f);
        player.transform.position = respawnPointA.position;
        yield return new WaitForSeconds(0.2f);
        playerControl.enabled = true;
    }

    void OnDisable()
    {
        StopCoroutine(WindCycle());
    }

    //IEnumerator OpenColdUI()
    //{
    //    isCold = true;
    //    cold.SetActive(true);
    //    cold.GetComponent<CanvasGroup>().alpha = 1;
    //    background.fillAmount = 0;

    //    float elapsedTime = 0f;

    //    while (elapsedTime < _animDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        background.fillAmount = Mathf.Clamp01(elapsedTime / _animDuration);
    //        yield return null;
    //    }

    //    background.fillAmount = 1;
    //}
    //IEnumerator CloseColdUI()
    //{
    //    isCold = false;

    //    float elapsedTime = 0f;

    //    while (elapsedTime < _animDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        cold.GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(1 - elapsedTime / _animDuration);
    //        yield return null;
    //    }

    //    cold.GetComponent<CanvasGroup>().alpha = 0;
    //    cold.SetActive(false);
    //}
}
