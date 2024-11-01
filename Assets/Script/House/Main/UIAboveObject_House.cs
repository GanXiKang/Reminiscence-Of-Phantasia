using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboveObject_House : MonoBehaviour
{
    [Header("Workbench")]
    public Transform workbench;
    public Vector3 workbenchOffset;

    [Header("Door")]
    public Transform door;
    public Vector3 doorOffset;

    [Header("Bed")]
    public Transform bed;
    public Vector3 bedOffset;

    [Header("Bookcase")]
    public Transform bookcase;
    public Vector3 bookcaseOffset;

    [Header("Showcase")]
    public Transform showcase;
    public Vector3 showcaseOffset;

    [Header("Hint")]
    public RectTransform hint;
    public Text hintName;

    void Update()
    {
        Hint();
    }

    void Hint()
    {
        Vector3 workbenchPos = workbench.position + workbenchOffset;
        hint.position = workbenchPos;
        hintName.text = "นคื๗ล_";

        //Vector3 doorPos = door.position + doorOffset;
        //doorHint.position = doorPos;

        //Vector3 bedPos = bed.position + bedOffset;
        //bedHint.position = bedPos;

        //Vector3 bookcasePos = bookcase.position + bookcaseOffset;
        //bookcaseHint.position = bookcasePos;

        //Vector3 showcasePos = showcase.position + showcaseOffset;
        //showcaseHint.position = showcasePos;
    }
}
