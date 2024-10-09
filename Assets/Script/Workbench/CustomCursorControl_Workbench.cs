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
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Camera.main.nearClipPlane + 5f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        objectToMove.transform.position = worldPosition;
    }
}
