using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase_House : MonoBehaviour
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
        if (CameraControl_House.isLookShowcase)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveShowcase());
            }
        }
    }
    IEnumerator LeaveShowcase()
    {
        LoadingUIControl_House.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookShowcase = false;
    }
}
