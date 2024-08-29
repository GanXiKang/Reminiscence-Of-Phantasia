using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl_House : MonoBehaviour
{
    [Header("FreeLookCamera")]
    public GameObject freeLookCamera;
    public static bool isFreeLook = false;
    private CinemachineFreeLook Camera;

    [Header("CameraPosition")]
    public Transform workbenchPos;
    public Transform doorPos;
    public Transform bedPos;
    public Transform bookcasePos;
    public Transform showcasePos;
    public Transform renewPos;
    public static bool isLookWorkbench = false;
    public static bool isLookDoor = false;
    public static bool isLookBed = false;
    public static bool isLookBookcase = false;
    public static bool isLookShowcase = false;

    [Header("CameraMovement")]
    public float _moveTime = 5f;

    void Start()
    {
        Invoke("StartFreeLookCamera", 1.5f);
    }

    void Update()
    {
        freeLookCamera.SetActive(isFreeLookCamera());

        CameraLooking();
        RenewCameraPosition();
    }

    void StartFreeLookCamera()
    {
        isFreeLook = true;
    }
    void CameraLooking()
    {
        if (isLookWorkbench)
        {
            transform.position = Vector3.Lerp(transform.position, workbenchPos.position, _moveTime * Time.deltaTime); ;
            transform.rotation = Quaternion.Lerp(transform.rotation, workbenchPos.rotation, _moveTime * Time.deltaTime);
        }
        else if (isLookDoor)
        {
            transform.position = Vector3.Lerp(transform.position, doorPos.position, _moveTime * Time.deltaTime); ;
            transform.rotation = Quaternion.Lerp(transform.rotation, doorPos.rotation, _moveTime * Time.deltaTime);
        }
        else if (isLookBed)
        {
            transform.position = Vector3.Lerp(transform.position, bedPos.position, _moveTime * Time.deltaTime); ;
            transform.rotation = Quaternion.Lerp(transform.rotation, bedPos.rotation, _moveTime * Time.deltaTime);
        }
        else if (isLookBookcase)
        {
            transform.position = Vector3.Lerp(transform.position, bookcasePos.position, _moveTime * Time.deltaTime); ;
            transform.rotation = Quaternion.Lerp(transform.rotation, bookcasePos.rotation, _moveTime * Time.deltaTime);
        }
        else if (isLookShowcase)
        {
            transform.position = Vector3.Lerp(transform.position, showcasePos.position, _moveTime * Time.deltaTime); ;
            transform.rotation = Quaternion.Lerp(transform.rotation, showcasePos.rotation, _moveTime * Time.deltaTime);
        }
    }
    void RenewCameraPosition()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //isFreeLook = false;
            //transform.position = Vector3.Lerp(transform.position, doorPos.position, _moveTime * 2 * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, doorPos.rotation, _moveTime * 2 * Time.deltaTime);
            //Invoke("StartFreeLookCamera", 5f);

            Camera.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.5f;
            Vector3 directionToResetPoint = renewPos.position - freeLookCamera.transform.position;
            Vector3 cameraForward = transform.InverseTransformDirection(directionToResetPoint);
            float targetAngle = Mathf.Atan2(cameraForward.x, cameraForward.z) * Mathf.Rad2Deg;
            Camera.GetComponent<CinemachineFreeLook>().m_XAxis.Value = targetAngle;
        }
    }

    bool isFreeLookCamera()
    {
        return isFreeLook &&
               !SettingControl.isSettingActive;
    }
}
