using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableControl_House : MonoBehaviour
{
    GameObject player;
    
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip interact;

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

    [Header("ObjectCollider")]
    public GameObject[] objectCollider;
    public static bool[] isColliderActive = new bool[6];

    void Awake()
    {
        for (int c = 1; c < isColliderActive.Length; c++)
        {
            isColliderActive[c] = false;
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
        currentColor = hintF.color;
    }

    void Update()
    {
        InteractableButton_F();
        Interactable();
        ObjectCollider();
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
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookWorkbench = true;
                        break;

                    case 2:
                        DoorControl_House.isLoading = true;
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookDoor = true;
                        StartCoroutine(WhoIsVisit());
                        break;

                    case 3:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBed = true;
                        BedControl_House.isMovingToBed = true;
                        BedControl_House.isPutBedcase = true;
                        PlayerControl_House.isSleep = true;
                        break; 

                    case 4:
                        CameraControl_House.isFreeLook = false;
                        CameraControl_House.isLookBookcase = true;
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

    IEnumerator WhoIsVisit()
    {
        yield return new WaitForSeconds(2f);
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
