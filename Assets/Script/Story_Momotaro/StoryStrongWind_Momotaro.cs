using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Wind")]
    public GameObject windA;
    public GameObject windB;
    public float _windCooldown; // 大风间隔时间
    public float _windDuration; // 大风持续时间
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform respawnPointA;
    public Transform respawnPointB;
    public static int _respawnNum;
    public static bool isBlownAway = false;
    StoryPlayerControl playerControl;

    [Header("HintUI")]
    public GameObject hintUI;
    public Image top, bottom;
    public RectTransform topPos, bottomPos;
    bool isMove = false;
    float _animDuration = 0.7f;
    float _speed = 360f;

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
            yield return new WaitForSeconds(_windCooldown);
            StartCoroutine(OpenWindUI());
            isMove = true;

            yield return new WaitForSeconds(_animDuration);
            isWindActive = true;

            yield return new WaitForSeconds(_windDuration);
            StartCoroutine(CloseWindUI());
            isMove = false;
            isWindActive = false;
            _windCooldown = Random.Range(2, 4);
            print(_windCooldown);
        }
    }
    IEnumerator OpenWindUI()
    {
        hintUI.SetActive(true);
        hintUI.GetComponent<CanvasGroup>().alpha = 1;
        top.fillAmount = 0;

        float elapsedTime = 0f;

        while (elapsedTime < _animDuration)
        {
            elapsedTime += Time.deltaTime;
            top.fillAmount = Mathf.Clamp01(elapsedTime / _animDuration);
            yield return null;
        }

        top.fillAmount = 1;
    }
    IEnumerator CloseWindUI()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _animDuration)
        {
            elapsedTime += Time.deltaTime;
            hintUI.GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(1 - elapsedTime / _animDuration);
            yield return null;
        }

        hintUI.GetComponent<CanvasGroup>().alpha = 0;
        hintUI.SetActive(false);
    }

    void Update()
    {
        windA.SetActive(isWindActive);
        windB.SetActive(isWindActive);

        HintUIMove();
        BlownAway();
    }

    void HintUIMove()
    {
        if (isMove)
        {
            top.rectTransform.anchoredPosition += Vector2.left * _speed * Time.deltaTime;
            bottom.rectTransform.anchoredPosition += Vector2.right * _speed * Time.deltaTime;
        }
        else
        {
            top.transform.position = topPos.position;
            bottom.transform.position = bottomPos.position;
        }
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
        if (_respawnNum == 1)
        {
            player.transform.position = respawnPointA.position;
        }
        else
        {
            player.transform.position = respawnPointB.position;
        }
        yield return new WaitForSeconds(0.2f);
        playerControl.enabled = true;
    }

    void OnDisable()
    {
        StopCoroutine(WindCycle());
    }
}
