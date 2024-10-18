using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPerformancesControl_Momotaro : MonoBehaviour
{
    [Header("Performances")]
    public Text test; //úy‘á∞Ê
    private int _randomDanceNum;
    private float timeLimit = 2f; //2√ÎÉ»∞¥œ¬∞¥‚o
    private float timer;
    private bool isPerformances = false;

    void OnEnable()
    {
        Invoke("StartNewRound", 1);
    }

    void Update()
    {
        PerformancesTimeOut();
    }

    void StartNewRound()
    {
        _randomDanceNum = Random.Range(1, 6);
        test.text = _randomDanceNum.ToString();
        timer = timeLimit;
        isPerformances = true;
    }
    void PerformancesTimeOut()
    {
        if (!isPerformances) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            test.text = "TimeOut!";
            float randomTime = Random.Range(0.3f, 1.2f);
            Invoke("StartNewRound", randomTime);
        }
    }

    public void Dance_Button(int num)
    {
        if (!isPerformances) return;

        if (num == _randomDanceNum)
        {
            test.text = "Correct!";
            float randomTime = Random.Range(0.3f, 1.2f);
            Invoke("StartNewRound", randomTime);
        }
        else
        {
            test.text = "Incorrect!";
        }
    }

    void OnDisable()
    {
        isPerformances = false;
    }
}
