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
    public Text test; //úy‘á∞Ê

    public static int _danceNum = 0;

    int _randomDanceNum;
    float timeLimit = 2f; //2√ÎÉ»∞¥œ¬∞¥‚o
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

    IEnumerator StartPerformance()
    {
        test.text = "3";
        yield return new WaitForSeconds(1f);
        test.text = "2";
        yield return new WaitForSeconds(1f);
        test.text = "1";
        yield return new WaitForSeconds(1f);
        test.text = "Ready";
        yield return new WaitForSeconds(1f);
        test.text = "Go!!!";
        yield return new WaitForSeconds(0.5f);
        StartNewRound();
    }

    void Update()
    {
        PerformancesTimeOut();
        BGMisSettingActive();
        KeyBroad();
    }

    void StartNewRound()
    {
        RandomDance();

        timer = timeLimit;
        isTiming = true;
        isPerformances = true;
    }
    void RandomDance()
    {
        _randomDanceNum = Random.Range(1, 9);

        switch (_randomDanceNum)
        {
            case 1:
            case 6:
                _danceNum = 1;
                test.text = "W";
                break;

            case 2:
            case 7:
                _danceNum = 2;
                test.text = "S";
                break;

            case 3:
                _danceNum = 3;
                test.text = "Space";
                break;

            case 4:
            case 8:
                _danceNum = 4;
                test.text = "A";
                break;

            case 5:
            case 9:
                _danceNum = 5;
                test.text = "D";
                break;
        }
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
    void KeyBroad()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Dance_Button(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Dance_Button(2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dance_Button(3);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Dance_Button(4);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dance_Button(5);
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
