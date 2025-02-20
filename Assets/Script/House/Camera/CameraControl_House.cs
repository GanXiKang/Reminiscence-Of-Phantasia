using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl_House : MonoBehaviour
{
    GameObject player;

    [Header("FreeLookCamera")]
    public GameObject freeLookCamera;
    public static bool isFreeLook = false;

    [Header("InteractableCameraPosition")]
    public Transform workbenchPos;
    public Transform storyBookPos;
    public Transform playerPos;
    public Transform doorPos;
    public Transform bedPos;
    public Transform bedcasePos;
    public Transform bookcasePos;
    public Transform showcasePos;
    public static bool isLookWorkbench = false;
    public static bool isLookStorkBook = false;
    public static bool isLookPlayer = false;
    public static bool isLookDoor = false;
    public static bool isLookBed = false;
    public static bool isLookBedcase = false;
    public static bool isLookBookcase = false;
    public static bool isLookShowcase = false;

    [Header("PlotCameraPoint")]
    public Transform doorPlotPoint;

    [Header("CameraMovement")]
    public float _moveTime = 5f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        player.SetActive(isPlayerActive());
        freeLookCamera.SetActive(isFreeLookCamera());
        
        CameraLooking();
    }

    void CameraLooking()
    {
        if (isLookWorkbench)
        {
            if (!isLookStorkBook)
            {
                transform.position = Vector3.Lerp(transform.position, workbenchPos.position, _moveTime * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, workbenchPos.rotation, _moveTime * Time.deltaTime);
            }
            else
            {
                if (!isLookPlayer)
                {
                    transform.position = Vector3.Lerp(transform.position, storyBookPos.position, _moveTime * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(transform.rotation, storyBookPos.rotation, _moveTime * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, playerPos.position, _moveTime * 3 * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(transform.rotation, playerPos.rotation, _moveTime * 3 * Time.deltaTime);
                }
            }
        }
        else if (isLookDoor)
        {
            transform.position = Vector3.Lerp(transform.position, doorPos.position, _moveTime * Time.deltaTime); 
            transform.rotation = Quaternion.Lerp(transform.rotation, doorPos.rotation, _moveTime * Time.deltaTime);
        }
        else if (isLookBed)
        {
            if (!isLookBedcase)
            {
                transform.position = Vector3.Lerp(transform.position, bedPos.position, _moveTime * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, bedPos.rotation, _moveTime * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, bedcasePos.position, _moveTime / 2 * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, bedcasePos.rotation, _moveTime / 2 * Time.deltaTime);
            }
        }
        else if (isLookBookcase)
        {
            transform.position = Vector3.Lerp(transform.position, bookcasePos.position, _moveTime * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, bookcasePos.rotation, _moveTime * Time.deltaTime);
        }
        else if (isLookShowcase)
        {
            transform.position = Vector3.Lerp(transform.position, showcasePos.position, _moveTime * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, showcasePos.rotation, _moveTime * Time.deltaTime);
        }
    }

    bool isPlayerActive()
    {
        return !isLookDoor &&
               !isLookBookcase &&
               !isLookShowcase;
    }
    bool isFreeLookCamera()
    {
        return isFreeLook &&
               !SettingControl.isSettingActive;
    }
}
