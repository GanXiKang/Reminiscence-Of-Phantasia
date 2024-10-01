using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePaperColor_Workbench : MonoBehaviour
{
    public Renderer[] canvasRenderers;
    public string saveDirectory;

    void Start()
    {
        saveDirectory = Application.persistentDataPath + "/Artwork.png";
    }

    public void SaveTexture()
    {
        WorkbenchControl_House.isClickSaveButton = true;
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        for (int i = 0; i < canvasRenderers.Length; i++)
        {
            Texture2D texture = (Texture2D)canvasRenderers[i].material.mainTexture;
            byte[] bytes = texture.EncodeToPNG();
            string filePath = Path.Combine(saveDirectory, $"Artwork_{i}.png");
            File.WriteAllBytes(filePath, bytes);
            Debug.Log("Artwork saved to " + filePath);
        }
    }
}
