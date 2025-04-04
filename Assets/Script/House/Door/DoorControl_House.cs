using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl_House : MonoBehaviour
{
    [Header("LoadingVideo")]
    public GameObject loadingUI;
    public GameObject entrustVideo;
    public GameObject storeVideo;
    public static bool isLoading = false;
    public static bool isEntrust = false;
    public static bool isStore = false;

    [Header("Animals")]
    public GameObject bird;
    public GameObject cat;
    public static bool isBird = false;
    public static bool isCat = false;
    public static bool isLeave = false;

    void Update()
    {
        loadingUI.SetActive(isLoading);
        entrustVideo.SetActive(isEntrust);
        storeVideo.SetActive(isStore);
        bird.SetActive(isBird);
        cat.SetActive(isCat);

        Leave();
    }

    void Leave()
    {
        if (CameraControl_House.isLookDoor)
        {
            if (isLeave)
                StartCoroutine(LeaveDoor());
        }
    }
    IEnumerator LeaveDoor()
    {
        isLeave = false;
        BlackScreenControl.isOpenBlackScreen = true;
        StoreControl_House.isStoreActive = false;
        yield return new WaitForSeconds(1f);

        InteractableControl_House.isColliderActive[2] = UIAboveObject_House.isStoreHintActive;
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookDoor = false;
        UIAboveObject_House.isAboveDoor = false;
        isEntrust = false;
        isStore = false;
        isBird = false;
        isCat = false;

        if (!InteractableControl_House.isCatLeave)
        {
            isCat = true;
            CameraControl_House.isFreeLook = false;
            CameraControl_House.isLookDoorPlot = true;
            InteractableControl_House.isCatLeave = true;
            UIControl_House.isDialogue = true;
            DialogueControl_House.isCatTalk = true;
            DialogueControl_House._textCount = 25;
        }
        else if (!InteractableControl_House.isBirdEntrust)
        {
            isBird = true;
            CameraControl_House.isFreeLook = false;
            CameraControl_House.isLookDoorPlot = true;
            InteractableControl_House.isBirdEntrust = true;
            UIControl_House.isDialogue = true;
            DialogueControl_House.isBirdTalk = true;
            DialogueControl_House._textCount = 28;
        }
        else if (InteractableControl_House.isBirdLeave)
        {
            InteractableControl_House.isBirdLeave = false;
            UIControl_House.isDialogue = true;
            DialogueControl_House._textCount = 10;
        }
        else if (InteractableControl_House.isReadMomLetter)
        {
            isBird = true;
            CameraControl_House.isFreeLook = false;
            CameraControl_House.isLookDoorPlot = true;
            InteractableControl_House.isReadMomLetter = false;
            UIControl_House.isDialogue = true;
            DialogueControl_House.isBirdTalk = true;
            DialogueControl_House._textCount = 35;
        }
    }
}
