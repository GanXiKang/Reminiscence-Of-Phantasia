using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase_House : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick, openBook;

    [Header("StoryBook")]
    public GameObject[] storyBook;
    public Transform showPoint;
    public Transform originalPoint;
    public static int _showNum = 0;
    bool isShow = false;

    [Header("StoryBookPoint")]
    public GameObject[] storyBookPoint;
    public static int[] _storyBookPaperNum = new int[5];

    [Header("Paper")]
    public GameObject[] paper;

    [Header("UI")]
    public GameObject showcaseUI;
    public GameObject buttonA, buttonD;

    void Update()
    {
        UIActive();
        FirstShowcase();
        ChangeStoryBook();
    }

    void UIActive()
    {
        showcaseUI.SetActive(CameraControl_House.isLookShowcase);
        if (_showNum == 0)
        {
            buttonA.SetActive(false);
        }
        else
        {
            buttonA.SetActive(true);
        }
        if (_showNum == storyBook.Length - 1)
        {
            buttonD.SetActive(false);
        }
        else
        {
            buttonD.SetActive(true);
        }
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
                BGM.PlayOneShot(openBook);
                storyBook[i].GetComponent<Animator>().SetBool("isOpen", true);
                paper[_storyBookPaperNum[_showNum]].transform.position = storyBookPoint[i].transform.position;
                paper[_storyBookPaperNum[_showNum]].transform.rotation = storyBookPoint[i].transform.rotation;
                if (i == 3)
                {
                    paper[_storyBookPaperNum[_showNum + 1]].transform.position = storyBookPoint[i + 1].transform.position;
                    paper[_storyBookPaperNum[_showNum + 1]].transform.rotation = storyBookPoint[i + 1].transform.rotation;
                }
                Invoke("WaitOpenBookPaper", 1.6f);
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
            if (_showNum != 0)
            {
                BGM.PlayOneShot(onClick);
                _showNum--;
                Limit();
                StoryBookShow();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_showNum != storyBook.Length - 1)
            {
                BGM.PlayOneShot(onClick);
                _showNum++;
                Limit();
                StoryBookShow();
            }
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

    public void Button_Page(string key)
    {
        if (key == "a")
        {
            if (_showNum != 0)
            {
                BGM.PlayOneShot(onClick);
                _showNum--;
                Limit();
                StoryBookShow();
            }
        }
        else if (key == "d")
        {
            if (_showNum != storyBook.Length - 1)
            {
                BGM.PlayOneShot(onClick);
                _showNum++;
                Limit();
                StoryBookShow();
            }
        }
    }
    public void Button_Leave()
    {
        BGM.PlayOneShot(onClick);
        StartCoroutine(LeaveShowcase());
    }

    IEnumerator LeaveShowcase()
    {
        BlackScreenControl.isOpenBlackScreen = true;
        yield return new WaitForSeconds(1f);
        CameraControl_House.isFreeLook = true;
        CameraControl_House.isLookShowcase = false;
    }
}
