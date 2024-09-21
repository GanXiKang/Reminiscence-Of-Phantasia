using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    GameObject player;
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
            StoryThermometerControl_Girl.isFirePB = false;
        }
        else
        {
            StoryThermometerControl_Girl.isFirePB = true;
        }
    }
}
