using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseControl_House : MonoBehaviour
{
    [Header("Book")]
    public GameObject[] book;
    public Transform[] bookMovePos;
    public static int _bookNum = 0;
    bool isOpen = false;
    
    void Update()
    {
        OpenBookcase();
        NextStoryBook();
        Leave();
    }

    void OpenBookcase()
    {
        if (!CameraControl_House.isLookBookcase) return;
        if (isOpen) return;

        if()
        book[_bookNum].transform.position = bookMovePos[1].transform.position;
        book[_bookNum].transform.rotation = bookMovePos[1].transform.rotation;
    }
    void MoveStoryBook()
    {
        
    }
    void NextStoryBook()
    {
        if (!CameraControl_House.isLookBookcase) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            _bookNum--;
            MoveStoryBook();
            Limit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _bookNum++;
            MoveStoryBook();
            Limit();
        }
    }
    void Limit()
    {
        if (_bookNum < 0)
        {
            _bookNum = 0;
        }
        if (_bookNum >= book.Length)
        {
            _bookNum = book.Length - 1;
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
    }
}
