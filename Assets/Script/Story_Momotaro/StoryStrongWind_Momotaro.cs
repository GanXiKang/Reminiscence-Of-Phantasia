using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStrongWind_Momotaro : MonoBehaviour
{
    public static bool isBlownAway = false;

    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        print("yes");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isBlownAway = false;
        }
    }
}
