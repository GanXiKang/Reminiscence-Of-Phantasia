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
    public GameObject resultUI;
    public Image gameResult;
    public Sprite success, fail;

    [Header("HandUI")]
    public GameObject handA;
    public GameObject handB;
    public Transform point1, point2;
    float _speed = 2.0f;

    [Header("ScoreUI")]
    public Image scoreBar;
    public Image star;
    public Sprite goldStar, blueStar;
    float _score;
    float _rotSpeed = 720f;
    int _circle = 3;
    bool isGoldStar = false;

    [Header("TimeUI")]
    public Image timeBar;
    float _countdownTime = 80f;
    float _remainingTime;
    bool isGameTiming = false;

    [Header("WordUI")]
    public GameObject wordUI;
    public Image word;
    public Sprite miss, nice, perfect, wrong;
    public float _duration = 0.5f;

    //random
    public static int _danceNum = 0;
    int _randomDanceNum;
   
    //control
    float timeLimit = 2f; //2ÃëƒÈ°´ÏÂ°´âo
    float timer;
    bool isTiming = false;
    bool isGamePerformances = false;
    bool isExcited = false;

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
        StoryNpcAnimator_Momotaro._performancesNum = 2;
        player.transform.rotation = Quaternion.identity;
        _score = 0;
        scoreBar.fillAmount = 0;
        _remainingTime = 80f;
        timeBar.fillAmount = 1f;
        star.sprite = blueStar;
        isGoldStar = false;
        BGM.Stop();
        BGM.clip = performancesBGM;
        BGM.Play();
        yield return new WaitForSeconds(2f);
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
                break;

            case 2:
            case 7:
                _danceNum = 2;
                break;

            case 3:
                isExcited = false;
                _danceNum = 3;
                break;

            case 4:
            case 8:
                _danceNum = 4;
                break;

            case 5:
            case 9:
                _danceNum = 5;
                break;
        }
    }

    void Update()
    {
        KeyBroad();
        PerformancesTimeOut();
        BGMisSettingActive();
        Hand();
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
                _score += 3;
                _danceNum = 0;
                isTiming = false;
                word.sprite = nice;
                StartCoroutine(ShowWordUI());
                float randomTime = Random.Range(0.8f, 2f);
                Invoke("StartNewRound", randomTime);
            }
            else
            {
                _score++;
                isExcited = true;
            }
        }
        else
        {
            _score -= 2;
            _danceNum = 0;
            isTiming = false;
            word.sprite = wrong;
            StartCoroutine(ShowWordUI());
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
        if (Input.GetKeyDown(KeyCode.P))
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
                _score -= 2;
                _danceNum = 0;
                isTiming = false;
                word.sprite = miss;
                StartCoroutine(ShowWordUI());

                float randomTime = Random.Range(0.8f, 2f);
                Invoke("StartNewRound", randomTime);
            }
            else
            {
                if (isExcited)
                {
                    _danceNum = 0;
                    isTiming = false;
                    word.sprite = perfect;
                    StartCoroutine(ShowWordUI());
                    float randomTime = Random.Range(1.5f, 2f);
                    Invoke("StartNewRound", randomTime);
                }
                else
                {
                    _score -= 2;
                    _danceNum = 0;
                    isTiming = false;
                    word.sprite = miss;
                    StartCoroutine(ShowWordUI());
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
    void Hand()
    {
        float t = Mathf.PingPong(Time.time * _speed, 1.0f);

        handA.transform.position = Vector3.Lerp(point1.position, point2.position, t);
        handB.transform.position = Vector3.Lerp(point2.position, point1.position, t);
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
            if (!isGoldStar)
            {
                StartCoroutine(RotateStar());
            }
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
        }
    }
    void GameResult()
    {
        resultUI.SetActive(true);

        if (_score >= 80)
        {
            gameResult.sprite = success;
            StoryInteractableControl_Momotaro.isSuccessfulPerformance = true;
        }
        else
        {
            gameResult.sprite = fail;
        }

        StartCoroutine(AnimateGameResult());
    }

    IEnumerator ShowWordUI()
    {
        Vector3 startScale = new Vector3(0.5f, 0.5f, 1f);
        Vector3 endScale = new Vector3(1f, 1f, 1f);
        float elapsedTime = 0f;

        wordUI.SetActive(true);
        wordUI.transform.localScale = startScale;

        while (elapsedTime < _duration)
        {
            float t = elapsedTime / _duration;
            wordUI.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        wordUI.transform.localScale = endScale;
        wordUI.SetActive(false);
    }
    IEnumerator RotateStar()
    {
        isGoldStar = true;

        float totalRotationTime = (360f * _circle) / _rotSpeed;
        float elapsedTime = 0f;
        float currentCircle = 0f;

        while (elapsedTime < totalRotationTime)
        {
            float angle = _rotSpeed * Time.deltaTime;
            star.transform.Rotate(0, angle, 0);

            currentCircle += angle;
            if (currentCircle >= 360f)
            {
                star.sprite = goldStar;
                currentCircle -= 360f;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        star.transform.rotation = Quaternion.identity;
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
        if (StoryInteractableControl_Momotaro.isSuccessfulPerformance)
        {
            StoryNpcAnimator_Momotaro._performancesNum = 3;
        }
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
