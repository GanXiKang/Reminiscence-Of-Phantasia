using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker_Workbench : MonoBehaviour
{
    public Color selectedColor = Color.red; 

    public void SetColor(Image colorImage)
    {
        selectedColor = colorImage.color;
    }
}
