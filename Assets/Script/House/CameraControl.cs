using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("FreeLookCamera")]
    public GameObject freeLookCamera;
    public static bool isFreeLook;

    void Start()
    {
        isFreeLook = false;
        Invoke("StartFreeLookCamera", 2f);
    }

    void Update()
    {
        freeLookCamera.SetActive(isFreeLook);
    }

    void StartFreeLookCamera()
    {
        isFreeLook = true;
    }
}
