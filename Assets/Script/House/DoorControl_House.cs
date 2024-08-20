using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl_House : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Leave();
    }

    void Leave()
    {
        if (CameraControl_House.isLookDoor)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveDoor());
            }
        }
    }
    IEnumerator LeaveDoor()
    {
        LoadingUIControl_House.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookDoor = false;
    }
}
