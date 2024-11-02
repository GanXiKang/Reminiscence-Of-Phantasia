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

    void Update()
    {
        clockUI.SetActive(isClockActice);

        Scene();
        ClockRotating();
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
                    print("ﬂ^»•");
                }
                break;
        }
    }

    public void Button_Time()
    {
        isRotating = !isRotating;

        if (!isRotating)
            CheckCurrentZone();
    }
    public void Button_ClockActive()
    {
       isClockActice = !isClockActice;
    }
}
