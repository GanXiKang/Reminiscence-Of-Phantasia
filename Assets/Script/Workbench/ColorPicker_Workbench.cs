using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker_Workbench : MonoBehaviour
{
    public Color selectedColor = Color.black; 

    public void SetColor(Image colorImage)
    {
        selectedColor = colorImage.color;
    }
}
