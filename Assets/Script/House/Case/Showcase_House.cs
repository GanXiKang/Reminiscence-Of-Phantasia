using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase_House : MonoBehaviour
{
    [Header("StoryBook")]
    public GameObject[] storyBook;
    public Transform showPoint;
    public Transform originalPoint;
    int _showNum = 0;
    bool isShow = false;

    [Header("StoryBookPoint")]
    public GameObject[] storyBookPoint;
    public static int[] _storyBookPaperNum = new int[5];

    [Header("Paper")]
    public GameObject[] paper;

    void Start()
    {
        _storyBookPaperNum[0] = 0;
        _storyBookPaperNum[1] = 1;
        _storyBookPaperNum[2] = 4;
        _storyBookPaperNum[3] = 7;
        _storyBookPaperNum[4] = 8;
    }

    void Update()
    {
        FirstShowcase();
        ChangeStoryBook();
        Leave();
    }

    void FirstShowcase()
    {
        if (!CameraControl_House.isLookShowcase) return;
        if (isShow) return;

        StoryBookShow();
        isShow = true;
    }
    void StoryBookShow()
    {
        for (int i = 0; i < storyBook.Length; i++)
        {
            if (i == _showNum)
            {
                storyBook[i].transform.position = showPoint.position;
                storyBook[i].transform.rotation = showPoint.rotation;
                storyBook[i].GetComponent<Animator>().SetBool("isOpen", true);
                paper[_storyBookPaperNum[_showNum]].transform.position = storyBookPoint[i].transform.position;
                paper[_storyBookPaperNum[_showNum]].transform.rotation = storyBookPoint[i].transform.rotation;
                if (i == 3)
                {
                    paper[_storyBookPaperNum[_showNum + 1]].transform.position = storyBookPoint[i + 1].transform.position;
                    paper[_storyBookPaperNum[_showNum + 1]].transform.rotation = storyBookPoint[i + 1].transform.rotation;
                }
                Invoke("WaitOpenBookPaper", 1.5f);
            }
            else
            {
                storyBook[i].transform.position = originalPoint.position;
                storyBook[i].transform.rotation = originalPoint.rotation;
                storyBook[i].GetComponent<Animator>().SetBool("isOpen", false);
                WaitCloseBookPaper();
            }
        }
    }
    void ChangeStoryBook()
    {
        if (!CameraControl_House.isLookShowcase) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            _showNum--;
            Limit();
            StoryBookShow();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _showNum++;
            Limit();
            StoryBookShow();
        }
    }
    void Limit()
    {
        if (_showNum < 0)
        {
            _showNum = 0;
        }
        if (_showNum >= storyBook.Length)
        {
            _showNum = storyBook.Length - 1;
        }
    }
    void WaitOpenBookPaper()
    {
        paper[_storyBookPaperNum[_showNum]].SetActive(true);
        if (_showNum == 3)
        {
            paper[_storyBookPaperNum[_showNum + 1]].SetActive(true);
        }
    }
    void WaitCloseBookPaper()
    {
        for (int i = 0; i < storyBookPoint.Length; i++)
        {
            paper[_storyBookPaperNum[i]].SetActive(false);
        }
    }


    void Leave()
    {
        if (CameraControl_House.isLookShowcase)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(LeaveShowcase());
            }
        }
    }
    IEnumerator LeaveShowcase()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookShowcase = false;
    }
}
