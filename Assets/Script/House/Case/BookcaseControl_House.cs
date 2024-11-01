using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseControl_House : MonoBehaviour
{
    [Header("Book")]
    public GameObject[] book;
    public Transform[] bookMovePos;
    public Transform originalPoint;
    public static int _bookNum = 0;
    bool isOpen = false;
    bool isMove = false;
    float _moveSpeed = 2f;
    
    void Update()
    {
        OpenBookcase();
        NextStoryBook();
        MoveStoryBook();
        Leave();
    }

    void OpenBookcase()
    {
        if (!CameraControl_House.isLookBookcase) return;
        if (isOpen) return;

        isOpen = true;
        for (int b = 0; b < book.Length; b++)
        {
            if (b == _bookNum)
            {
                book[b].transform.position = bookMovePos[1].transform.position;
                book[b].transform.rotation = bookMovePos[1].transform.rotation;
            }
            else
            {
                book[b].transform.position = bookMovePos[0].transform.position;
                book[b].transform.rotation = bookMovePos[0].transform.rotation;
            }
        }
    }
    void NextStoryBook()
    {
        if (!CameraControl_House.isLookBookcase) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_bookNum != 0 && !isMove)
            {
                _bookNum--;
                isMove = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_bookNum < book.Length - 1 && !isMove)
            {
                _bookNum++;
                isMove = true;
            }
        }
    }
    void MoveStoryBook()
    {
        if (!CameraControl_House.isLookBookcase) return;
        if (!isMove) return;

        book[_bookNum].transform.position = Vector3.Lerp(book[_bookNum].transform.position, bookMovePos[1].transform.position, _moveSpeed * Time.deltaTime);
        book[_bookNum - 1].transform.position = Vector3.Lerp(book[_bookNum - 1].transform.position, bookMovePos[2].transform.position, _moveSpeed * Time.deltaTime);

        float dis = Vector3.Distance(book[_bookNum].transform.position, bookMovePos[1].transform.position);
        if (dis < 0.5f)
        {
            isMove = false;
            book[_bookNum - 1].transform.position = bookMovePos[0].transform.position;
        }
    }
   
    void Leave()
    {
        if (CameraControl_House.isLookBookcase)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveBookcase());
            }
        }
    }
    IEnumerator LeaveBookcase()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookBookcase = false;
        isOpen = false;
        for (int k = 0; k < book.Length; k++)
        {
            book[k].transform.position = originalPoint.transform.position;
            book[k].transform.rotation = originalPoint.transform.rotation;
        }
    }
}
