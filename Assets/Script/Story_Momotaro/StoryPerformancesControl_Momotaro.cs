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

    [Header("PerformancesUI")]
    public Text test; //úy‘á∞Ê
    public Image bar;
    float _score;

    //random
    public static int _danceNum = 0;
    int _randomDanceNum;
   
    //control
    float timeLimit = 2f; //2√ÎÉ»∞¥œ¬∞¥‚o
    float timer;
    bool isTiming = false;
    bool isPerformances = false;
    bool isSpace = false;

    void OnEnable()
    {
        StartCoroutine(StartPerformance());
    }

    IEnumerator StartPerformance()
    {
        _score = 0;
        BGM.Stop();
        BGM.clip = performancesBGM;
        BGM.Play();
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
                isSpace = false;
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

    void Update()
    {
        PerformancesTimeOut();
        BGMisSettingActive();
        KeyBroad();
        Score();
    }

    void PerformancesTimeOut()
    {
        if (SettingControl.isSettingActive) return;
        if (!isPerformances) return;
        if (!isTiming) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (_danceNum != 3)
            {
                _danceNum = 0;
                isTiming = false;
                test.text = "TimeOut!";
                _score -= 2;
                float randomTime = Random.Range(0.8f, 2f);
                Invoke("StartNewRound", randomTime);
            }
            else
            {
                if (isSpace)
                {
                    _danceNum = 0;
                    isTiming = false;
                    test.text = "Perfect!";
                    float randomTime = Random.Range(1.5f, 2f);
                    Invoke("StartNewRound", randomTime);
                }
                else
                {
                    _danceNum = 0;
                    isTiming = false;
                    test.text = "Miss!";
                    _score -= 2;
                    float randomTime = Random.Range(0.8f, 2f);
                    Invoke("StartNewRound", randomTime);
                }
            }
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
    void Score()
    {
        if (!isPerformances) return;

        bar.fillAmount = _score / 30;
    }

    public void Dance_Button(int num)
    {
        if (SettingControl.isSettingActive) return;
        if (!isPerformances) return;
        if (!isTiming) return; 

        if (num == _danceNum)
        {
            if (_danceNum != 3)
            {
                test.text = "Correct!";
                _score++;
                _danceNum = 0;
                isTiming = false;
                float randomTime = Random.Range(0.8f, 2f);
                Invoke("StartNewRound", randomTime);
            }
            else
            {
                isSpace = true;
                test.text = "Good!";
                _score += 0.5f;
            }
        }
        else
        {
            test.text = "Incorrect!";
            _score--;
            _danceNum = 0;
            isTiming = false;
            float randomTime = Random.Range(0.8f, 2f);
            Invoke("StartNewRound", randomTime);
        }
        print(_score);
    }

    void OnDisable()
    {
        isPerformances = false;
        _danceNum = 0;
    }
}
