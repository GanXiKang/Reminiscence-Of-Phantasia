using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl_House : MonoBehaviour
{
    GameObject player;

    [Header("FreeLookCamera")]
    public GameObject freeLookCamera;
    public static bool isFreeLook = false;

    [Header("CameraPosition")]
    public Transform workbenchPos;
    public Transform storyBookPos;
    public Transform doorPos;
    public Transform bedPos;
    public Transform bookcasePos;
    public Transform showcasePos;
    public static bool isLookWorkbench = false;
    public static bool isLookStorkBook = false;
    public static bool isLookDoor = false;
    public static bool isLookBed = false;
    public static bool isLookBookcase = false;
    public static bool isLookShowcase = false;

    [Header("CameraMovement")]
    public float _moveTime = 5f;

    void Start()
    {
        player = GameObject.Find("Player");

        Invoke("StartFreeLookCamera", 1.5f);
    }

    void Update()
    {
        player.SetActive(isPlayerActive());
        freeLookCamera.SetActive(isFreeLookCamera());
        
        CameraLooking();
    }

    void StartFreeLookCamera()
    {
        isFreeLook = true;
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
                transform.position = Vector3.Lerp(transform.position, storyBookPos.position, _moveTime * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, storyBookPos.rotation, _moveTime * Time.deltaTime);
            }
        }
        else if (isLookDoor)
        {
            transform.position = Vector3.Lerp(transform.position, doorPos.position, _moveTime * Time.deltaTime); 
            transform.rotation = Quaternion.Lerp(transform.rotation, doorPos.rotation, _moveTime * Time.deltaTime);
        }
        else if (isLookBed)
        {
            transform.position = Vector3.Lerp(transform.position, bedPos.position, _moveTime * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, bedPos.rotation, _moveTime * Time.deltaTime);
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
               !isLookBookcase;
    }
    bool isFreeLookCamera()
    {
        return isFreeLook &&
               !SettingControl.isSettingActive;
    }
}