using System.Collections;
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

    [Header("Showcase")]
    public Transform showcase;
    public Vector3 showcaseOffset;
    public static bool isAboveShowcase = false;

    [Header("Hint")]
    public GameObject hintObject;
    public RectTransform hint;
    public Text hintName;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        hintObject.SetActive(isHintActive());
        
        Hint();
    }

    bool isHintActive()
    {
        return isAboveWorkbench ||
               isAboveDoor ||
               isAboveBed ||
               isAboveBookcase ||
               isAboveShowcase;
    }

    void Hint()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, hint.transform.position.y, player.transform.position.z);
        hint.LookAt(targetPosition);

        if (isAboveWorkbench)
        {
            Vector3 workbenchPos = workbench.position + workbenchOffset;
            hint.position = workbenchPos;
            hintName.text = "工作臺";
        }
        else if (isAboveDoor)
        {
            Vector3 doorPos = door.position + doorOffset;
            hint.position = doorPos;
            hintName.text = "大門";
        }
        else if (isAboveBed)
        {
            Vector3 bedPos = bed.position + bedOffset;
            hint.position = bedPos;
            hintName.text = "睡床";
        }
        else if (isAboveBookcase)
        {
            Vector3 bookcasePos = bookcase.position + bookcaseOffset;
            hint.position = bookcasePos;
            hintName.text = "書架";
        }
        else if (isAboveShowcase)
        {
            Vector3 showcasePos = showcase.position + showcaseOffset;
            hint.position = showcasePos;
            hintName.text = "展示臺";
        }
    }
}
