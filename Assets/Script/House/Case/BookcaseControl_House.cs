using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookcaseControl_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick, putOut, putBack, display;

    [Header("Book")]
    public GameObject[] book;
    public Transform[] bookMovePos;
    public Transform[] originalPoint;
    public static bool[] bookActive;
    public static bool isNewBookActive = false;
    public static int _bookActiveNum = 0;
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
    public GameObject[] bookButton;
    public GameObject bookContent;
    public GameObject contentInteractable;
    public Image image;
    public Sprite[] letter;

    //Plot
    bool isEntrustCome = true;

    void Start()
    {
        bookActive = new bool[book.Length];
    }

    void Update()
    {
        bookUI.SetActive(CameraControl_House.isLookBookcase);

        BookActive();
        OpenBookcase();
        NextStoryBook();
        MoveStoryBook();
    }

    void BookActive()
    {
        if (!isNewBookActive) return;

        isNewBookActive = false;
        for (int a = 0; a < bookActive.Length; a++)
        {
            if (bookActive[a])
            {
                _bookActiveNum++;
                book[a].SetActive(true);
            }
        }
    }
    void OpenBookcase()
    {
        if (!CameraControl_House.isLookBookcase) return;
        if (isOpen) return;

        isOpen = true;
        bookContent.SetActive(false);
        bookButton[0].SetActive(true);
        bookButton[1].SetActive(GameControl_House._storyNum > 0);
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
        if (bookContent.activeSelf) return;

        bookButton[2].SetActive(_bookNum != 0);
        bookButton[3].SetActive(_bookNum != _bookActiveNum - 1);

        if (GameControl_House._storyNum > 0)
        {
            if (Input.GetKeyDown(KeyCode.A)) //上一本
            {
                if (_bookNum != 0)
                {
                    BGM.PlayOneShot(onClick);
                    _bookNum--;
                    isBack = true;
                    bookButton[1].SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.D)) //下一本
            {
                if (_bookNum < book.Length - 1)
                {
                    BGM.PlayOneShot(onClick);
                    _bookNum++;
                    isNext = true;
                    bookButton[1].SetActive(false);
                }
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
                bookButton[1].SetActive(true);
            }
        }
        else if (isBack)
        {
            book[_bookNum].transform.position = Vector3.MoveTowards(book[_bookNum].transform.position, bookMovePos[1].transform.position, _moveSpeed * Time.deltaTime);
            book[_bookNum + 1].transform.position = Vector3.MoveTowards(book[_bookNum + 1].transform.position, bookMovePos[0].transform.position, _moveSpeed * Time.deltaTime);

            if (book[_bookNum].transform.position == bookMovePos[1].transform.position)
            {
                isBack = false;
                bookButton[1].SetActive(true);
            }
        }
        else if (isForward)
        {
            book[_bookNum].transform.position = Vector3.MoveTowards(book[_bookNum].transform.position, bookMovePos[3].transform.position, _moveSpeed / 2 * Time.deltaTime);
            book[_bookNum].transform.rotation = Quaternion.Lerp(book[_bookNum].transform.rotation, bookMovePos[3].transform.rotation, _moveSpeed / 2 * Time.deltaTime);

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
            book[_bookNum].transform.position = Vector3.MoveTowards(book[_bookNum].transform.position, bookMovePos[1].transform.position, _moveSpeed / 2 * Time.deltaTime);
            book[_bookNum].transform.rotation = Quaternion.Lerp(book[_bookNum].transform.rotation, bookMovePos[1].transform.rotation, _moveSpeed / 2 * Time.deltaTime);

            if (book[_bookNum].transform.position == bookMovePos[1].transform.position)
            {
                isBackward = false;
                bookButton[0].SetActive(true);
            }
        }
    }

    public void Button_Book()
    {
        BGM.PlayOneShot(putOut);
        isForward = true;
        bookButton[0].SetActive(false);
    }
    public void Button_Page(string key)
    {
        if (isNext || isBack || isForward || isBackward) return;

        if (key == "a")
        {
            if (_bookNum != 0)
            {
                BGM.PlayOneShot(onClick);
                _bookNum--;
                isBack = true;
                bookButton[1].SetActive(false);
            }
        }
        else if (key == "d")
        {
            if (_bookNum < _bookActiveNum - 1)
            {
                BGM.PlayOneShot(onClick);
                _bookNum++;
                isNext = true;
                bookButton[1].SetActive(false);
            }
        }
    }
    public void Button_ImageContent(int letterNum)
    {
        BGM.PlayOneShot(display);
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
        BGM.PlayOneShot(putBack);
        isBackward = true;
        bookContent.SetActive(false);
    }
    public void Button_Leave()
    {
        BGM.PlayOneShot(onClick);

        if (!UIControl_House.isDialogue)
            StartCoroutine(LeaveBookcase());
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
    IEnumerator LeaveBookcase()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookBookcase = false;
        isOpen = false;
        for (int k = 0; k < book.Length; k++)
        {
            book[k].transform.position = originalPoint[k].transform.position;
            book[k].transform.rotation = originalPoint[k].transform.rotation;
        }
        if (GameControl_House._day > 2 && isEntrustCome)
        {
            isEntrustCome = false;
            UIControl_House.isDialogue = true;
            DialogueControl_House._textCount = 19;
        }
    }
}
