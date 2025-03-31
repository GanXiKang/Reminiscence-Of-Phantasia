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
    float _smoothSpeed = 3f;

    [Header("TimeUI")]
    public Image timeBG;
    public Text timeText;
    float _gameTime;

    [Header("NeedUI")]
    public GameObject needUI;
    public GameObject diagolueBG;
    public GameObject[] item;
    public Transform[] pointItem;
    public Sprite[] itemSprite;

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
        GameTime();
        Score();
        Patience();
        Combo();
        KeyBoardControl();
        PlayerMoveAndAnimator();
        BoxSpriteScale();
    }

    void GameTime()
    {
        if (!isGameStart) return;

        if (_gameTime > 0)
        {
            _gameTime -= Time.deltaTime;
            timeText.text = _gameTime.ToString("0");
        }
        else 
        {
            
        }
    }
    void Score()
    {
        scoreText.text = _score.ToString();
        scoreTargetText.text = StoryGameControl_Prince.isSuppliesGameEasy ? "/2500" : "/3000";
    }
    void Patience()
    {
        _patience = Mathf.Clamp(_patience, 0, 100);
        if (Input.GetKeyDown(KeyCode.P))
            _patience -= 10;
        barB.fillAmount = _patience / 100;
        if (barA.fillAmount != barB.fillAmount)
            barA.fillAmount = Mathf.Lerp(barA.fillAmount, barB.fillAmount, Time.deltaTime * _smoothSpeed);
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
        Vector3 oriScale = new Vector3(0.1f, 0.1f, 0f);
        Vector3 bigScale = new Vector3(0.12f, 0.12f, 0f);

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
