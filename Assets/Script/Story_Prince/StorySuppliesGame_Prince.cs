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
    int _pointNum;
    float _moveSpeed = 5f;

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
        _pointNum = StoryGameControl_Prince.isSuppliesGameEasy ? 2 : 5;
        player.transform.position = boxPoint[_pointNum].position;
        player.transform.rotation = boxPoint[_pointNum].rotation;
    }

    void Update()
    {
        GameKeyBoardControl();
        PlayerMove();
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
        }
    }
    void PlayerMove()
    {
        _pointNum = StoryGameControl_Prince.isSuppliesGameEasy ? Mathf.Clamp(_pointNum, 1, 3) : Mathf.Clamp(_pointNum, 4, 7);
        player.transform.position = Vector3.MoveTowards(player.transform.position, boxPoint[_pointNum].position, _moveSpeed);
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
