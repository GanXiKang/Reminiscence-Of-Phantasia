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
    bool isBusyTime = false;

    [Header("NeedUI")]
    public GameObject needUI;
    public GameObject diagolueBG;
    public GameObject[] item;
    public GameObject[] itemCorrect;
    public Transform[] pointItem;
    public Sprite[] itemSprite;
    bool isNeedItem;

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
    bool isLineUpMoving;
    int firstResident;

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
        _score = 0;
        _patience = 100f;
        _gameTime = 90f;
        _combo = 0;
        _pointNum = StoryGameControl_Prince.isSuppliesGameEasy ? 2 : 5;
        player.transform.rotation = boxPoint[_pointNum].rotation;
        yield return new WaitForSeconds(2f);
        isGameStart = true;
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
        if (!isGameStart) return;

        if (_gameTime > 0)
        {
            _gameTime -= Time.deltaTime;
            timeText.text = _gameTime.ToString("0");

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
        diagolueBG.transform.position = needPos + offset;
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
            print(_pointNum);
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
        if (isLineUpMoving)
        {
            isLineUpMoving = false;

            for (int i = 1; i < 7 - 1; i++)
            {
                
            }
        }
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

    IEnumerator LineUpMoving()
    {
        
        // 讓隊首的居民移動到最後的待命位置
        GameObject firstResident = residentHard[1];
        yield return MoveTo(firstResident, lineUpPoint[6].position);
        firstResident.transform.position = lineUpPoint[7].position;

        // 其他居民依次向前移動
        for (int i = 1; i < residentHard.Length - 1; i++)
        {
            yield return MoveTo(residentHard[i + 1], lineUpPoint[i].position);
        }
    }

    IEnumerator MoveTo(GameObject obj, Vector3 targetPosition)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPosition = obj.transform.position;

        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = targetPosition;
    }
}
