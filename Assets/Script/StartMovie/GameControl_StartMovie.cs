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
        Skip_Button();
    }

    public void Skip_Button()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        videoPlayer.Stop();
        TransitionUIControl.isTransitionUIAnim_In = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
