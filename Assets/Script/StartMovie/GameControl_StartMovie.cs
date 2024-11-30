using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameControl_StartMovie : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip onClick;

    [Header("Video")]
    public VideoPlayer videoPlayer;
    bool isPause = false;

    [Header("TransitionUI")]
    public GameObject transitionUI;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
        videoPlayer.SetDirectAudioVolume(0, SettingControl.volumeBGM);

        if (SettingControl.isSettingActive)
        {
            BGM.Play();
            videoPlayer.Pause();
            isPause = true;
        }
        else
        {
            if (isPause)
            {
                BGM.Stop();
                videoPlayer.Play();
                isPause = false;
            }
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Skip_Button();
    }

    public void Skip_Button()
    {
        BGM.PlayOneShot(onClick);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        TransitionUIControl.isTransitionUIAnim_In = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
