using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryInteractableControl : MonoBehaviour
{
    GameObject player;
    float snapDistance = 10f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        
    }
    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > snapDistance) return;
            
        print("Down!!");
    }
    void OnMouseUpAsButton()
    {
        print("UP");
    }
}
