using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySkillControl_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip nowBGM, pastBGM, futureBGM;

    [Header("Scene")]
    public GameObject now;
    public GameObject past;
    public GameObject future;
    public static bool isNowScene = true;
    public static bool isPastScene = false;
    public static bool isFutureScene = false;

    [Header("ClockUI")]
    public GameObject clockUI;
    public GameObject pointer;
    public Button time;
    bool isClockActice = false;
    bool isRotating = false;
    bool isIncreasing = false;
    float _rotationSpeed = 90f;
    float _currentTime = 0f;

    [Header("EnergyUI")]
    public Image energyBar;
    float _energyValue = 0.7f;
    float _rotation = 0.01f;
    float _smallArea = 0.1f;
    float _largeArea = 0.15f;
    float _nowArea = 0.05f;
    bool isCheckConsume = false;
    int _checkZoneNum;

    void Update()
    {
        clockUI.SetActive(isClockActice);

        Scene();
        ClockRotating();
        Energy();
    }

    void Scene()
    {
        now.SetActive(isNowScene);
        past.SetActive(isPastScene);
        future.SetActive(isFutureScene);
    }
    void ClockRotating()
    {
        if (!isClockActice) return;

        if (isRotating)
        {
            if (isIncreasing)
            {
                _currentTime += Time.deltaTime; // 增加时间
                if (_currentTime >= 2f)
                {
                    _currentTime = 2f; // 确保不超过最大时间
                    isIncreasing = false; // 开始减少速度
                }
            }
            float _rotationSpeed = Mathf.Lerp(30f, 90f, _currentTime/2f);

            pointer.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
            print(_rotationSpeed);
            _energyValue -= _rotation * Time.deltaTime;
        }
    }
    void CheckCurrentZone()
    {
        float zRotation = pointer.transform.eulerAngles.z % 360;
        int zone = Mathf.FloorToInt(zRotation / 30f) + 1;
        _checkZoneNum = zone;

        switch (zone)
        {
            case 1:
            case 2:
            case 3:
            case 12:
                if (!isNowScene)
                {
                    isNowScene = true;
                    isPastScene = false;
                    isFutureScene = false;
                    isClockActice = false;
                    print("現實");
                }
                break;

            case 4:
            case 5:
            case 6:
            case 7:
                if (!isFutureScene)
                {
                    isNowScene = false;
                    isPastScene = false;
                    isFutureScene = true;
                    isClockActice = false;
                    print("未來");
                }
                break;

            case 8:
            case 9:
            case 10:
            case 11:
                if (!isPastScene)
                {
                    isNowScene = false;
                    isPastScene = true;
                    isFutureScene = false;
                    isClockActice = false;
                    print("過去");
                }
                break;
        }
    }
    void Energy()
    {
        energyBar.fillAmount = _energyValue;

        if (energyBar.fillAmount <= 0)
        {
            time.interactable = false;
            if (isRotating)
            {
                isRotating = false;
                CheckCurrentZone();
            }
        }
        else
        {
            time.interactable = true;
        }

        if (!isCheckConsume) return;

        switch (_checkZoneNum)
        {
            case 1:
            case 2:
            case 12:
                if (!isNowScene)
                {
                    _energyValue -= _largeArea * Time.deltaTime;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 3:
                if (!isNowScene)
                {
                    _energyValue -= _smallArea * Time.deltaTime;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 4:
            case 5:
            case 6:
                if (!isFutureScene)
                {
                    _energyValue -= _largeArea * Time.deltaTime;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 7:
                if (!isFutureScene)
                {
                    _energyValue -= _smallArea * Time.deltaTime;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 8:
            case 9:
            case 10:
                if (!isPastScene)
                {
                    _energyValue -= _largeArea * Time.deltaTime;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 11:
                if (!isPastScene)
                {
                    _energyValue -= _smallArea * Time.deltaTime;
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
        isCheckConsume = false;
    }

    public void Button_Time()
    {
        isRotating = !isRotating;

        if (!isRotating)
        {
            CheckCurrentZone();
            isCheckConsume = true;
        }
    }
    public void Button_ClockActive()
    {
       isClockActice = !isClockActice;
    }
}
