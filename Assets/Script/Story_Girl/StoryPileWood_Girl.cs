using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    GameObject player;
    AudioSource fire;

    [Header("Effects")]
    public GameObject fireEffect;
    public static bool isFireActice = true;
    float _snapDistance = 20f;

    void Start()
    {
        player = GameObject.Find("Player");
        fire = GetComponent<AudioSource>();
    }

    void Update()
    {
        fireEffect.SetActive(isFireActice);

        DistancePileWoodFire();
    }

    void DistancePileWoodFire()
    {
        if (!isFireActice) return;

        if (Vector3.Distance(fireEffect.transform.position, player.transform.position) <= _snapDistance)
        {
            StoryThermometerControl_Girl.isFireBeside = true;
            fire.Play();
            fire.volume = SettingControl.volumeBGM;
        }
        else
        {
            StoryThermometerControl_Girl.isFireBeside = false;
            fire.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(fireEffect.transform.position, _snapDistance);
    }
}
