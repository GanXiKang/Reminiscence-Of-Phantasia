using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase_House : MonoBehaviour
{
    [Header("StoryBook")]
    public GameObject[] storyBook;
    public Transform showPoint;
    Transform originalPoint;
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
        switch (_showNum)
        {
            case 0:
                originalPoint = storyBook[0].transform;
                storyBook[0].transform.position = showPoint.position;
                storyBook[0].transform.rotation = showPoint.rotation;
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;
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
