using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadPaperColor_Workbench : MonoBehaviour
{
    public Renderer[] canvasRenderers;
    public string loadDirectory;

    void Start()
    {
        loadDirectory = Application.persistentDataPath + "/Artwork.png";
        LoadTexture();
    }

    void LoadTexture()
    {
        for (int i = 0; i < canvasRenderers.Length; i++)
        {
            string filePath = Path.Combine(loadDirectory, $"Artwork_{i}.png");

            if (File.Exists(filePath))
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);
                canvasRenderers[i].material.mainTexture = texture;
                Debug.Log("Artwork loaded from " + filePath);
            }
            else
            {
                Debug.LogWarning("File not found: " + filePath);
            }
        }
    }
}
