using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryRiceDumpling_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;

    [Header("SkillUI")]
    public GameObject skill;
    public static bool isSkillActive;

    public static int _whoEatGoldRice;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        skill.SetActive(isSkillActive);

        RaccoonSkill();
        ParrotPerformances();
    }

    void RaccoonSkill()
    {
        if (!StoryPlayerAnimator_Momotaro.isRaccoon) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            StoryPlayerAnimator_Momotaro.isRaccoonStone = !StoryPlayerAnimator_Momotaro.isRaccoonStone;
        }
    }
    void ParrotPerformances()
    {
        if (!StoryPlayerAnimator_Momotaro.isParrot) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            StoryPlayerAnimator_Momotaro.isRaccoonStone = !StoryPlayerAnimator_Momotaro.isRaccoonStone;
        }
    }

    public void RiceDumpling_Button()
    {
        switch (_whoEatGoldRice)
        {
            case 3:
                StoryPlayerAnimator_Momotaro.isDonkey = !StoryPlayerAnimator_Momotaro.isDonkey;
                break;

            case 6:
                StoryPlayerAnimator_Momotaro.isRaccoon = !StoryPlayerAnimator_Momotaro.isRaccoon;
                break;

            case 9:
                StoryPlayerAnimator_Momotaro.isParrot = !StoryPlayerAnimator_Momotaro.isParrot;
                break;
        }
    }
}
