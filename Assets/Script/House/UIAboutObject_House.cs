using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboutObject_House : MonoBehaviour
{
    [Header("Workbench")]
    public Transform workbench;
    public RectTransform workbenchHint;
    public Vector3 workbenchOffset;

    void Update()
    {
        Vector3 Pos = workbench.position + workbenchOffset;
        workbenchHint.position = Pos;
    }
}
