using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPileWood_Girl : MonoBehaviour
{
    SphereCollider sc;
    public GameObject fireEffect;
    public static bool isFireActice = false;

    void Start()
    {
        sc = GetComponent<SphereCollider>();
    }

    void Update()
    {
        fireEffect.SetActive(isFireActice);
        sc.enabled = isFireActice;
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
