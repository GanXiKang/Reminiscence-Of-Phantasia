using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryThermometerControl_Girl : MonoBehaviour
{
    GameObject player;

    //Controller
    public static bool isThermometer = false;
    public static bool isStepOnSnow = false;
    public static bool isDead = false;
    float _temperature = 36.5f;
    float _decline = 0.1f;
    float _snow = 0.2f;
    float _rise = 0.15f;
    bool isUseMatches = false;

    [Header("UI")]
    public GameObject thermometerUI;
    public Image energyBar;
    public Text temperature;

    [Header("SkillUI")]
    public GameObject skill;
    public static bool isSkillActive;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        skill.SetActive(isSkillActive);

        Thermometer();
        UseMatch();
        Limit();   
    }

    void Thermometer()
    {
        thermometerUI.SetActive(isThermometer);
        Vector3 worldPos = player.transform.position + new Vector3(-8f, -3f, 0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        thermometerUI.transform.position = screenPos;

        if (isThermometer)
        {
            if (!isUseMatches)
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
                if (!isStepOnSnow)
                {
                    _temperature += _rise * Time.deltaTime;
                }
                else
                {
                    _temperature -= _rise / 2 * Time.deltaTime;
                }
            }
            energyBar.fillAmount = (_temperature - 35f) / 2;
            temperature.text = _temperature.ToString("F1");
        }
    }
    void UseMatch()
    {
        if (!isSkillActive) return;

        if (Input.GetKeyDown(KeyCode.E) && !isDead)
        {
            isUseMatches = true;
            Invoke("FalseUseMatches", 2f);
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
            isDead = true;
        }
    }
}
