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
    public Image top;
    float _animDuration = 0.8f;
    float _speed = 200f;
    float _resetPositionX = 500f;

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

            yield return new WaitForSeconds(_animDuration);
            isWindActive = true;

            yield return new WaitForSeconds(_windDuration);
            StartCoroutine(CloseWindUI());
            isWindActive = false;
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

            top.rectTransform.anchoredPosition += Vector2.left * _speed * Time.deltaTime;
            if (top.rectTransform.anchoredPosition.x < -_resetPositionX)
            {
                top.rectTransform.anchoredPosition = new Vector2(_resetPositionX, top.rectTransform.anchoredPosition.y);
            }

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
