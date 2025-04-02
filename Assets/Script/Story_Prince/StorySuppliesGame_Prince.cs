using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySuppliesGame_Prince : MonoBehaviour
{
    GameObject player;
    Animator anim;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip suppliesBGM, nowBGM, pastBGM;
    public AudioClip gainEnergy;
    public AudioClip move, correct, error, whist, score, timehint, need, suc, fai, rea, peak;
    bool isScoreOnce;

    [Header("Camera")]
    public GameObject suppliesCamera;

    [Header("UI")]
    public GameObject suppliesUI;
    public GameObject resultUI;
    public Image resultImage;
    public Sprite win, lose, patience;
    public Sprite really, go, busy;

    [Header("ScoreUI")]
    public Image scoreBG;
    public Sprite pass, not;
    public Text scoreText;
    public Text scoreTargetText;
    int _score;
    int _scoreTarget;

    [Header("PatienceUI")]
    public Image patienceUI;
    public Sprite happy, normal, angry;
    public Image barA, barB;
    float _patience;
    float _smoothSpeed = 5f;

    [Header("TimeUI")]
    public Image timeBG;
    public Sprite t, b, e;
    public Text timeText;
    float _gameTime;
    bool isBusyTime;
    bool isEnterBusyTime;
    bool isOnce;
    int _timeHint;

    [Header("NeedUI")]
    public GameObject needUI;
    public GameObject diagolue;
    public GameObject diagolueBG;
    public GameObject[] item;
    public GameObject[] itemBlack;
    public GameObject[] itemCorrect;
    public Transform[] pointItem;
    public Sprite[] itemSprite;
    float _appearSpeed = 5f;
    bool isNeedItem;
    bool isRandomOnce;
    bool[] isItemCorrect = new bool[4];
    int[] _itemNumber = new int[4];
    int _itemCount;

    [Header("ComboUI")]
    public GameObject comboUI;
    public Sprite c, s, g;
    public Text comboText;
    int _combo;

    [Header("SceneGameObject")]
    public GameObject sceneObject;
    public GameObject gameEasyObject;
    public GameObject gameHardObject;
    public GameObject[] boxSprite;
    public Transform[] boxPoint;

    [Header("Resident")]
    public GameObject[] residentEasy;
    public GameObject[] residentHard;
    public Transform[] lineUpPoint;
    Transform[] resident;
    bool isRecordResidentOnce;
    bool isLineUpMoving;
    int _firstResident;

    [Header("Effects")]
    public GameObject shinyEF;

    //PlayerMove
    int _pointNum;
    bool isPlayerMove = false;
    float _moveSpeed = 5f;
    //Animator
    bool isCarrying = false;
    bool isCorrect = false;
    bool isError = false;
    //GameControl
    float _gameCount = 0;
    bool isGameStart = false;
    
    void Start()
    {
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
    }

    void OnEnable()
    {
        StartCoroutine(StartSuppliesGame());
    }

    IEnumerator StartSuppliesGame()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1.2f);

        BGM.Stop();
        BGM.clip = suppliesBGM;
        BGM.Play();

        suppliesUI.SetActive(true);
        sceneObject.SetActive(true);
        suppliesCamera.SetActive(true);
        gameEasyObject.SetActive(StoryGameControl_Prince.isSuppliesGameEasy);
        gameHardObject.SetActive(StoryGameControl_Prince.isSuppliesGameHard);

        isScoreOnce = true;
        isPlayerMove = true;
        isCarrying = true;
        isRecordResidentOnce = true;
        isOnce = true;
        isBusyTime = false;
        isEnterBusyTime = false;
        isLineUpMoving = false;

        _score = 0;
        _patience = 100f;
        _gameTime = StoryGameControl_Prince.isSuppliesGameEasy ? 45f : 60f;
        _timeHint = 5;
        _combo = 0;
        _firstResident = 1;

        _pointNum = StoryGameControl_Prince.isSuppliesGameEasy ? 2 : 5;
        player.transform.rotation = boxPoint[_pointNum].rotation;

        yield return new WaitForSeconds(0.5f);
        resultUI.SetActive(true);
        resultImage.sprite = really;
        BGM.PlayOneShot(rea);

        yield return new WaitForSeconds(0.8f);
        resultImage.sprite = go;
        BGM.PlayOneShot(whist);

        yield return new WaitForSeconds(0.5f);
        resultUI.SetActive(false);
        isGameStart = true;
        isNeedItem = true;
        isRandomOnce = true;
    }

    void Update()
    {
        Score();
        Patience();
        GameTime();
        Need();
        Combo();
        KeyBoardControl();
        PlayerMoveAndAnimator();
        BoxSpriteScale();
        LineUp();
    }

    void Score()
    {
        _scoreTarget = StoryGameControl_Prince.isSuppliesGameEasy ? 2500 : 3000;

        scoreText.text = _score.ToString();
        scoreTargetText.text = "/" + _scoreTarget;

        if (_score >= _scoreTarget)
        {
            if (isScoreOnce)
            {
                BGM.PlayOneShot(score);
                isScoreOnce = false;
            }
            scoreBG.sprite = pass;
            StoryNpcAnimator_Prince.isSmiling_Prince = true;
            StoryNpcAnimator_Prince.isSmiling_Swallow = true;
        }
        else
        {
            scoreBG.sprite = not;
        }
    }
    void Patience()
    {
        _patience = Mathf.Clamp(_patience, 0, 100);
        float amount = _patience / 100;

        if (amount > barB.fillAmount)
        {
            barA.fillAmount = amount;
            if (barB.fillAmount != amount)
                barB.fillAmount = Mathf.Lerp(barB.fillAmount, barA.fillAmount, Time.deltaTime * _smoothSpeed);
        }
        else
        {
            barB.fillAmount = amount;
            if (barA.fillAmount != amount)
                barA.fillAmount = Mathf.Lerp(barA.fillAmount, barB.fillAmount, Time.deltaTime * _smoothSpeed);
        }

        float reducePatience;
        if (StoryGameControl_Prince.isPassGameEasy)
        {
            reducePatience = isBusyTime ? 3f : 2f;
        }
        else
        {
            reducePatience = isBusyTime ? 4f : 5f;
        }

        if (isNeedItem && !isEnterBusyTime && isGameStart && !SettingControl.isSettingActive)
        {
            _patience -= reducePatience * Time.deltaTime;
        }

        if (_patience <= 0 && isGameStart)
        {
            StartCoroutine(EndGame(false));
        }
    }
    void GameTime()
    {
        timeText.text = _gameTime.ToString("0");

        if (SettingControl.isSettingActive) return;
        if (!isGameStart) return;
        if (isEnterBusyTime) return;

        float _busyTime = StoryGameControl_Prince.isSuppliesGameEasy ? 20f : 30f;

        if (_gameTime > 0)
        {
            _gameTime -= Time.deltaTime;

            if (_gameTime <= _busyTime)
            {
                isBusyTime = true;
                if (isOnce)
                {
                    isOnce = false;
                    BGM.PlayOneShot(peak);
                    isEnterBusyTime = true;
                    timeBG.sprite = b;
                    resultUI.SetActive(true);
                    resultImage.sprite = busy;
                    Invoke("FalseIsEnterBusyTime", 1.5f);
                }
            }
            else
            {
                timeBG.sprite = t;
            }
        }
        else
        {
            StartCoroutine(EndGame(true));
        }

        int _gameTimeInt = Mathf.FloorToInt(_gameTime);
        if (_gameTimeInt <= 5 && _gameTimeInt == _timeHint)
        {
            BGM.PlayOneShot(timehint);
            _timeHint--;
        }
    }
    void FalseIsEnterBusyTime()
    {
        isEnterBusyTime = false;
        resultUI.SetActive(false);
    }
    void Need()
    {
        Vector3 offset = new Vector3(0f, 300f, 0f);
        Vector3 needPos = suppliesCamera.GetComponent<Camera>().WorldToScreenPoint(lineUpPoint[1].position);
        diagolue.transform.position = needPos + offset;

        diagolueBG.SetActive(isNeedItem);
        for (int t = 1; t < item.Length; t++)
            itemCorrect[t].SetActive(isItemCorrect[t]);
        _appearSpeed = isBusyTime ? 5f : 2f;

        if (isNeedItem)
        {
            if (isRandomOnce)
            {
                BGM.PlayOneShot(need);
                patienceUI.sprite = normal;
                _itemCount = StoryGameControl_Prince.isSuppliesGameEasy ? 1 : Random.Range(1, 4);
                for (int n = 1; n <= _itemCount; n++)
                    _itemNumber[n] = StoryGameControl_Prince.isSuppliesGameEasy ? Random.Range(1, 4) : Random.Range(4, 8);
                isRandomOnce = false;
            }

            switch (_itemCount)
            {
                case 1:
                    diagolueBG.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
                    item[1].SetActive(true);
                    itemBlack[1].SetActive(true);
                    item[1].transform.position = pointItem[1].position;
                    itemBlack[1].transform.position = pointItem[1].position;
                    item[1].GetComponent<Image>().sprite = itemSprite[_itemNumber[1]];
                    itemBlack[1].GetComponent<Image>().sprite = itemSprite[_itemNumber[1]];
                    item[1].GetComponent<Image>().fillAmount = Mathf.Lerp(item[1].GetComponent<Image>().fillAmount, 1f, Time.deltaTime * _appearSpeed);
                    break;

                case 2:
                    diagolueBG.transform.localScale = new Vector3(4f, 2.5f, 1f);
                    for (int c = 1; c <= _itemCount; c++)
                    {
                        item[c].SetActive(true);
                        itemBlack[c].SetActive(true);
                        item[c].transform.position = pointItem[c + 1].position;
                        itemBlack[c].transform.position = pointItem[c + 1].position;
                        item[c].GetComponent<Image>().sprite = itemSprite[_itemNumber[c]];
                        itemBlack[c].GetComponent<Image>().sprite = itemSprite[_itemNumber[c]];
                        item[c].GetComponent<Image>().fillAmount = Mathf.Lerp(item[1].GetComponent<Image>().fillAmount, 1f, Time.deltaTime * _appearSpeed);
                    }
                    break;

                case 3:
                    diagolueBG.transform.localScale = new Vector3(5.5f, 2.5f, 1f);
                    for (int c = 1; c <= _itemCount; c++)
                    {
                        item[c].SetActive(true);
                        itemBlack[c].SetActive(true);
                        item[c].transform.position = pointItem[c + 3].position;
                        itemBlack[c].transform.position = pointItem[c + 3].position;
                        item[c].GetComponent<Image>().sprite = itemSprite[_itemNumber[c]];
                        itemBlack[c].GetComponent<Image>().sprite = itemSprite[_itemNumber[c]];
                        item[c].GetComponent<Image>().fillAmount = Mathf.Lerp(item[1].GetComponent<Image>().fillAmount, 1f, Time.deltaTime * _appearSpeed);
                    }
                    break;
            }
        }
        else 
        {
            for (int t = 1; t < item.Length; t++)
            {
                item[t].SetActive(false);
                itemBlack[t].SetActive(false);
                item[t].GetComponent<Image>().fillAmount = 0;
                isItemCorrect[t] = false;
            }
        }
    }
    void Combo()
    {
        comboText.text = _combo.ToString();
        comboUI.SetActive(_combo >= 10);

        if (_combo >= 30)
            comboUI.GetComponent<Image>().sprite = g;
        else if (_combo >= 20)
            comboUI.GetComponent<Image>().sprite = s;
        else if (_combo >= 10)
            comboUI.GetComponent<Image>().sprite = c;
    }
    void AddComboScore()
    {
        int add = _combo / 10;
        _score += add * 10;
    }
    void KeyBoardControl()
    {
        if (SettingControl.isSettingActive) return;
        if (!isGameStart) return;
        if (isEnterBusyTime) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            _pointNum -= 1;
            BGM.PlayOneShot(move);
            StoryPlayerControl._direction = 0;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _pointNum += 1;
            BGM.PlayOneShot(move);
            StoryPlayerControl._direction = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int c = 1; c <= _itemCount; c++)
            {
                if (!isItemCorrect[c])
                {
                    if (_itemNumber[c] == 0) return;

                    if (_pointNum == _itemNumber[c])
                    {
                        BGM.PlayOneShot(correct);
                        _combo++;
                        AddComboScore();
                        _score += 50;
                        patienceUI.sprite = normal;
                        isCorrect = true;
                        isItemCorrect[c] = true;
                        break;
                    }
                    else
                    {
                        BGM.PlayOneShot(error);
                        _combo = 0;
                        _patience -= 5f;
                        patienceUI.sprite = angry;
                        isError = true;
                        break;
                    }
                }
            }

            bool allItemsCorrect = true;
            for (int i = 1; i <= _itemCount; i++)
            {
                if (!isItemCorrect[i])
                {
                    allItemsCorrect = false;
                    break;
                }
            }

            if (allItemsCorrect)
            {
                _score += 100;
                _patience += 5f;
                patienceUI.sprite = happy;
                isLineUpMoving = true;
                isNeedItem = false;
                shinyEF.SetActive(true);
                for (int n = 1; n <= 3; n++)
                    _itemNumber[n] = 0;
            }
        }
    }
    void PlayerMoveAndAnimator()
    {
        if (!isPlayerMove) return;

        _pointNum = StoryGameControl_Prince.isSuppliesGameEasy ? Mathf.Clamp(_pointNum, 1, 3) : Mathf.Clamp(_pointNum, 4, 7);
        player.transform.position = Vector3.MoveTowards(player.transform.position, boxPoint[_pointNum].position, _moveSpeed);

        anim.SetBool("isCarrying", isCarrying);
        anim.SetBool("isCarryHappy", isCorrect);
        anim.SetBool("isCarrySurprised", isError);

        if (isCorrect || isError)
            Invoke("FalseAnimation", 0.3f);
    }
    void FalseAnimation()
    {
        isCorrect = false;
        isError = false;
    }
    void BoxSpriteScale()
    {
        Vector3 oriScale = new Vector3(0.0025f, 0.0025f, 0f);
        Vector3 bigScale = new Vector3(0.003f, 0.003f, 0f);

        int a = StoryGameControl_Prince.isSuppliesGameEasy ? 1 : 4;
        int b = StoryGameControl_Prince.isSuppliesGameEasy ? 4 : 8;

        for (int i = a; i < b; i++)
        {
            if (i == _pointNum)
                boxSprite[i].transform.localScale = bigScale;
            else
                boxSprite[i].transform.localScale = oriScale;
        }
    }
    void LineUp()
    {
        if (isRecordResidentOnce)
        {
            if (StoryGameControl_Prince.isSuppliesGameEasy)
            {
                resident = new Transform[residentEasy.Length];
                for (int i = 0; i < residentEasy.Length; i++)
                {
                    if (residentEasy[i] != null)
                        resident[i] = residentEasy[i].transform;
                }
            }
            else
            {
                resident = new Transform[residentHard.Length];
                for (int i = 0; i < residentHard.Length; i++)
                {
                    if (residentHard[i] != null)
                        resident[i] = residentHard[i].transform;
                }
            }

            for (int i = 1; i < 4; i++)
                resident[i].position = lineUpPoint[i].position;
            for (int d = 4; d < 7; d++)
                resident[d].position = lineUpPoint[7].position;

            isRecordResidentOnce = false;
        }

        if (isLineUpMoving)
        {
            resident[_firstResident].position = Vector3.Lerp(
                resident[_firstResident].position, 
                lineUpPoint[6].position, 
                Time.deltaTime * _moveSpeed
                );

            int pointIndex = 1;
            int residentCount = isBusyTime ? 5 : 3;
            for (int i = 1; i <= residentCount; i++)
            {
                int currentResident = _firstResident + i;

                if (currentResident > 6)
                    currentResident -= 6;

                resident[currentResident].position = Vector3.Lerp(
                    resident[currentResident].position,
                    lineUpPoint[pointIndex].position,
                    Time.deltaTime * _moveSpeed
                );

                pointIndex++;
            }

            if (HasReachedTarget())
            {
                isLineUpMoving = false;
                isNeedItem = true;
                isRandomOnce = true;
                resident[_firstResident].position = lineUpPoint[7].position;

                _firstResident++;
                if (_firstResident >= resident.Length)
                    _firstResident = 1;
            }
        }
    }
    bool HasReachedTarget()
    {
        float tolerance = 0.1f;

        if (Vector3.Distance(resident[_firstResident].position, lineUpPoint[6].position) > tolerance)
            return false;

        int pointIndex = 1;
        int residentCount = isBusyTime ? 5 : 3;
        for (int i = 1; i <= residentCount; i++)
        {
            int currentResident = _firstResident + i;
            if (currentResident > 6)
                currentResident -= 6;

            if (Vector3.Distance(resident[currentResident].position, lineUpPoint[pointIndex].position) > tolerance)
                return false;

            pointIndex++;
        }

        return true;
    }

    IEnumerator EndGame(bool isTimeOut)
    {
        BGM.PlayOneShot(whist);
        isGameStart = false;
        isNeedItem = false;
        timeBG.sprite = e;
        yield return new WaitForSeconds(1f);

        resultUI.SetActive(true);
        if (isTimeOut)
        {
            if (_score >= _scoreTarget)
            {
                isCorrect = true;
                resultImage.sprite = win;
                patienceUI.sprite = happy;
                BGM.PlayOneShot(suc);

                if (StoryGameControl_Prince.isSuppliesGameEasy)
                    StoryGameControl_Prince.isPassGameEasy = true;
                else if (StoryGameControl_Prince.isSuppliesGameHard)
                    StoryGameControl_Prince.isPassGameHard = true;
            }
            else
            {
                isError = true;
                resultImage.sprite = lose;
                patienceUI.sprite = normal;
                BGM.PlayOneShot(fai);
            }
        }
        else
        {
            isError = true;
            resultImage.sprite = patience;
            patienceUI.sprite = angry;
            BGM.PlayOneShot(fai);
        }

        yield return new WaitForSeconds(1f);
        isCarrying = false;
        yield return new WaitForSeconds(1f);
        StoryUIControl_Prince.isSuppliesActive = false;
    }

    void OnDisable()
    {
        BGM.Stop();
        BGM.clip = _gameCount == 0 ? pastBGM : nowBGM;
        BGM.Play();

        isPlayerMove = false;
        resultUI.SetActive(false);
        suppliesUI.SetActive(false);
        sceneObject.SetActive(false);
        suppliesCamera.SetActive(false);
        timeBG.sprite = t;
        patienceUI.sprite = normal;

        StoryNpcAnimator_Prince.isSmiling_Prince = false;
        StoryNpcAnimator_Prince.isSmiling_Swallow = false;

        player.transform.rotation = Quaternion.Euler(30f, -45f, 0f);

        if (StoryGameControl_Prince.isPassGameEasy && _gameCount == 0)
        {
            _gameCount++;
            StoryNpcAnimator_Prince.isFindGem = true;
            StoryGameControl_Prince.isSuppliesGameEasy = false;
        }
        if (StoryGameControl_Prince.isPassGameHard && _gameCount >= 1)
        {
            StoryUIControl_Prince.isDialogue = true;
            StoryDialogueControl_Prince._isAboveWho1 = 2;
            StoryDialogueControl_Prince._isAboveWho2 = 4;
            StoryDialogueControl_Prince._textCount = 41;
            StoryGameControl_Prince.isSuppliesGameHard = false;
        }

        BGM.PlayOneShot(gainEnergy);
        StorySkillControl_Prince.isGainEnegry = true;
        StorySkillControl_Prince._gainEnegryValue = 0.2f;
    }
}
