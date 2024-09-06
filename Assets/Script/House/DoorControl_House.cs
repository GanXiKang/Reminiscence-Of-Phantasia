using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl_House : MonoBehaviour
{
    [Header("LoadingVideo")]
    public GameObject loadingUI;
    public static bool isLoading = false;
    public static bool isBrid = false;
    public static bool isCat = false;

    void Update()
    {
        loadingUI.SetActive(isLoading);

        Leave();
        //úy‘á
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            isBrid = true;
            isCat = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isCat = true;
            isBrid = false;
        }
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
        EntrustControl_House.isEntrustActive = false;
        StoreControl_House.isStoreActive = false;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookDoor = false;
    }
}
