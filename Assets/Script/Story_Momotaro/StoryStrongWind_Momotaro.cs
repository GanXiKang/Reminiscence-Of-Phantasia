using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Wind")]
    public GameObject[] wind;
    public float _windCooldown; // 大风间隔时间
    public float _windDuration; // 大风持续时间
    bool isWindActive = false;

    [Header("BlownAway")]
    public Transform[] respawnPoint;
    public static int _respawnNum;
    public static bool isBlownAway = false;
    StoryPlayerControl playerControl;
    Animator anim;

    [Header("HintUI")]
    public GameObject hintUI;
    public Image top, bottom;
    public RectTransform topPos, bottomPos;
    bool isMove = false;
    float _animDuration = 0.7f;
    float _speed = 360f;

    //value
    bool isFirstBlown = true;

    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<StoryPlayerControl>();
        anim = player.GetComponent<Animator>();
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
            _windCooldown = Random.Range(1, 4);
        }
    }
    IEnumerator OpenWindUI()
    {
        hintUI.SetActive(true);
        hintUI.GetComponent<CanvasGroup>().alpha = 1;
        top.fillAmount = 0;
        bottom.fillAmount = 0;

        float elapsedTime = 0f;

        while (elapsedTime < _animDuration)
        {
            elapsedTime += Time.deltaTime;
            top.fillAmount = Mathf.Clamp01(elapsedTime / _animDuration);
            bottom.fillAmount = Mathf.Clamp01(elapsedTime / _animDuration);
            yield return null;
        }

        top.fillAmount = 1;
        bottom.fillAmount = 1;
    }
    IEnumerator CloseWindUI()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            hintUI.GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(1 - elapsedTime / 0.5f);
            yield return null;
        }

        hintUI.GetComponent<CanvasGroup>().alpha = 0;
        hintUI.SetActive(false);
    }

    void Update()
    {
        WindActive();
        HintUIMove();
        BlownAway();
    }

    void WindActive()
    {
        for (int i = 0; i < wind.Length; i++)
        {
            wind[i].SetActive(isWindActive);
        }
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
        anim.enabled = false;
        yield return new WaitForSeconds(0.8f);
        player.transform.position = respawnPoint[_respawnNum].position;
        if (isFirstBlown && !StoryPlayerAnimator_Momotaro.isRaccoon)
        {
            isFirstBlown = false;
            StoryUIControl_Momotaro.isDialogue = true;
            StoryDialogueControl_Momotaro._textCount = 5;
        }
        yield return new WaitForSeconds(0.5f);
        playerControl.enabled = true;
        anim.enabled = true;
    }

    void OnDisable()
    {
        StopCoroutine(WindCycle());
        hintUI.SetActive(false);
        isMove = false;
    }
}
