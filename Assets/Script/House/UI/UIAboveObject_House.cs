﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboveObject_House : MonoBehaviour
{
    GameObject player;

    [Header("Workbench")]
    public Transform workbench;
    public Vector3 workbenchOffset;
    public static bool isAboveWorkbench = false;

    [Header("Door")]
    public Transform door;
    public Vector3 doorOffset;
    public static bool isAboveDoor = false;

    [Header("Bed")]
    public Transform bed;
    public Vector3 bedOffset;
    public static bool isAboveBed = false;

    [Header("Bookcase")]
    public Transform bookcase;
    public Vector3 bookcaseOffset;
    public static bool isAboveBookcase = false;

    [Header("Hint")]
    public GameObject hint;
    public GameObject store;
    public Text hintName;
    public static bool isStoreHintActive = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (isHintActive())
            hint.SetActive(CameraControl_House.isFreeLook);
        else
            hint.SetActive(false);
        
        store.SetActive(isStoreHintAppear());

        Hint();
    }

    bool isHintActive()
    {
        return isAboveWorkbench ||
               (isAboveDoor && !isStoreHintActive) ||
               isAboveBed ||
               isAboveBookcase;
    }
    bool isStoreHintAppear()
    {
        return isStoreHintActive &&
               !CameraControl_House.isLookDoor &&
               !CameraControl_House.isLookDoorPlot;
    }

    void Hint()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, hint.GetComponent<RectTransform>().transform.position.y, player.transform.position.z);
        hint.GetComponent<RectTransform>().LookAt(targetPosition);

        if (isAboveWorkbench)
        {
            Vector3 workbenchPos = workbench.position + workbenchOffset;
            hint.GetComponent<RectTransform>().position = workbenchPos;
            hintName.text = "工作臺";
        }
        else if (isAboveDoor && !isStoreHintActive)
        {
            Vector3 doorPos = door.position + doorOffset;
            hint.GetComponent<RectTransform>().position = doorPos;
            hintName.text = "大門";
        }
        else if (isAboveBed)
        {
            Vector3 bedPos = bed.position + bedOffset;
            hint.GetComponent<RectTransform>().position = bedPos;
            hintName.text = "睡床";
        }
        else if (isAboveBookcase)
        {
            Vector3 bookcasePos = bookcase.position + bookcaseOffset;
            hint.GetComponent<RectTransform>().position = bookcasePos;
            hintName.text = "書櫃";
        }

        if (isStoreHintActive)
        {
            Vector3 storePosition = new Vector3(player.transform.position.x, store.GetComponent<RectTransform>().transform.position.y, player.transform.position.z);
            store.GetComponent<RectTransform>().LookAt(storePosition);

            Vector3 doorPos = door.position + doorOffset;
            store.GetComponent<RectTransform>().position = doorPos;
        }
    }
}
