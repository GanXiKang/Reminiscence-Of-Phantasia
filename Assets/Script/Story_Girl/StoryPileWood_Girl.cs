using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    GameObject player;
    //SphereCollider sc;
    public GameObject fireEffect;
    public static bool isFireActice = false;
    float _snapDistance = 10f;

    void Start()
    {
        player = GameObject.Find("Player");
        //sc = GetComponent<SphereCollider>();
    }

    void Update()
    {
        fireEffect.SetActive(isFireActice);
        //sc.enabled = isFireActice;
        DistancePileWoodFire();
    }

    void DistancePileWoodFire()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _snapDistance)
        {
            StoryThermometerControl_Girl.isFirePB = false;
        }
        else
        {
            StoryThermometerControl_Girl.isFirePB = true;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
            
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
            
    //    }
    //}
}
