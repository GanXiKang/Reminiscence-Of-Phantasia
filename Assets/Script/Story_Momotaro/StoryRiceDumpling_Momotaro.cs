using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryRiceDumpling_Momotaro : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip donkey, raccoon, parrot, open, stone;

    [Header("SkillUI")]
    public GameObject skillUI;
    public GameObject riceButton, riceOpenButton;
    public static bool isSkillActive = false;
    public static bool isChangeRoles = false;

    [Header("RoleUI")]
    public GameObject roleUI;
    public GameObject[] roleButton;
    public Transform[] point;
    public static bool isChangeRolePlot = false;
    public static int _whoEatGoldRice;
    public static bool isEat = false;
    bool isRoleActive = false;

    [Header("RaccoonSkillUI")]
    public GameObject raccoonSkillUI;
    public GameObject stoneButton, notStoneButton;

    void Update()
    {
        skillUI.SetActive(isSkillActive && !StoryUIControl_Momotaro.isPerformances);

        raccoonSkillUI.SetActive(StoryPlayerAnimator_Momotaro.isRaccoon);
        RoleUI();
        RaccoonSkill();

        if (Input.GetKeyDown(KeyCode.E) && skillUI.activeSelf)
        {
            Button_RiceDumpling();
        }
        if (isChangeRolePlot)
        {
            isChangeRolePlot = false;
            roleUI.SetActive(false);
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
                   StoryPlayerAnimator_Momotaro.isParrot;
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
        BGM.PlayOneShot(open);
        if (!isChangeRoles)
        {
            isRoleActive = !isRoleActive;
            if (isRoleActive)
            {
                roleUI.SetActive(true);
                StartCoroutine(RiceAppearAnimation(roleButton[1], point[0].position, point[1].position));
                StartCoroutine(RiceAppearAnimation(roleButton[2], point[0].position, point[2].position));
                StartCoroutine(RiceAppearAnimation(roleButton[3], point[0].position, point[3].position));
            }
            else
            {
                StartCoroutine(RiceDisappearAnimation(roleButton[1], point[1].position, point[0].position));
                StartCoroutine(RiceDisappearAnimation(roleButton[2], point[2].position, point[0].position));
                StartCoroutine(RiceDisappearAnimation(roleButton[3], point[3].position, point[0].position));
            }
        }
        else
        {
            isRoleActive = false;
            isChangeRoles = false;
            riceButton.SetActive(true);
            riceOpenButton.SetActive(false);
            StoryPlayerAnimator_Momotaro.isDonkey = false;
            StoryPlayerAnimator_Momotaro.isRaccoon = false;
            StoryPlayerAnimator_Momotaro.isStone = false;
            StoryPlayerAnimator_Momotaro.isParrot = false;
            StoryPlayerAnimator_Momotaro.isSmokeEF = true;
        }
    }
    public void Button_Role(int role)
    {
        if (StoryUIControl_Momotaro.isDialogue) return;

        isEat = true;
        Invoke("EatFinish", 0.3f);

        isChangeRoles = true;
        StartCoroutine(RiceDisappearAnimation(roleButton[1], point[1].position, point[0].position));
        StartCoroutine(RiceDisappearAnimation(roleButton[2], point[2].position, point[0].position));
        StartCoroutine(RiceDisappearAnimation(roleButton[3], point[3].position, point[0].position));
        StoryPlayerAnimator_Momotaro.isSmokeEF = true;
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
        BGM.PlayOneShot(stone);
        StoryPlayerAnimator_Momotaro.isSmokeEF = true;
        StoryPlayerAnimator_Momotaro.isStone = !StoryPlayerAnimator_Momotaro.isStone;
        stoneButton.SetActive(!StoryPlayerAnimator_Momotaro.isStone);
        notStoneButton.SetActive(StoryPlayerAnimator_Momotaro.isStone);
    }

    void EatFinish()
    {
        isEat = false;
    }

    IEnumerator RiceAppearAnimation(GameObject obj, Vector3 start, Vector3 end)
    {
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();

        obj.transform.position = start;
        cg.alpha = 0;
        cg.interactable = false;

        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);

            cg.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = end;
        cg.alpha = 1;
        cg.interactable = true;
    }
    IEnumerator RiceDisappearAnimation(GameObject obj, Vector3 start, Vector3 end)
    {
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();

        obj.transform.position = start;
        cg.alpha = 1;
        cg.interactable = true;

        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);

            cg.alpha = Mathf.Lerp(1, 0, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = end;
        cg.alpha = 0;
        cg.interactable = false;
        isRoleActive = false;
        roleUI.SetActive(false);
    }
}
