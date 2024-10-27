using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableControl_House : MonoBehaviour
{
    [Header("InteractableUI")]
    public GameObject interactableUI;
    public Image hintF;
    Color currentColor;
    float _alpha = 0f;
    public float _screenSpeed = 3f;
    public static bool isInteractable = false;

    [Header("Animals")]
    public GameObject bird;
    public GameObject cat;

    void Start()
    {
        currentColor = hintF.color;
    }

    void Update()
    {
        InteractableButton_F();
        Interactable();
    }

    void InteractableButton_F()
    {
        interactableUI.SetActive(isInteractable);
        currentColor.a = _alpha;
        hintF.color = currentColor;
    }
    void AppearInteractableHint()
    {
        if (_alpha < 1)
        {
            _alpha += _screenSpeed * Time.deltaTime;
        }
    }
    void Interactable()
    {
        if (isInteractable)
        {
            AppearInteractableHint();
            if (Input.GetKeyDown(KeyCode.F))
            {
                isInteractable = false;
                switch (ColliderControl_House._nowNumber)
                {
                    case 1:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookWorkbench = true;
                        break;  //����̨

                    case 2:
                        DoorControl_House.isLoading = true;
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookDoor = true;
                        StartCoroutine(WhoIsVisit());
                        break;  //�T

                    case 3:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBed = true;
                        BedControl_House.isGoStoryWorld = true;
                        BedControl_House._storyNum = 0; //�F�ڹ���һ��0 
                        break;  //��

                    case 4:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBookcase = true;
                        break;  //����

                    case 5:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookShowcase = true;
                        break;  //չʾ��
                }
            }
        }
        else 
        {
            _alpha = 0;
        }
    }

    IEnumerator WhoIsVisit()
    {
        yield return new WaitForSeconds(3f);
        DoorControl_House.isLoading = false;
        if (DoorControl_House.isBird)
        {
            bird.SetActive(true);

            UIControl_House.isDialogue = true;
            DialogueControl_House.isBird = true;
            DialogueControl_House._textCount = 35;

            yield return new WaitForSeconds(2f);

            BirdControl_House.isIdle = true;
            BirdControl_House.isDeliver = true;

            EntrustControl_House.isEntrustActive = true;
            DialogueControl_House.isAutoNext = true;
            DialogueControl_House._paragraph = 2;
        }
        else if (DoorControl_House.isCat)
        {
            cat.SetActive(true);
            CatControl_House.isWave = true;

            UIControl_House.isDialogue = true;
            DialogueControl_House.isBird = false;
            DialogueControl_House._textCount = 42;

            yield return new WaitForSeconds(2f);

            CatControl_House.isWave = false;

            StoreControl_House.isStoreActive = true;
            DialogueControl_House.isAutoNext = true;
            DialogueControl_House._paragraph = 2;
        }
    }
}
