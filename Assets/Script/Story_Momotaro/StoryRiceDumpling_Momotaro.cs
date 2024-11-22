using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryRiceDumpling_Momotaro : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip donkey, raccoon, parrot;

    [Header("SkillUI")]
    public GameObject skillUI;
    public GameObject riceButton, riceOpenButton;
    public static bool isSkillActive = false;
    public static bool isChangeRoles = false;

    [Header("RoleUI")]
    public GameObject roleUI;
    public GameObject[] roleButton;
    public static bool isRoleActive = false;
    public static int _whoEatGoldRice;
    public static bool isEat = false;

    [Header("RaccoonSkillUI")]
    public GameObject raccoonSkillUI;
    public GameObject stoneButton, notStoneButton;

    void Update()
    {
        skillUI.SetActive(isSkillActive && !StoryUIControl_Momotaro.isPerformances);
        roleUI.SetActive(isRoleActive);
        raccoonSkillUI.SetActive(StoryPlayerAnimator_Momotaro.isRaccoon);

        RoleUI();
        RaccoonSkill();

        if (Input.GetKeyDown(KeyCode.E) && skillUI.activeSelf)
        {
            Button_RiceDumpling();
        }
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
        }
        riceButton.SetActive(!riceOpenButton.activeSelf);
        riceOpenButton.SetActive(isRiceOpenButtonActive());

        bool isRiceOpenButtonActive()
        {
            return isRoleActive || 
                   StoryPlayerAnimator_Momotaro.isDonkey || 
                   StoryPlayerAnimator_Momotaro.isRaccoon ||
                   StoryPlayerAnimator_Momotaro.isStone;
        }
    }
    void RaccoonSkill()
    {
        if (!StoryPlayerAnimator_Momotaro.isRaccoon) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Button_RaccoonSkill();
        }
    }

    public void Button_RiceDumpling()
    {
        if (!isChangeRoles)
        {
            isRoleActive = !isRoleActive;
        }
        else
        {
            isChangeRoles = false;
            riceButton.SetActive(true);
            riceOpenButton.SetActive(false);
            StoryPlayerAnimator_Momotaro.isDonkey = false;
            StoryPlayerAnimator_Momotaro.isRaccoon = false;
            StoryPlayerAnimator_Momotaro.isStone = false;
            StoryPlayerAnimator_Momotaro.isParrot = false;
        }
    }
    public void Button_Role(int role)
    {
        if (StoryUIControl_Momotaro.isDialogue) return;

        isEat = true;
        Invoke("EatFinish", 0.3f);

        isChangeRoles = true;
        isRoleActive = false;
        switch (role)
        {
            case 1:
                BGM.PlayOneShot(donkey);
                StoryPlayerAnimator_Momotaro.isDonkey = true;
                break;

            case 2:
                BGM.PlayOneShot(raccoon);
                StoryPlayerAnimator_Momotaro.isRaccoon = true;
                break;

            case 3:
                BGM.PlayOneShot(parrot);
                StoryPlayerAnimator_Momotaro.isParrot = true;
                break;
        }
    }
    public void Button_RaccoonSkill()
    {
        StoryPlayerAnimator_Momotaro.isStone = !StoryPlayerAnimator_Momotaro.isStone;
        stoneButton.SetActive(!StoryPlayerAnimator_Momotaro.isStone);
        notStoneButton.SetActive(StoryPlayerAnimator_Momotaro.isStone);
    }

    void EatFinish()
    {
        isEat = false;
    }
}
