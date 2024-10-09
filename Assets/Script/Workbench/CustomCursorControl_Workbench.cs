using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorControl_Workbench : MonoBehaviour
{
    public Texture2D customCursorTexture;  // �Զ��x��ˈD��
    public Vector2 hotSpot = Vector2.zero; // �Զ��x��˵ğ��c
    private bool isCursorChanged = false;  // ��ӛ�Ƿ��Ѹ��Q���

    public GameObject objectToMove;
    Rigidbody rb;

    void Start()
    {
        rb = objectToMove.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ȡ��ΞĻ���ȵ�һ��
        float screenHalfWidth = Screen.width / 2;

        // �Д໬���Ƿ����Ұ�߅
        if (Input.mousePosition.x > screenHalfWidth)
        {
            // �������λ��Ұ�߅�ҹ��δ�����Q���t���Q���Զ��x���
            if (!isCursorChanged)
            {
                Cursor.SetCursor(customCursorTexture, hotSpot, CursorMode.Auto);
                isCursorChanged = true;
            }

            MoveObjectWithMouse();
        }
        else
        {
            // �������λ����߅�ҹ���ѱ����Q���t߀ԭ��Ĭ�J���
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

        // ���� Y �Sλ�ò�׃
        Vector3 newPosition = new Vector3(worldPosition.x, objectToMove.transform.position.y, worldPosition.z);

        // ʹ�� Rigidbody �� MovePosition ����
        rb.MovePosition(newPosition);
    }
}
