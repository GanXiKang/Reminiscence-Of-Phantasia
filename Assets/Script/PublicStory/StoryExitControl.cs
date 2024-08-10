using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryExitControl : MonoBehaviour
{
    public static int _goToThatScene;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("IN");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Exit");
        }
    }
}
