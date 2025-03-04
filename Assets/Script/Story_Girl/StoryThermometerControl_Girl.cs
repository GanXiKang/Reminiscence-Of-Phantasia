using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryThermometerControl_Girl : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip use;

    [Header("Value")]
    public float _temperature = 36.5f;
    public static bool isThermometer;
    public static bool isStepOnSnow;
    public static bool isFireBeside;
    public static bool isDead = false;
    bool isUseMatches = false;
    float _decline = 0.08f;
    float _rise = 0.14f;
    float _snow = 0.2f;

    [Header("UI")]
    public GameObject thermometerUI;
    public Image energyBar;
    public Text temperature;

    [Header("SkillUI")]
    public GameObject skill;
    public Text quantity;
    public static bool isSkillActive;
    public static int _matchQuantity;
    int _countUse = 0;
    bool isteach = false;

    [Header("ColdUI")]
    public GameObject cold;
    public Image background;
    private Coroutine currentCoroutine;
    float _animDuration = 1f;
    bool isCold = false;

    void Update()
    {    
        Thermometer();
        Teach();
        SkillUI();
        ColdUI();
        UseMatch();
        RenewTemperature();
        Limit();   
    }

    void Thermometer()
    {
        if (!isThermometer) return;

        thermometerUI.SetActive(!StoryUIControl_Girl.isDialogue);

        if (SettingControl.isSettingActive) return;
        if (StoryExitControl_Girl.isExit) return;
        if (StoryUIControl_Girl.isStoryEnding) return;
        if (StoryLoadingScene_Girl.isLoading) return;
        if (StoryUIControl_Girl.isDialogue) return;
        if (isDead) return;

        if (!isUseMatches)
        {
            if (!isFireBeside)
            {
                if (!isStepOnSnow)
                {
                    _temperature -= _decline * Time.deltaTime;
                }
                else
                {
                    _temperature -= _snow * Time.deltaTime;
                }
            }
            else
            {
                _temperature += _rise * Time.deltaTime;
            }
        }
        else
        {
            if (!isStepOnSnow)
            {
                _temperature += _rise * Time.deltaTime;
            }
            else
            {
                _temperature += _rise / 2 * Time.deltaTime;
            }
        }
        energyBar.fillAmount = (_temperature - 35f) / 2;
        temperature.text = _temperature.ToString("F1");
    }
    void Teach()
    {
        if (_temperature < 36f && _countUse == 0 && !isteach)
        {
            isteach = true;
            StoryUIControl_Girl.isDialogue = true;
            StoryDialogueControl_Girl._textCount = 3;
        }
    }
    void SkillUI()
    {
        skill.SetActive(isSkillActive);
        quantity.text = _matchQuantity.ToString();
    }
    void ColdUI()
    {
        if (_temperature <= 35.6f)
        {
            if (isCold) return;

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(OpenColdUI());
        }
        else 
        {
            if (!isCold) return;

            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(CloseColdUI());
        }
    }
    void UseMatch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Match_Button();
        }
    }
    void FalseUseMatches()
    {
        isUseMatches = false;
        StoryPlayerAnimator_Girl.isMatch = false;
    }
    void RenewTemperature()
    {
        if (!StoryGameControl_Girl.isRenewTemperature) return;

        StoryGameControl_Girl.isRenewTemperature = false;
        _temperature = 36f;
    }
    void Limit()
    {
        if (_temperature >= 37.0f)
        {
            _temperature = 37.0f;
        }

        if (_temperature <= 35.0f)
        {
            _temperature = 35.0f;
            if (!isDead)
            {
                isDead = true;
                StoryPlayerControl.isSad = true;
                StoryUIControl_Girl.isDialogue = true;
                StoryDialogueControl_Girl._textCount = 10;
            }
        }
    }

    IEnumerator OpenColdUI()
    {
        isCold = true;
        cold.SetActive(true);
        cold.GetComponent<CanvasGroup>().alpha = 1;
        background.fillAmount = 0;

        float elapsedTime = 0f;

        while (elapsedTime < _animDuration)
        {
            elapsedTime += Time.deltaTime;
            background.fillAmount = Mathf.Clamp01(elapsedTime / _animDuration);
            yield return null;
        }

        background.fillAmount = 1;
    }
    IEnumerator CloseColdUI()
    {
        isCold = false;

        float elapsedTime = 0f;

        while (elapsedTime < _animDuration)
        {
            elapsedTime += Time.deltaTime;
            cold.GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(1 - elapsedTime / _animDuration);
            yield return null;
        }

        cold.GetComponent<CanvasGroup>().alpha = 0;
        cold.SetActive(false);
    }

    public void Match_Button()
    {
        if (StoryUIControl_Girl.isDialogue) return;
        if (StoryUIControl_Girl.isStoryEnding) return;
        if (!isSkillActive) return;
        if (isUseMatches) return;
        if (isDead) return;

        if (_matchQuantity != 0)
        {
            isUseMatches = true;
            BGM.PlayOneShot(use);
            StoryPlayerAnimator_Girl.isMatch = true;
            _matchQuantity--;
            _countUse++;
            if (_countUse == 1)
            {
                StoryUIControl_Girl.isDialogue = true;
                StoryDialogueControl_Girl._textCount = 4;
            }
            Invoke("FalseUseMatches", 2f);
        }
        else
        {
            StoryUIControl_Girl.isDialogue = true;
            StoryDialogueControl_Girl._textCount = 5;
        }
    }
}
