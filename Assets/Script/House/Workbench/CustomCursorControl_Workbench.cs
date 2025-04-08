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
    public Texture2D interactable1;
    public Texture2D interactable2;
    public Vector2 hotSpot = Vector2.zero; 
    bool isScissors = false;
    bool isClick = false;
    bool isAnim = false;

    [Header("Object")]
    public GameObject objectToMove;

    Texture2D currentCursorTexture = null;
    Coroutine scissorsRoutine;

    void Update()
    {
        if (CameraControl_House.isLookWorkbench)
        {
            switch (WorkbenchControl_House._process)
            {
                case 1:
                    Cursor.SetCursor(Input.GetMouseButton(0) ? stampB : stampA, hotSpot, CursorMode.Auto);
                    break;

                case 2:
                    if (!isScissors)
                    {
                        isScissors = true;
                        Cursor.SetCursor(scissors2, hotSpot, CursorMode.Auto);
                    }

                    if (ScissorsControl_Workbench.isUseScissors)
                    {
                        if (!isAnim)
                            scissorsRoutine = StartCoroutine(ScissorsAnimation());
                    }
                    else
                    {
                        if (scissorsRoutine != null)
                        {
                            StopCoroutine(scissorsRoutine);
                            scissorsRoutine = null;
                            isAnim = false;
                        }
                    }

                    MoveObjectWithMouse();
                    break;

                case 3:
                    Cursor.SetCursor(Input.GetMouseButton(0) ? pencil2 : pencil1, hotSpot, CursorMode.Auto);
                    break;

                case 4:
                    Cursor.SetCursor(Input.GetMouseButton(0) ? glue2 : glue1, hotSpot, CursorMode.Auto);
                    break;

                default:
                    isScissors = false;
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }
        else
        {
            if (isAnim)
            {
                StopCoroutine(scissorsRoutine);
                isAnim = false;
                isScissors = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isClick = true;
                Invoke("FalseisClick", 0.5f);
            }

            Texture2D targetCursor = isClick ? interactable2 : interactable1;

            if (currentCursorTexture != targetCursor)
            {
                Cursor.SetCursor(targetCursor, hotSpot, CursorMode.Auto);
                currentCursorTexture = targetCursor;
            }
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
    void FalseisClick()
    {
        isClick = false;
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
