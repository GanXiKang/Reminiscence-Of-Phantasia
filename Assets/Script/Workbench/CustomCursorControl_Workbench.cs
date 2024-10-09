using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorControl_Workbench : MonoBehaviour
{
    public Texture2D customCursorTexture; 
    public Vector2 hotSpot = Vector2.zero; 
    private bool isCursorChanged = false;  

    public GameObject objectToMove;

    void Update()
    {
        if (WorkbenchControl_House._process != 2) return;

        float screenHalfWidth = Screen.width / 2;

        if (Input.mousePosition.x > screenHalfWidth)
        {
            if (!isCursorChanged)
            {
                Cursor.SetCursor(customCursorTexture, hotSpot, CursorMode.Auto);
                isCursorChanged = true;
            }

            MoveObjectWithMouse();
        }
        else
        {
            if (isCursorChanged)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isCursorChanged = false;
            }
        }
    }

    void MoveObjectWithMouse()
    {
        //Vector3 mousePosition = Input.mousePosition;

        //mousePosition.z = Camera.main.nearClipPlane + 5f;
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //objectToMove.transform.position = worldPosition;

        Vector3 mousePosition = Input.mousePosition;

        // 使用物體的 Z 值來設定 Z 軸距離
        mousePosition.z = Camera.main.WorldToScreenPoint(objectToMove.transform.position).z;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 只跟隨 X 和 Z，不影響 Y 軸
        Vector3 newPosition = new Vector3(worldPosition.x, objectToMove.transform.position.y, worldPosition.z);

        // 平滑移動
        objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, newPosition, Time.deltaTime * 10f);
    }
}
