using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookcaseControl_House : MonoBehaviour
{
    [Header("Book")]
    public GameObject[] book;
    public Transform[] bookMovePos;
    public Transform originalPoint;
    public static int _bookNum = 0;

    //Move
    bool isOpen = false;
    bool isNext = false;
    bool isBack = false;
    bool isForward = false;
    bool isBackward = false;
    float _moveSpeed = 10f;

    [Header("UI")]
    public GameObject bookUI;
    public GameObject bookButton;
    public GameObject bookContent;
    public GameObject contentInteractable;
    public Image image;
    public Sprite[] letter;
    
    void Update()
    {
        bookUI.SetActive(CameraControl_House.isLookBookcase);

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
        bookButton.SetActive(true);
        for (int b = 0; b < book.Length; b++)
        {
            if (b == _bookNum)
            {
                book[b].transform.position = bookMovePos[1].transform.position;
                book[b].transform.rotation = bookMovePos[1].transform.rotation;
            }
            else
            {
                if (b > _bookNum)
                {
                    book[b].transform.position = bookMovePos[0].transform.position;
                    book[b].transform.rotation = bookMovePos[0].transform.rotation;
                }
                else
                {
                    book[b].transform.position = bookMovePos[2].transform.position;
                    book[b].transform.rotation = bookMovePos[2].transform.rotation;
                }
            }
        }
    }
    void NextStoryBook()
    {
        if (!CameraControl_House.isLookBookcase) return;
        if (isNext || isBack || isForward || isBackward) return;

        if (Input.GetKeyDown(KeyCode.D)) //下一本
        {
            if (_bookNum < book.Length - 1)
            {
                _bookNum++;
                isNext = true;
                bookButton.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) //上一本
        {
            if (_bookNum != 0)
            {
                _bookNum--;
                isBack = true;
                bookButton.SetActive(false);
            }
        }
    }
    void MoveStoryBook()
    {
        if (!CameraControl_House.isLookBookcase) return;

        if (isNext)
        {
            book[_bookNum].transform.position = Vector3.MoveTowards(book[_bookNum].transform.position, bookMovePos[1].transform.position, _moveSpeed * Time.deltaTime);
            book[_bookNum - 1].transform.position = Vector3.MoveTowards(book[_bookNum - 1].transform.position, bookMovePos[2].transform.position, _moveSpeed * Time.deltaTime);

            if (book[_bookNum].transform.position == bookMovePos[1].transform.position)
            {
                isNext = false;
                bookButton.SetActive(true);
            }
        }
        else if (isBack)
        {
            book[_bookNum].transform.position = Vector3.MoveTowards(book[_bookNum].transform.position, bookMovePos[1].transform.position, _moveSpeed * Time.deltaTime);
            book[_bookNum + 1].transform.position = Vector3.MoveTowards(book[_bookNum + 1].transform.position, bookMovePos[0].transform.position, _moveSpeed * Time.deltaTime);

            if (book[_bookNum].transform.position == bookMovePos[1].transform.position)
            {
                isBack = false;
                bookButton.SetActive(true);
            }
        }
        else if (isForward)
        {
            book[_bookNum].transform.position = Vector3.MoveTowards(book[_bookNum].transform.position, bookMovePos[3].transform.position, _moveSpeed * Time.deltaTime);
            book[_bookNum].transform.rotation = Quaternion.Lerp(book[_bookNum].transform.rotation, bookMovePos[3].transform.rotation, _moveSpeed * Time.deltaTime);

            if (book[_bookNum].transform.position == bookMovePos[3].transform.position)
            {
                isForward = false;
                bookContent.SetActive(true);
                Button_ImageContent(1);
                StartCoroutine(AnimateReceiveAppear());
            }
        }
        else if (isBackward)
        {
            book[_bookNum].transform.position = Vector3.MoveTowards(book[_bookNum].transform.position, bookMovePos[1].transform.position, _moveSpeed * Time.deltaTime);
            book[_bookNum].transform.rotation = Quaternion.Lerp(book[_bookNum].transform.rotation, bookMovePos[1].transform.rotation, _moveSpeed * Time.deltaTime);

            if (book[_bookNum].transform.position == bookMovePos[1].transform.position)
            {
                isBackward = false;
                bookButton.SetActive(true);
            }
        }
    }

    public void Button_Book()
    {
        isForward = true;
        bookButton.SetActive(false);
    }
    public void Button_ImageContent(int letterNum)
    {
        switch (_bookNum)
        {
            case 0:
                image.sprite = letter[letterNum];
                break;

            case 1:
                image.sprite = letter[letterNum + 3];
                break;

            case 2:
                image.sprite = letter[letterNum + 6];
                break;

            case 3:
                image.sprite = letter[letterNum + 9];
                break;
        }
    }
    public void Button_Back()
    {
        isBackward = true;
        bookContent.SetActive(false);
    }

    IEnumerator AnimateReceiveAppear()
    {
        CanvasGroup canvasGroup = contentInteractable.GetComponent<CanvasGroup>();
        RectTransform rect = contentInteractable.GetComponent<RectTransform>();

        Vector3 startScale = new Vector3(0.7f, 0.7f, 1f);
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        float _duration = 0.6f;
        float _timeElapsed = 0f;
        canvasGroup.alpha = 0;
        rect.localScale = startScale;

        while (_timeElapsed < _duration)
        {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / _duration;
            rect.localScale = Vector3.Lerp(startScale, targetScale, t);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        canvasGroup.alpha = 1;
        rect.localScale = targetScale;
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
