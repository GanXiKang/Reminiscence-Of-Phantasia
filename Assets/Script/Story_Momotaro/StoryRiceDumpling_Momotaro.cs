using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryRiceDumpling_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip plazaBGM;

    [Header("SkillUI")]
    public GameObject skillUI;
    public static bool isSkillActive = false;

    [Header("RoleUI")]
    public GameObject roleUI;
    public GameObject[] roleButton;
    public static bool isRoleActive = false;
    public static int _whoEatGoldRice;
    public static bool isEat = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        skillUI.SetActive(isSkillActive && !StoryUIControl_Momotaro.isPerformances);
        roleUI.SetActive(isRoleActive);

        RoleUI();
        RaccoonSkill();
        ParrotPerformances();
    }

    void RoleUI()
    {
        switch (_whoEatGoldRice)
        {
            case 3:
                roleButton[1].SetActive(true);
                break;

            case 6:
                roleButton[2].SetActive(true);
                break;

            case 9:
                roleButton[3].SetActive(true);
                break;

            default:
                return;
        }
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
            StoryUIControl_Momotaro.isPerformances = !StoryUIControl_Momotaro.isPerformances;
            if (!StoryUIControl_Momotaro.isPerformances)
            {
                BGM.Stop();
                BGM.clip = plazaBGM;
                BGM.Play();
            }
        }
    }

    public void Button_RiceDumpling()
    {
        isRoleActive = !isRoleActive;
    }
    public void Button_Role(int role)
    {
        isEat = true;
        Invoke("EatFinish", 0.3f);

        isRoleActive = false;
        switch (role)
        {
            case 1:
                StoryPlayerAnimator_Momotaro.isDonkey = !StoryPlayerAnimator_Momotaro.isDonkey;
                break;

            case 2:
                StoryPlayerAnimator_Momotaro.isRaccoon = !StoryPlayerAnimator_Momotaro.isRaccoon;
                break;

            case 3:
                StoryPlayerAnimator_Momotaro.isParrot = !StoryPlayerAnimator_Momotaro.isParrot;
                break;
        }
    }

    void EatFinish()
    {
        isEat = false;
    }
}
