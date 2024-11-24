using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl_House : MonoBehaviour
{
    [Header("Door")]
    public GameObject door;
    public static bool isLeave = false;

    [Header("LoadingVideo")]
    public GameObject loadingUI;
    public GameObject entrustVideo;
    public GameObject storeVideo;
    public static bool isLoading = false;

    [Header("Animals")]
    public GameObject bird;
    public GameObject cat;
    public static bool isBird = false;
    public static bool isCat = false;

    void Update()
    {
        loadingUI.SetActive(isLoading);
        entrustVideo.SetActive(isBird);
        storeVideo.SetActive(isCat);
        
        Leave();
    }

    void Leave()
    {
        if (CameraControl_House.isLookDoor)
        {
            if (isLeave)
            {
                StartCoroutine(LeaveDoor());
            }
        }
    }
    IEnumerator LeaveDoor()
    {
        isLeave = false;
        BlackScreenControl.isOpenBlackScreen = true;
        StoreControl_House.isStoreActive = false;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookDoor = false;
        bird.SetActive(false);
        cat.SetActive(false);
    }
}
