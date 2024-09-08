using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboveObject_House : MonoBehaviour
{
    [Header("Workbench")]
    public Transform workbench;
    public RectTransform workbenchHint;
    public Vector3 workbenchOffset;

    [Header("Door")]
    public Transform door;
    public RectTransform doorHint;
    public Vector3 doorOffset;

    [Header("Bed")]
    public Transform bed;
    public RectTransform bedHint;
    public Vector3 bedOffset;

    [Header("Bookcase")]
    public Transform bookcase;
    public RectTransform bookcaseHint;
    public Vector3 bookcaseOffset;

    [Header("Showcase")]
    public Transform showcase;
    public RectTransform showcaseHint;
    public Vector3 showcaseOffset;

    [Header("DialogBox")]
    public GameObject dialogBox;
    public Text dialogText;
    public static bool isDialogBox;
    public Transform brid;
    public Transform cat;

    void Update()
    {
        Hint();
        Dialog();
    }

    void Hint()
    {
        Vector3 workbenchPos = workbench.position + workbenchOffset;
        workbenchHint.position = workbenchPos;

        Vector3 doorPos = door.position + doorOffset;
        doorHint.position = doorPos;

        Vector3 bedPos = bed.position + bedOffset;
        bedHint.position = bedPos;

        Vector3 bookcasePos = bookcase.position + bookcaseOffset;
        bookcaseHint.position = bookcasePos;

        Vector3 showcasePos = showcase.position + showcaseOffset;
        showcaseHint.position = showcasePos;
    }
    void Dialog()
    {
        
    }
}
