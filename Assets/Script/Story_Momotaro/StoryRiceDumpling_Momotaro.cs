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
    }

    public void RiceDumpling_Button()
    {
        switch (_whoEatGoldRice)
        {
            case 3:
                print("׃���H��");
                break;

            case 6:
                print("׃����؈��");
                break;

            case 9:
                print("׃���W�^��");
                break;
        }
    }
}
