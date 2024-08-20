using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedControl_House : MonoBehaviour
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
        if (CameraControl_House.isLookBed)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveBed());
            }
        }
    }
    IEnumerator LeaveBed()
    {
        LoadingUIControl_House.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookBed = false;
    }
}
