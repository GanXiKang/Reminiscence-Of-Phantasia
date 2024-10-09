using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorControl_Workbench : MonoBehaviour
{
    public Texture2D customCursorTexture;  // 自定義光標圖像
    public Vector2 hotSpot = Vector2.zero; // 自定義光標的熱點
    private bool isCursorChanged = false;  // 標記是否已更換光標

    public GameObject objectToMove;

    void Update()
    {
        // 取得螢幕寬度的一半
        float screenHalfWidth = Screen.width / 2;

        // 判斷滑鼠是否在右半邊
        if (Input.mousePosition.x > screenHalfWidth)
        {
            // 如果滑鼠位於右半邊且光標未被更換，則更換為自定義光標
            if (!isCursorChanged)
            {
                Cursor.SetCursor(customCursorTexture, hotSpot, CursorMode.Auto);
                isCursorChanged = true;
            }

            MoveObjectWithMouse();
        }
        else
        {
            // 如果滑鼠位於左半邊且光標已被更換，則還原為默認光標
            if (isCursorChanged)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isCursorChanged = false;
            }
        }
    }

    void MoveObjectWithMouse()
    {
        // 取得滑鼠當前位置
        Vector3 mousePosition = Input.mousePosition;

        // 將滑鼠位置轉換為世界座標，並指定Z軸距離
        mousePosition.z = Camera.main.nearClipPlane + 5f;  // 這裡設定Z軸距離，根據場景需要調整
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 將3D物件移動到滑鼠的世界座標位置
        objectToMove.transform.position = worldPosition;
    }
}
