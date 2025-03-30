using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySuppliesGame_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip gainEnergy;

    [Header("UI")]
    public GameObject suppliesUI;

    [Header("SceneGameObject")]
    public GameObject sceneObject;

    float _gameCount = 0;

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
    }

    void Update()
    {

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
