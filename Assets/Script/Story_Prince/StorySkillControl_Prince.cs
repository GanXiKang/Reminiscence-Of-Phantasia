using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySkillControl_Prince : MonoBehaviour
{
    [Header("ClockUI")]
    public GameObject clockUI;
    public GameObject pointer;
    public Button time;
    public static bool isClockActice = false;
    bool isCheckZone = false;
    bool isRotating = false;
    bool isIncreasing = false;
    bool isReducing = false;
    bool isChange = false;
    float _currentTime = 0f;
    float _duration = 3f;
    float _maxRotationSpeed = 270f;

    [Header("EnergyUI")]
    public Image energyBar;
    public static int _zoneNum;
    float _energyValue = 0.7f;
    float _rotation = 0.005f;
    float _smallArea = 0.1f;
    float _largeArea = 0.15f;
    float _nowArea = 0.05f;
    bool isEnergyConsume = false;

    void Update()
    {
        ObjectActive();
        ClockRotating();
        CheckCurrentZone();
        Energy();
    }

    void ObjectActive()
    {
        clockUI.SetActive(isClockActice);

        time.interactable = ButtonTimeInteractable();
    }
    bool ButtonTimeInteractable()
    {
        return !isIncreasing &&
               !isReducing &&
               energyBar.fillAmount > 0;
    }

    void ClockRotating()
    {
        if (!isRotating) return;

        if (isIncreasing)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _duration)
            {
                _currentTime = _duration;
                isIncreasing = false;
            }
        }
        float _rotationSpeed = Mathf.Lerp(0f, _maxRotationSpeed, _currentTime / _duration);
        pointer.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        _energyValue -= _rotation * Time.deltaTime;
    }
    void CheckCurrentZone()
    {
        if (!isCheckZone) return;

        if (isReducing)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _duration)
            {
                _currentTime = _duration;
                isReducing = false;
            }
        }
        float _rotationSpeed = Mathf.Lerp(_maxRotationSpeed, 0f, _currentTime / _duration);
        pointer.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);

        if (_rotationSpeed <= 0)
        {
            float zRotation = pointer.transform.eulerAngles.z % 360;
            int zone = Mathf.FloorToInt(zRotation / 30f) + 1;
            isCheckZone = false;
            isEnergyConsume = true;
            _zoneNum = zone;
        }
    }
    void Energy()
    {
        energyBar.fillAmount = _energyValue;

        if (energyBar.fillAmount <= 0)
        {
            if (isRotating)
            {
                isRotating = false;
                CheckCurrentZone();
            }
        }

        if (!isEnergyConsume) return;

        switch (_zoneNum)
        {
            case 1:
            case 2:
            case 12:
                if (!StoryLoadingScene_Prince.isNowScene)
                {
                    _energyValue -= _largeArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = true;
                    StoryLoadingScene_Prince.isPastScene = false;
                    StoryLoadingScene_Prince.isFutureScene = false;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 3:
                if (!StoryLoadingScene_Prince.isNowScene)
                {
                    _energyValue -= _smallArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = true;
                    StoryLoadingScene_Prince.isPastScene = false;
                    StoryLoadingScene_Prince.isFutureScene = false;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 4:
            case 5:
            case 6:
                if (!StoryLoadingScene_Prince.isFutureScene)
                {
                    _energyValue -= _largeArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = false;
                    StoryLoadingScene_Prince.isPastScene = false;
                    StoryLoadingScene_Prince.isFutureScene = true;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 7:
                if (!StoryLoadingScene_Prince.isFutureScene)
                {
                    _energyValue -= _smallArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = false;
                    StoryLoadingScene_Prince.isPastScene = false;
                    StoryLoadingScene_Prince.isFutureScene = true;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 8:
            case 9:
            case 10:
                if (!StoryLoadingScene_Prince.isPastScene)
                {
                    _energyValue -= _largeArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = false;
                    StoryLoadingScene_Prince.isPastScene = true;
                    StoryLoadingScene_Prince.isFutureScene = false;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 11:
                if (!StoryLoadingScene_Prince.isPastScene)
                {
                    _energyValue -= _smallArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = false;
                    StoryLoadingScene_Prince.isPastScene = true;
                    StoryLoadingScene_Prince.isFutureScene = false;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;
        }
        Invoke("FalseByisCheckConsume", 1f);
    }
    void FalseByisCheckConsume()
    {
        isEnergyConsume = false;
        if (isChange)
        {
            StoryLoadingScene_Prince.isOpen = true;
            isChange = false;
        }
    }

    public void Button_Time()
    {
        if (isIncreasing || isReducing) return;

        isRotating = !isRotating;
        _currentTime = 0f;
        if (isRotating)
        {
            isIncreasing = true;
        }
        else
        {
            isCheckZone = true;
            isReducing = true;
        }
    }
    public void Button_ClockActive()
    {
       isClockActice = !isClockActice;
    }
}
