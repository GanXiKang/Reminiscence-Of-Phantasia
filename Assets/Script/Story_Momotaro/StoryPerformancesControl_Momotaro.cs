using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPerformancesControl_Momotaro : MonoBehaviour
{
    [Header("Performances")]
    public Text test; //�yԇ��
    private int _randomDanceNum;
    private float timeLimit = 1f; //1��Ȱ��°��o
    private float timer;
    private bool isPerformances = false;

    void OnEnable()
    {
        StartNewRound();
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
            StartNewRound();
        }
    }

    public void Dance_Button(int num)
    {
        if (!isPerformances) return;

        if (num == _randomDanceNum)
        {
            test.text = "Correct!";
            StartNewRound();
        }
        else
        {
            test.text = "Incorrect!";
        }
    }
}
