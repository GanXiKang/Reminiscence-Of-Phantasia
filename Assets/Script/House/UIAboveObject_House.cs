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

    void Update()
    {
        Vector3 workbenchPos = workbench.position + workbenchOffset;
        workbenchHint.position = workbenchPos;
    }
}
