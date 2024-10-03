using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop_Workbench : MonoBehaviour
{
    BoxCollider boxC;

    private Vector3 offset;
    private bool isDragging = false;
    private bool isFixed = false;
    public Transform targetPosition;
    public float snapDistance = 1.0f;

    void Start()
    {
        boxC.GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (!isFixed)
        {
            if (isDragging)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
                transform.position = worldPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if (Vector3.Distance(transform.position, targetPosition.position) <= snapDistance)
                {
                    transform.position = targetPosition.position;
                    WorkbenchControl_House.isFinishStoryBook = true;
                    isFixed = true;
                }
            }
        }
        else 
        {
            transform.position = targetPosition.position;
        }
    }

    void OnMouseDown()
    {
        if (isFixed) return;

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            offset = transform.position - worldPosition;
        }
    }
}
