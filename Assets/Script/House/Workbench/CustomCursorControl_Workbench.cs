using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorControl_Workbench : MonoBehaviour
{
    [Header("Texture")]
    public Texture2D stamp1;
    public Texture2D stamp2;
    public Texture2D scissors1;
    public Texture2D scissors2;
    public Texture2D pencil1;
    public Texture2D pencil2;
    public Texture2D glue1;
    public Texture2D glue2;
    public Vector2 hotSpot = Vector2.zero; 
    private bool isCursorChanged = false;
    bool isUse = false;
    bool isAnim = false;

    [Header("Object")]
    public GameObject objectToMove;

    void Update()
    {
        switch (WorkbenchControl_House._process)
        {
            case 1:
                if (isUse)
                {
                    Cursor.SetCursor(stamp2, hotSpot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(stamp1, hotSpot, CursorMode.Auto);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    isUse = true;
                    Invoke("FalseisUse", 0.5f);
                }
                break;

            case 2:
                float screenHalfWidth = Screen.width / 2;

                if (Input.mousePosition.x > screenHalfWidth)
                {
                    if (!isCursorChanged)
                    {
                        Cursor.SetCursor(scissors2, hotSpot, CursorMode.Auto);
                        isCursorChanged = true;
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
                if (isUse)
                {
                    Cursor.SetCursor(pencil2, hotSpot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(pencil1, hotSpot, CursorMode.Auto);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    isUse = true;
                    Invoke("FalseisUsePencil", 0.5f);
                }
                break;

            case 4:
                if (isUse)
                {
                    Cursor.SetCursor(glue2, hotSpot, CursorMode.Auto);
                }
                else
                {
                    Cursor.SetCursor(glue1, hotSpot, CursorMode.Auto);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    isUse = true;
                    Invoke("FalseisUsePencil", 0.5f);
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
    void FalseisUse()
    {
        isUse = false;
    }

    IEnumerator ScissorsAnimation()
    {
        isAnim = true;

        while (isCursorChanged && ScissorsControl_Workbench.isUseScissors)
        {
            Cursor.SetCursor(scissors1, hotSpot, CursorMode.Auto);
            yield return new WaitForSeconds(0.4f);

            Cursor.SetCursor(scissors2, hotSpot, CursorMode.Auto);
            yield return new WaitForSeconds(0.4f);
        }

        isAnim = false;
    }
}
