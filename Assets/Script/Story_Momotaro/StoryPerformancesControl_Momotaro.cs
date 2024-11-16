using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPerformancesControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip performancesBGM, plazaBGM;
    bool isStopBGM = false;

    [Header("Camera")]
    public GameObject performancesCam;

    [Header("PerformancesUI")]
    public GameObject allUI;
    public Text test; //úy‘á∞Ê
    public Image scoreBar;
    public Image timeBar;
    public GameObject resultUI;
    public Image gameResult;
    public Sprite success, fail;
    float _score;
    float _countdownTime = 90f;
    float _remainingTime;
    bool isGameTiming = false;

    //random
    public static int _danceNum = 0;
    int _randomDanceNum;
   
    //control
    float timeLimit = 2f; //2√ÎÉ»∞¥œ¬∞¥‚o
    float timer;
    bool isTiming = false;
    bool isGamePerformances = false;
    bool isSpace = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        StartCoroutine(StartPerformance());
    }

    IEnumerator StartPerformance()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        allUI.SetActive(true);
        performancesCam.SetActive(true);
        player.transform.rotation = Quaternion.identity;
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
        isGameTiming = true;
        _remainingTime = _countdownTime;
        timeBar.fillAmount = 1f;
    }

    void StartNewRound()
    {
        RandomDance();

        timer = timeLimit;
        isTiming = true;
        isGamePerformances = true;
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
        KeyBroad();
        PerformancesTimeOut();
        BGMisSettingActive();
        Score();
        GameTime();
    }

    public void Dance_Button(int num)
    {
        if (SettingControl.isSettingActive) return;
        if (!isGamePerformances) return;
        if (!isGameTiming) return;
        if (!isTiming) return;

        if (num == _danceNum)
        {
            if (_danceNum != 3)
            {
                test.text = "Correct!";
                _score += 3;
                _danceNum = 0;
                isTiming = false;
                float randomTime = Random.Range(0.8f, 2f);
                Invoke("StartNewRound", randomTime);
            }
            else
            {
                isSpace = true;
                test.text = "Good!";
                _score++;
            }
        }
        else
        {
            test.text = "Incorrect!";
            _score -= 2;
            _danceNum = 0;
            isTiming = false;
            float randomTime = Random.Range(0.8f, 2f);
            Invoke("StartNewRound", randomTime);
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
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.L) || Input.GetMouseButtonDown(1))
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
    void PerformancesTimeOut()
    {
        if (SettingControl.isSettingActive) return;
        if (!isGamePerformances) return;
        if (!isGameTiming) return;
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
            if (BGM.isPlaying)
            {
                BGM.Pause();
                isStopBGM = true;
            }
        }
        else
        {
            if (isStopBGM)
            {
                BGM.UnPause();
                isStopBGM = false;
            }
        }
    }
    void Score()
    {
        if (!isGamePerformances) return;

        scoreBar.fillAmount = _score / 100;

        if (_score < 0)
        {
            _score = 0;
        }
        else if (_score > 100)
        {
            _score = 100;
        }
    }
    void GameTime()
    {
        if (!isGameTiming) return;

        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            float fillAmount = _remainingTime / _countdownTime;
            timeBar.fillAmount = fillAmount;
        }
        else
        {
            GameResult();
            isGameTiming = false;
            _remainingTime = 0f;
            timeBar.fillAmount = 0f;
        }
    }
    void GameResult()
    {
        resultUI.SetActive(true);

        if (_score >= 80)
        {
            gameResult.sprite = success;
        }
        else
        {
            gameResult.sprite = fail;
        }

        StartCoroutine(AnimateGameResult());
    }

    IEnumerator AnimateGameResult()
    {
        CanvasGroup canvasGroup = gameResult.GetComponent<CanvasGroup>();
        RectTransform rect = gameResult.GetComponent<RectTransform>();

        Vector3 startScale = new Vector3(3f, 1f, 1f);
        Vector3 targetScale = new Vector3(8f, 5f, 1f);

        float _duration = 0.3f;
        float _timeElapsed = 0f;
        canvasGroup.alpha = 0;
        rect.localScale = startScale;

        while (_timeElapsed < _duration)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / _duration;
            rect.localScale = Vector3.Lerp(startScale, targetScale, t);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        canvasGroup.alpha = 1;
        rect.localScale = targetScale;

        yield return new WaitForSeconds(2f);
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        StoryUIControl_Momotaro.isPerformances = false;
        allUI.SetActive(false);
        performancesCam.SetActive(false);
        player.transform.eulerAngles = new Vector3(30, -45, 0);
        BGM.Stop();
        BGM.clip = plazaBGM;
        BGM.Play();
        resultUI.SetActive(false);
    }

    void OnDisable()
    {
        isGamePerformances = false;
        _danceNum = 0;
    }
}
