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
    public AudioClip gainEnergy;

    [Header("Camera")]
    public GameObject suppliesCamera;

    [Header("UI")]
    public GameObject suppliesUI;
    public GameObject resultUI;
    public Image resultImage;
    public Sprite win, Lose;

    [Header("ScoreUI")]
    public Image scoreBG;
    public Text scoreText;
    public Text scoreTargetText;
    int _score;

    [Header("PatienceUI")]
    public Image patienceBG;
    public Image barA, barB;
    float _patience;
    float _smoothSpeed = 5f;

    [Header("TimeUI")]
    public Image timeBG;
    public Text timeText;
    float _gameTime;
    bool isBusyTime;

    [Header("NeedUI")]
    public GameObject needUI;
    public GameObject diagolue;
    public GameObject diagolueBG;
    public GameObject[] item;
    public GameObject[] itemCorrect;
    public Transform[] pointItem;
    public Sprite[] itemSprite;
    bool isNeedItem;
    bool isRandomOnce;
    int _itemCount;
    int[] _itemNumber = new int[4];

    [Header("ComboUI")]
    public GameObject comboUI;
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

        suppliesUI.SetActive(true);
        sceneObject.SetActive(true);
        suppliesCamera.SetActive(true);
        gameEasyObject.SetActive(StoryGameControl_Prince.isSuppliesGameEasy);
        gameHardObject.SetActive(StoryGameControl_Prince.isSuppliesGameHard);

        isPlayerMove = true;
        isCarrying = true;
        isRecordResidentOnce = true;
        isBusyTime = false;
        isLineUpMoving = false;

        _score = 0;
        _patience = 100f;
        _gameTime = 90f;
        _combo = 0;
        _firstResident = 1;

        _pointNum = StoryGameControl_Prince.isSuppliesGameEasy ? 2 : 5;
        player.transform.rotation = boxPoint[_pointNum].rotation;

        yield return new WaitForSeconds(2f);
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
        scoreText.text = _score.ToString();
        scoreTargetText.text = StoryGameControl_Prince.isSuppliesGameEasy ? "/2500" : "/3000";
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

        if (_patience <= 0 && isGameStart)
        {
            GameEnd();
        }
    }
    void GameTime()
    {
        timeText.text = _gameTime.ToString("0");

        if (!isGameStart) return;

        if (_gameTime > 0)
        {
            _gameTime -= Time.deltaTime;
            if(_gameTime <= 30)
                isBusyTime = true;
        }
        else
        {

        }
    }
    void Need()
    {
        Vector3 offset = new Vector3(0f, 300f, 0f);
        Vector3 needPos = suppliesCamera.GetComponent<Camera>().WorldToScreenPoint(lineUpPoint[1].position);
        diagolue.transform.position = needPos + offset;

        diagolueBG.SetActive(isNeedItem);

        if (isNeedItem)
        {
            if (isRandomOnce)
            {
                _itemCount = StoryGameControl_Prince.isSuppliesGameEasy ? 1 : Random.Range(1, 2);
                for (int n = 1; n <= _itemCount; n++)
                    _itemNumber[n] = StoryGameControl_Prince.isSuppliesGameEasy ? Random.Range(1, 4) : Random.Range(4, 8);
                isRandomOnce = false;
            }

            switch (_itemCount)
            {
                case 1:
                    diagolueBG.transform.localScale = new Vector3(2.5f, 2.5f, 1f);
                    item[1].SetActive(true);
                    item[1].transform.position = pointItem[1].position;
                    item[1].GetComponent<Image>().sprite = itemSprite[_itemNumber[1]];
                    //itemImage[1].fillAmount = Mathf.Lerp(itemImage[1].fillAmount, 1f, Time.deltaTime * 1f);
                    break;

                case 2:
                    diagolueBG.transform.localScale = new Vector3(4f, 2.5f, 1f);
                    for (int c = 1; c <= _itemCount; c++)
                    {
                        item[c].SetActive(true);
                        item[c].transform.position = pointItem[c + 1].position;
                        item[c].GetComponent<Image>().sprite = itemSprite[_itemNumber[c]];
                        //itemImage[c].fillAmount = Mathf.Lerp(itemImage[c].fillAmount, 1f, Time.deltaTime * 1f);
                    }
                    break;

                case 3:
                    diagolueBG.transform.localScale = new Vector3(5.5f, 2.5f, 1f);
                    for (int c = 1; c <= _itemCount; c++)
                    {
                        item[c].SetActive(true);
                        item[c].transform.position = pointItem[c + 3].position;
                        item[c].GetComponent<Image>().sprite = itemSprite[_itemNumber[c]];
                        //itemImage[c].fillAmount = Mathf.Lerp(itemImage[c].fillAmount, 1f, Time.deltaTime * 1f);
                    }
                    break;
            }
        }
        else 
        {
            for (int t = 1; t < item.Length; t++)
            {
                item[t].SetActive(false);
                item[t].GetComponent<Image>().fillAmount = 0;
            }
        }
    }
    void Combo()
    {
        comboText.text = _combo.ToString();
    }
    void KeyBoardControl()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _pointNum -= 1;
            StoryPlayerControl._direction = 0;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _pointNum += 1;
            StoryPlayerControl._direction = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLineUpMoving = true;
            isNeedItem = false;
            if (_pointNum == 5)
                isCorrect = true;
            else
                isError = true;
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

    void GameEnd()
    {
        StoryUIControl_Prince.isSuppliesActive = false;
        if (StoryGameControl_Prince.isSuppliesGameEasy)
        {
            StoryGameControl_Prince.isPassGameEasy = true;
        }
        else if (StoryGameControl_Prince.isSuppliesGameHard)
        {
            StoryGameControl_Prince.isPassGameHard = true;
        }
    }

    void OnDisable()
    {
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
