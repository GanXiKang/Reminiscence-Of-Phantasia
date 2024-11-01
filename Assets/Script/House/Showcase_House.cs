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

    void Start()
    {
        
    }

    void Update()
    {
        Leave();
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
                storyBook[i].GetComponent<Animator>().SetBool("isClose", true);
            }
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
