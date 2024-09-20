using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    public GameObject fireEffect;
    public static bool isFireActice = false;
    public static bool isInPileWoodFire = false;

    void Update()
    {
        fireEffect.SetActive(isFireActice);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isInPileWoodFire = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInPileWoodFire = false;
        }
    }
}
