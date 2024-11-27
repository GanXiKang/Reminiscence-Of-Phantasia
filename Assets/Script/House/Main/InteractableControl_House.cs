using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableControl_House : MonoBehaviour
{    
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip interact;

    [Header("InteractableUI")]
    public GameObject interactableUI;
    public Image hintF;
    Color currentColor;
    float _alpha = 0f;
    int _eventNum = 0;
    public float _screenSpeed = 3f;
    public static bool isInteractable = false;

    [Header("ObjectCollider")]
    public GameObject[] objectCollider;
    public static bool[] isColliderActive = new bool[6];

    //Plot
    public static bool isCatSeeWorkbench = false;
    public static bool isCatLeave = false;
    public static bool isBirdDoorBell = false;
    public static bool isBirdEntrust = false;
    public static bool isBirdSeeBed = false;

    void Awake()
    {
        for (int c = 1; c < isColliderActive.Length; c++)
        {
            isColliderActive[c] = false;
        }
    }

    void Start()
    {
        currentColor = hintF.color;
    }

    void Update()
    {
        InteractableButton_F();
        Interactable();
        ObjectCollider();
        Plot();
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
                BGM.PlayOneShot(interact);
                isInteractable = false;
                switch (ColliderControl_House._nowNumber)
                {
                    case 1:
                        isColliderActive[1] = false;
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookWorkbench = true;
                        if (GameControl_House._storyNum == 0)
                        {
                            UIControl_House.isDialogue = true;
                            DialogueControl_House.isAutoPlot = true;
                            DialogueControl_House._textCount = 3;
                        }
                        break;

                    case 2:
                        isColliderActive[2] = false;
                        if (DoorControl_House.isEntrust || DoorControl_House.isStore)
                        {
                            DoorControl_House.isLoading = true;
                            CameraControl_House.isFreeLook = false;
                            CameraControl_House.isLookDoor = true;
                            StartCoroutine(WhoIsVisit());
                        }
                        else
                        {
                            if (!isCatLeave)
                            {
                                isColliderActive[2] = false;
                                _eventNum = 1;
                                BlackScreenControl.isOpenBlackScreen = true;
                                Invoke("WaitEvent", 1f);
                            }
                            else
                            {
                                _eventNum = 3;
                                BlackScreenControl.isOpenBlackScreen = true;
                                Invoke("WaitEvent", 1f);
                            }
                        }
                        break;

                    case 3:
                        isColliderActive[3] = false;
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBed = true;
                        BedControl_House.isMovingToBed = true;
                        BedControl_House.isPutBedcase = true;
                        PlayerControl_House.isSleep = true;
                        break; 

                    case 4:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBookcase = true;
                        if (GameControl_House._storyNum == 0)
                        {
                            UIControl_House.isDialogue = true;
                            DialogueControl_House.isCatTalk = true;
                            DialogueControl_House._textCount = 23;
                        }
                        break;

                    case 5:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookShowcase = true;
                        break;
                }
            }
        }
        else 
        {
            _alpha = 0;
        }
    }
    void ObjectCollider()
    {
        for (int c = 1; c < isColliderActive.Length; c++)
        {
            objectCollider[c].SetActive(isColliderActive[c]);
        }
    }  
    void Plot()
    {
        if (isBirdDoorBell)
        {
            _eventNum = 2;
            Invoke("WaitEvent", 5f);
            isBirdDoorBell = false;
        }
    }

    IEnumerator WhoIsVisit()
    {
        yield return new WaitForSeconds(2f);
        DoorControl_House.isLoading = false;
        if (DoorControl_House.isEntrust)
        {
            DoorControl_House.isBird = true;

            UIControl_House.isDialogue = true;
            DialogueControl_House.isBirdTalk = true;
            DialogueControl_House._textCount = 38;

            yield return new WaitForSeconds(2f);

            BirdControl_House.isIdle = true;
            BirdControl_House.isDeliver = true;

            EntrustControl_House.isEntrustActive = true;
            DialogueControl_House.isAutoNext = true;
            DialogueControl_House._paragraph = 2;
        }
        else if (DoorControl_House.isStore)
        {
            DoorControl_House.isCat = true;
            CatControl_House.isWave = true;

            UIControl_House.isDialogue = true;
            DialogueControl_House.isCatTalk = true;
            DialogueControl_House._textCount = 26;

            yield return new WaitForSeconds(2f);

            CatControl_House.isWave = false;

            StoreControl_House.isStoreActive = true;
            DialogueControl_House.isAutoNext = true;
            DialogueControl_House._paragraph = 2;
        }
    }

    void WaitEvent()
    {
        switch (_eventNum)
        {
            case 1:
                DoorControl_House.isCat = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House.isCatTalk = true;
                DialogueControl_House._textCount = 20;
                break;

            case 2:
                UIControl_House.isDialogue = true;
                DialogueControl_House._textCount = 9;
                break;

            case 3:
                DoorControl_House.isBird = true;
                BirdControl_House.isIdle = true;
                UIControl_House.isDialogue = true;
                DialogueControl_House.isBirdTalk = true;
                DialogueControl_House._textCount = 27;
                break;
        }
    }
}
