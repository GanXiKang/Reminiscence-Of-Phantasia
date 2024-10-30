using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorControl_Workbench : MonoBehaviour
{
    public Texture2D scissors1, scissors2;
    public Texture2D pencil1, pencil2;
    public Vector2 hotSpot = Vector2.zero; 
    private bool isCursorChanged = false;  

    public GameObject objectToMove;

    void Update()
    {
        switch (WorkbenchControl_House._process)
        {
            case 2:
                float screenHalfWidth = Screen.width / 2;

                if (Input.mousePosition.x > screenHalfWidth)
                {
                    if (!isCursorChanged)
                    {
                        Cursor.SetCursor(scissors1, hotSpot, CursorMode.Auto);
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
                break;

            case 3:
                if (Input.GetMouseButtonDown(0))
                {
                    Cursor.SetCursor(pencil2, hotSpot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(pencil1, hotSpot, CursorMode.Auto);
                }
                break;

            default:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    void MoveObjectWithMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(objectToMove.transform.position).z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 newPosition = new Vector3(worldPosition.x, objectToMove.transform.position.y, worldPosition.z);

        objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, newPosition, Time.deltaTime * 10f);
    }
}
