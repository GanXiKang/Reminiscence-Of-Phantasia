using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl_House : MonoBehaviour
{
    [Header("FreeLookCamera")]
    public GameObject freeLookCamera;
    public static bool isFreeLook;

    [Header("CameraWorkbenchPosition")]
    public Transform startPos;
    public Transform workbenchPos;
    public static bool isLookWorkbench;
    float _moveTime = 1f;

    void Start()
    {
        isFreeLook = false;
        isLookWorkbench = false;
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
