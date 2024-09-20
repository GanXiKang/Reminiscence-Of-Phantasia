using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    public GameObject fireEffect;
    public static bool isFireActice = true; //test

    void Update()
    {
        fireEffect.SetActive(isFireActice);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            StoryThermometerControl_Girl.isFirePB = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StoryThermometerControl_Girl.isFirePB = false;
        }
    }
}
