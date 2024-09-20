using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    public GameObject fireEffect;
    public static bool isFireActice = false;
    public static bool isOutside = false;

    void Update()
    {
        fireEffect.SetActive(isFireActice);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isOutside= true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOutside = false;
        }
    }
}
