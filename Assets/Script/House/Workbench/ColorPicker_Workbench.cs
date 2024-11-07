using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker_Workbench : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick;

    public Color selectedColor; 

    public void SetColor(Image colorImage)
    {
        BGM.PlayOneShot(onClick);
        selectedColor = colorImage.color;
    }
}
