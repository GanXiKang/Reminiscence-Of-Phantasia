using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookcaseControl_House : MonoBehaviour
{
    [Header("Book")]
    public GameObject[] book;
    public Transform[] bookMovePos;
    bool isOpenBookcaseOnce = false;
    int _bookNum = 0;

    void Start()
    {
        
    }

    void Update()
    {
        MoveStoryBook();
        Leave();
    }

    void MoveStoryBook()
    {
        if (!CameraControl_House.isLookBookcase) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            _bookNum--;
            Limit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _bookNum++;
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
