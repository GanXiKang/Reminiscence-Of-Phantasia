using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryThermometerControl_Girl : MonoBehaviour
{
    public static bool isThermometer = false;
    public static bool isDead = false;
    float _temperature = 36.5f;
    float _decline = 0.1f;
    float _rise = 0.15f;
    bool isUseMatches = false;

    [Header("UI")]
    public GameObject thermometerUI;
    public Image energyBar;
    public Text temperature;

    void Update()
    {
        Thermometer();
        Limit();

        if (Input.GetKeyDown(KeyCode.E))
        {
            isUseMatches = true;
            Invoke("Matches", 2f);
        }
    }

    void Thermometer()
    {
        thermometerUI.SetActive(isThermometer);
        energyBar.fillAmount = (_temperature - 35f) / 2;
        temperature.text = _temperature.ToString("F1");

        if (isThermometer)
        {
            if (!isUseMatches)
            {
                _temperature -= _decline * Time.deltaTime;
            }
            else
            {
                _temperature += _rise * Time.deltaTime;
            }
        }
    }
    void Matches()
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
