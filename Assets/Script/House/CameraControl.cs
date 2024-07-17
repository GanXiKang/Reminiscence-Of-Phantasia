using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("FreeLookCamera")]
    public GameObject freeLookCamera;
    public static bool isFreeLook;

    [Header("CameraPosition")]
    public Transform startPos;
    public Transform workbenchPos;
    public static bool isLookWorkbench;
    float _moveTime = 1f;
    bool once = false;

    void Start()
    {
        isFreeLook = false;
        isLookWorkbench = false;
        Invoke("StartFreeLookCamera", 2f);
    }

    void Update()
    {
        freeLookCamera.SetActive(isFreeLook);

        if (isLookWorkbench)
        {
            once = true;
            transform.position = Vector3.Lerp(transform.position, workbenchPos.position, _moveTime * Time.deltaTime); ;
            transform.rotation = Quaternion.Lerp(transform.rotation, workbenchPos.rotation, _moveTime * Time.deltaTime);
        }
        else
        {
            if (once)
            {
                transform.position = startPos.position;
                transform.rotation = startPos.rotation;
            }
        }
    }

    void StartFreeLookCamera()
    {
        isFreeLook = true;
    }
}
