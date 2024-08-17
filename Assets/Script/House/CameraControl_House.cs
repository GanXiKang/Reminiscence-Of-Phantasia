using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl_House : MonoBehaviour
{
    [Header("FreeLookCamera")]
    public GameObject freeLookCamera;
    public static bool isFreeLook = false;

    [Header("CameraPosition")]
    public Transform workbenchPos;
    public Transform doorPos;
    public Transform bedPos;
    public Transform bookcasePos;
    public Transform showcasePos;
    public static bool isLookWorkbench = false;
    public static bool isLookDoor = false;
    public static bool isLookBed = false;
    public static bool isLookBookcase = false;
    public static bool isLookShowcase = false;

    [Header("CameraMovement")]
    public Transform startPos;
    float _moveTime = 5f;

    void Start()
    {
        Invoke("StartFreeLookCamera", 1.5f);
    }

    void Update()
    {
        freeLookCamera.SetActive(isFreeLook);

        LookWorkbench();
    }

    void StartFreeLookCamera()
    {
        isFreeLook = true;
    }
    void LookWorkbench()
    {
        if (isLookWorkbench)
        {
            transform.position = Vector3.Lerp(transform.position, workbenchPos.position, _moveTime * Time.deltaTime); ;
            transform.rotation = Quaternion.Lerp(transform.rotation, workbenchPos.rotation, _moveTime * Time.deltaTime);
        }
    }
}
