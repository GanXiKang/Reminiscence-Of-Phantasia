using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameControl_StartMovie : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }

    public void Skip_Button()
    {
        videoPlayer.Stop();
        SceneManager.LoadScene(1);
    }
}
