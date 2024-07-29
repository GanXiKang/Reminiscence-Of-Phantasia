using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryInteractableControl : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        print(player.transform.position);
    }

    
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        print("Yes!!");
    }
    void OnMouseUp()
    {
        print("Try");
    }
}
