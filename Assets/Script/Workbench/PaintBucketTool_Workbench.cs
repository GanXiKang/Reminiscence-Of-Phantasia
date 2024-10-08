using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintBucketTool_Workbench : MonoBehaviour
{
    Renderer canvasRenderer;
    public int _number = 0;
    public ColorPicker_Workbench colorPicker;
    private Texture2D texture;

    public Slider progressBar;

    void Start()
    {
        canvasRenderer = GetComponent<MeshRenderer>();
        Texture2D originalTexture = (Texture2D)canvasRenderer.material.mainTexture;
        texture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        //Material那邊Read/Write一定要開
        texture.SetPixels(originalTexture.GetPixels());
        texture.Apply();
        canvasRenderer.material.mainTexture = texture;
    }

    void Update()
    {
        if (WorkbenchControl_House._process != 3) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == canvasRenderer.transform)
                {
                    Vector2 uv = hit.textureCoord;
                    int x = Mathf.FloorToInt(uv.x * texture.width);
                    int y = Mathf.FloorToInt(uv.y * texture.height);

                    Color targetColor = texture.GetPixel(x, y);
                    Color replacementColor = colorPicker.selectedColor;
                    WorkbenchControl_House.isChangeColor[_number] = true;

                    FloodFill(texture, x, y, targetColor, replacementColor);
                }
            }
        }
    }

    void FloodFill(Texture2D texture, int x, int y, Color targetColor, Color replacementColor)
    {
        if (targetColor == replacementColor) return;
        if (texture.GetPixel(x, y) != targetColor) return;

        Queue<Vector2Int> pixels = new Queue<Vector2Int>();
        pixels.Enqueue(new Vector2Int(x, y));

        while (pixels.Count > 0)
        {
            Vector2Int currentPixel = pixels.Dequeue();
            int px = currentPixel.x;
            int py = currentPixel.y;

            if (texture.GetPixel(px, py) == targetColor)
            {
                texture.SetPixel(px, py, replacementColor);

                if (px > 0) pixels.Enqueue(new Vector2Int(px - 1, py));
                if (px < texture.width - 1) pixels.Enqueue(new Vector2Int(px + 1, py));
                if (py > 0) pixels.Enqueue(new Vector2Int(px, py - 1));
                if (py < texture.height - 1) pixels.Enqueue(new Vector2Int(px, py + 1));
            }
        }

        texture.Apply();
    }
}
