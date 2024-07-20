using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboutObject_House : MonoBehaviour
{
    public Camera cam;
    public Transform workbench;
    public RectTransform hint;
    public Vector3 offset;

    void Update()
    {
        Vector3 Pos = workbench.position + offset;
        hint.position = Pos;
    }
}
