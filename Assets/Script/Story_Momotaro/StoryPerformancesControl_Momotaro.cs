using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPerformancesControl_Momotaro : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip performancesBGM;
    bool isStopBGM = false;

    [Header("Performances")]
    public Text test; //�yԇ��

    int _randomDanceNum;
    float timeLimit = 2f; //2��Ȱ��°��o
    float timer;
    bool isTiming = false;
    bool isPerformances = false;

    void OnEnable()
    {
        Invoke("StartNewRound", 1);
        BGM.Stop();
        BGM.clip = performancesBGM;
        BGM.Play();
    }

    void Update()
    {
        PerformancesTimeOut();
        BGMisSettingActive();
    }

    void StartNewRound()
    {
        _randomDanceNum = Random.Range(1, 6);
        test.text = _randomDanceNum.ToString();
        timer = timeLimit;
        isTiming = true;
        isPerformances = true;
    }
    void PerformancesTimeOut()
    {
        if (SettingControl.isSettingActive) return;
        if (!isPerformances) return;
        if (!isTiming) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isTiming = false;
            test.text = "TimeOut!";
            float randomTime = Random.Range(0.3f, 1.2f);
            Invoke("StartNewRound", randomTime);
        }
    }
    void BGMisSettingActive()
    {
        if (SettingControl.isSettingActive)
        {
            BGM.Stop();
            isStopBGM = true;
        }
        else
        {
            if (isStopBGM)
            {
                BGM.Play();
                isStopBGM = false;
            }
        }
    }

    public void Dance_Button(int num)
    {
        if (SettingControl.isSettingActive) return;
        if (!isPerformances) return;

        if (num == _randomDanceNum)
        {
            test.text = "Correct!";
        }
        else
        {
            test.text = "Incorrect!";
        }
        isTiming = false;
        float randomTime = Random.Range(0.3f, 1.2f);
        Invoke("StartNewRound", randomTime);
    }

    void OnDisable()
    {
        isPerformances = false;
    }
}
