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
    public static bool isLoading = false;

    [Header("Animals")]
    public GameObject bird;
    //public GameObject cat;
    public static bool isBird = false;
    public static bool isCat = false;

    //ï∫ïr
    public static bool isDoorMesh = true;

    void Update()
    {
        loadingUI.SetActive(isLoading);
        door.GetComponent<MeshRenderer>().enabled = isDoorMesh;

        Leave();

        //úy‘á
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            isBird = true;
            isCat = false;
            bird.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isCat = true;
            isBird = false;
            //cat.SetActive(true);
        }
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
        isDoorMesh = true; //ï∫ïr
        isLeave = false;
        LoadingUIControl_House.isOpenBlackScreen = true;
        StoreControl_House.isStoreActive = false;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookDoor = false;
        bird.SetActive(false);
        //cat.SetActive(false);
    }
}
