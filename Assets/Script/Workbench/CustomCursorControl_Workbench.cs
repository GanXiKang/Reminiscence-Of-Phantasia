using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorControl_Workbench : MonoBehaviour
{
    public Texture2D customCursorTexture;  // 自定義光標圖像
    public Vector2 hotSpot = Vector2.zero; // 自定義光標的熱點
    private bool isCursorChanged = false;  // 標記是否已更換光標

    public GameObject objectToMove;
    Rigidbody rb;

    void Start()
    {
        rb = objectToMove.GetComponent<Rigidbody>();
    }

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
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane + 5f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 保持 Y 軸位置不變
        Vector3 newPosition = new Vector3(worldPosition.x, objectToMove.transform.position.y, worldPosition.z);

        // 使用 Rigidbody 的 MovePosition 方法
        rb.MovePosition(newPosition);
    }
}
