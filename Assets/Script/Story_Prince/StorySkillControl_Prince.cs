using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool isClockActice = false;
    bool isRotating = false;
    float _rotationSpeed = 90f;

    [Header("EnergyUI")]
    public GameObject energyBar;
    public float _energyValue = 0.7f;
    public float _rotation = 0.05f;
    public float _smallArea = 0.1f;
    public float _largeArea = 0.15f;
    bool isCheckConsume = false;

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
            pointer.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }
    void CheckCurrentZone()
    {
        float zRotation = pointer.transform.eulerAngles.z % 360;
        int zone = Mathf.FloorToInt(zRotation / 30f) + 1;

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
                    print("¨Fåç");
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
                    print("Œ¥ÅÌ");
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
                    print("ﬂ^»•");
                }
                break;
        }
    }
    void Energy()
    {
        if (!isClockActice) return;

        if (isRotating)
        {
            _energyValue -= _rotation * Time.deltaTime;
        }
        if (isCheckConsume)
        {
            
        }
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
