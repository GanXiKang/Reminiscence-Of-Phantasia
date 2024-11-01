using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePaperColor_Workbench : MonoBehaviour
{
    public Renderer[] canvasRenderers;
    public string saveDirectory;
    public static bool isSave = false;

    void Start()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, saveDirectory);
    }

    void Update()
    {
        if (!isSave) return;

        isSave = false;
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
