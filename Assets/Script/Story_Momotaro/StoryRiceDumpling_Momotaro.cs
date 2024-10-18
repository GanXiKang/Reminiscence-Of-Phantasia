using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryRiceDumpling_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip use;

    [Header("SkillUI")]
    public GameObject skill;
    public static bool isSkillActive;

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

    }
}
