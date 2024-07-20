using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl_House : MonoBehaviour
{
    public int _serialNumber;
    public static int _nowNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InteractableControl_House.isInteractable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _nowNumber = _serialNumber;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InteractableControl_House.isInteractable = false;
        }
    }
}
