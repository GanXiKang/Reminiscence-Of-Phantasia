using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryInteractableControl : MonoBehaviour
{
    GameObject player;
    float snapDistance = 1f;

    void Start()
    {
        player = GameObject.Find("Player");
        print(player.transform.position);
    }

    
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        
    }
    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= snapDistance)
        {
            print("Down!!");
        }
        else 
        {
            print("No");
        }
    }
    void OnMouseUpAsButton()
    {
        print("UP");
    }
}
