using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorControl_Workbench : MonoBehaviour
{
    [Header("Texture")]
    public Texture2D stampA;
    public Texture2D stampB;
    public Texture2D scissors1;
    public Texture2D scissors2;
    public Texture2D pencil1;
    public Texture2D pencil2;
    public Texture2D glue1;
    public Texture2D glue2;
    public Vector2 hotSpot = Vector2.zero; 
    bool isStamp = false;
    bool isScissors = false;
    bool isPencil = false;
    bool isGlue = false;
    bool isAnim = false;

    [Header("Object")]
    public GameObject objectToMove;

    void Update()
    {
        if (CameraControl_House.isLookWorkbench)
        {
            switch (WorkbenchControl_House._process)
            {
                case 1:
                    if (!isStamp)
                    {
                        Cursor.SetCursor(stampA, hotSpot, CursorMode.Auto);
                        isStamp = true;
                    }
                    if (Input.GetMouseButton(0))
                        Cursor.SetCursor(stampB, hotSpot, CursorMode.Auto);
                    if (Input.GetMouseButtonUp(0))
                        Cursor.SetCursor(stampA, hotSpot, CursorMode.Auto);
                    break;

                case 2:
                    if (!isScissors)
                    {
                        Cursor.SetCursor(scissors2, hotSpot, CursorMode.Auto);
                        isScissors = true;
                    }
                    if (ScissorsControl_Workbench.isUseScissors)
                    {
                        if (!isAnim)
                        {
                            StartCoroutine(ScissorsAnimation());
                        }
                    }
                    else
                    {
                        StopCoroutine(ScissorsAnimation());
                        Cursor.SetCursor(scissors2, hotSpot, CursorMode.Auto);
                        isAnim = false;
                    }

                    MoveObjectWithMouse();
                    break;

                case 3:
                    if (!isPencil)
                    {
                        Cursor.SetCursor(pencil1, hotSpot, CursorMode.Auto);
                        isPencil = true;
                    }
                    if (Input.GetMouseButton(0))
                        Cursor.SetCursor(pencil2, hotSpot, CursorMode.Auto);
                    if (Input.GetMouseButtonUp(0))
                        Cursor.SetCursor(pencil1, hotSpot, CursorMode.Auto);
                    break;

                case 4:
                    if (!isGlue)
                    {
                        Cursor.SetCursor(glue1, hotSpot, CursorMode.Auto);
                        isGlue = true;
                    }
                    if (Input.GetMouseButton(0))
                        Cursor.SetCursor(glue2, hotSpot, CursorMode.Auto);
                    if (Input.GetMouseButtonUp(0))
                        Cursor.SetCursor(glue1, hotSpot, CursorMode.Auto);
                    break;

                default:
                    isStamp = false;
                    isScissors = false;
                    isPencil = false;
                    isGlue = false;
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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

    IEnumerator ScissorsAnimation()
    {
        isAnim = true;

        while (isScissors && ScissorsControl_Workbench.isUseScissors)
        {
            Cursor.SetCursor(scissors1, hotSpot, CursorMode.Auto);
            yield return new WaitForSeconds(0.4f);

            Cursor.SetCursor(scissors2, hotSpot, CursorMode.Auto);
            yield return new WaitForSeconds(0.4f);
        }

        isAnim = false;
    }
}
