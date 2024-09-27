using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip fire;
    bool isPlayAudio = false;

    [Header("Effects")]
    public GameObject fireEffect;
    public static bool isFireActice = true;
    float _snapDistance = 65f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        fireEffect.SetActive(isFireActice);

        DistancePileWoodFire();
    }

    void DistancePileWoodFire()
    {
        if (!isFireActice) return;

        if (Vector3.Distance(transform.position, player.transform.position) <= _snapDistance)
        {
            StoryThermometerControl_Girl.isFireBeside = true;

            if (!isPlayAudio)
            {
                BGM.PlayOneShot(fire);
                isPlayAudio = true;
                Invoke("isRenewPlayerAudio", 7f);
            }
        }
        else
        {
            StoryThermometerControl_Girl.isFireBeside = false;       
        }
    }

    void isRenewPlayerAudio()
    {
        isPlayAudio = false;
    }
}
