using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Wind")]
    public GameObject wind;
    public float windCooldown = 3f; // �����ʱ��
    public float windDuration = 2f; // ������ʱ��
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform originalPoint;
    public static bool isBlownAway = false;
    StoryPlayerControl playerControl;

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
            isWindActive = true; // ��翪ʼ
            Debug.Log("��翪ʼ��");
            yield return new WaitForSeconds(windDuration);
            isWindActive = false; // ������
            Debug.Log("��������");
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
        player.transform.position = originalPoint.position;
        yield return new WaitForSeconds(0.2f);
        playerControl.enabled = true;

    }

    void OnDisable()
    {
        StopCoroutine(WindCycle());
    }
}
