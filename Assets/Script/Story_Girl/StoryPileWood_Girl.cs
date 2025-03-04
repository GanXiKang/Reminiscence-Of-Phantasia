using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    GameObject player;
    AudioSource campfire;

    [Header("Effects")]
    public GameObject fireEffect;
    public static bool isFireActice;
    float _snapDistance = 20f;

    void Start()
    {
        player = GameObject.Find("Player");
        campfire = GetComponent<AudioSource>();

        isFireActice = false;
    }

    void Update()
    {
        fireEffect.SetActive(isFireActice);
        campfire.volume = SettingControl.volumeBGM;

        DistancePileWoodFire();
    }

    void DistancePileWoodFire()
    {
        if (!isFireActice) return;

        if (Vector3.Distance(fireEffect.transform.position, player.transform.position) <= _snapDistance)
        {
            StoryThermometerControl_Girl.isFireBeside = true;
            campfire.Play();
        }
        else
        {
            StoryThermometerControl_Girl.isFireBeside = false;
            campfire.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(fireEffect.transform.position, _snapDistance);
    }
}
