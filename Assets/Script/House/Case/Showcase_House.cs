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
    public static int[] _storyBookNum;

    [Header("Paper")]
    public GameObject[] paper;

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
            }
            else
            {
                storyBook[i].transform.position = originalPoint.position;
                storyBook[i].transform.rotation = originalPoint.rotation;
                storyBook[i].GetComponent<Animator>().SetBool("isOpen", false);
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
        print(_showNum);
    }
    void PaperFollow()
    {
        paper[_storyBookNum[0]].transform.position = storyBookPoint[0].transform.position;
        paper[_storyBookNum[0]].transform.rotation = storyBookPoint[0].transform.rotation;
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
