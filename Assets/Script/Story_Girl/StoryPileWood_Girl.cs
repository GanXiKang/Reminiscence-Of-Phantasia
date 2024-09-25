using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    GameObject player;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip fire;
    bool isOnce = true;

    [Header("Effects")]
    public GameObject fireEffect;
    public static bool isFireActice = false;
    float _snapDistance = 18f;

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

        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance)
        {
            StoryThermometerControl_Girl.isFireBeside = false;
        }
        else
        {
            StoryThermometerControl_Girl.isFireBeside = true;
        }

        if (!isOnce) return;

        BGM.PlayOneShot(fire);
        isOnce = false;
    }
}
