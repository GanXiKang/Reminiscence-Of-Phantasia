using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryColliderControl_Girl : MonoBehaviour
{
    public int _whatCollider;
    bool isOnce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
