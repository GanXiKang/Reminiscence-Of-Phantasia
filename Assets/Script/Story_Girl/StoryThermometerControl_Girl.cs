using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryThermometerControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip use;

    [Header("Value")]
    public static bool isThermometer = false;
    public static bool isStepOnSnow = false;
    public static bool isFireBeside = false;
    public static bool isDead = false;
    public float _temperature = 36.5f;
    public float _decline = 0.1f;
    public float _snow = 0.2f;
    public float _rise = 0.15f;
    bool isUseMatches = false;
    bool isOnce = true;

    [Header("UI")]
    public GameObject thermometerUI;
    public Image energyBar;
    public Text temperature;

    [Header("SkillUI")]
    public GameObject skill;
    public Text quantity;
    public static bool isSkillActive;
    public static int _matchQuantity = 0;
    int _countUse = 0;
    bool isteach = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {    
        Thermometer();
        Teach();
        SkillUI();
        UseMatch();
        Limit();   
    }

    void Thermometer()
    {
        if (!isThermometer) return;

        thermometerUI.SetActive(!StoryUIControl_Girl.isDialogue);
        Vector3 worldPos = player.transform.position + new Vector3(-8f, -3f, 0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        thermometerUI.transform.position = screenPos;

        if (!StoryUIControl_Girl.isDialogue)
        {
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
    void UseMatch()
    {
        if (StoryUIControl_Girl.isDialogue) return;
        if (!isSkillActive) return;
        if (isUseMatches) return;
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_matchQuantity != 0)
            {
                isUseMatches = true;
                BGM.PlayOneShot(use);
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
    void FalseUseMatches()
    {
        isUseMatches = false;
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
            if (isOnce)
            {
                isOnce = false;
                isDead = true;
                StoryUIControl_Girl.isDialogue = true;
                StoryDialogueControl_Girl._textCount = 10;
            }
        }
    }
}
