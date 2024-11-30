using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop_Workbench : MonoBehaviour
{
    BoxCollider boxC;

    [Header("Target")]
    public Transform targetPosition;
    public Transform paperDropPoint;
    float snapDistance = 0.5f;

    private Vector3 offset;
    private bool isDragging = false;
    private bool isFixed = false;

    [Header("Effects")]
    public GameObject starEF;

    void Start()
    {
        boxC = GetComponent<BoxCollider>();
    }

    void Update()
    {
        BoxColliderEnable();
        Fixed();
    }

    void BoxColliderEnable()
    {
        if (WorkbenchControl_House._process == 4)
        {
            boxC.enabled = true;
        }
        else
        {
            boxC.enabled = false;
        }
    }
    void Fixed()
    {
        if (WorkbenchControl_House._process != 4) return;

        if (!isFixed)
        {
            if (isDragging)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
                transform.position = worldPosition;
            }
            else
            {
                transform.position = paperDropPoint.position;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if (Vector3.Distance(transform.position, targetPosition.position) <= snapDistance)
                {
                    transform.position = targetPosition.position;
                    transform.rotation = targetPosition.rotation;
                    WorkbenchControl_House.isFinishStoryBook = true;
                    starEF.SetActive(true);
                    isFixed = true;
                }
            }
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
