using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseControl_House : MonoBehaviour
{
    [Header("Book")]
    public GameObject[] book;
    public Transform[] bookPos;

    void Start()
    {
        
    }

    void Update()
    {
        Leave();
    }

    void Leave()
    {
        if (CameraControl_House.isLookBookcase)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveBookcase());
            }
        }
    }
    IEnumerator LeaveBookcase()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookBookcase = false;
    }
}
