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

    [Header("SceneGameObject")]
    public GameObject sceneObject;
    public GameObject gameEasyObject;
    public GameObject gameHardObject;
    public GameObject[] boxSprite;
    public Transform[] boxPoint;

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
        yield return new WaitForSeconds(1f);
        suppliesUI.SetActive(true);
        sceneObject.SetActive(true);
        suppliesCamera.SetActive(true);
        gameEasyObject.SetActive(StoryGameControl_Prince.isSuppliesGameEasy);
        gameHardObject.SetActive(StoryGameControl_Prince.isSuppliesGameHard);
        isPlayerMove = true;
        isCarrying = true;
        _pointNum = StoryGameControl_Prince.isSuppliesGameEasy ? 2 : 5;
        player.transform.rotation = boxPoint[_pointNum].rotation;
    }

    void Update()
    {
        GameKeyBoardControl();
        PlayerMoveAndAnimator();
        BoxSpriteScale();
    }

    void GameKeyBoardControl()
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
        Vector3 bigScale = new Vector3(0.15f, 0.15f, 0f);

        for (int i = 1; i < boxSprite.Length; i++)
        {
            if (i == _pointNum)
            {
                boxSprite[i].transform.localScale = bigScale;
            }
            else
            {
                boxSprite[i].transform.localScale = oriScale;
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
}
