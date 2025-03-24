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

    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        transitionUI.SetActive(TransitionUIControl.isTransitionUIAnim_In || TransitionUIControl.isTransitionUIAnim_Out);
        videoPlayer.SetDirectAudioVolume(0, SettingControl.volumeBGM);
        MouseCursor();

        if (SettingControl.isSettingActive)
        {
            videoPlayer.Pause();
            isPause = true;
        }
        else
        {
            if (isPause)
            {
                videoPlayer.Play();
                isPause = false;
            }
        }
    }

    void MouseCursor()
    {
        if (isClick)
            Cursor.SetCursor(mouse[1], hotSpot, CursorMode.Auto);
        else
            Cursor.SetCursor(mouse[0], hotSpot, CursorMode.Auto);

        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            Invoke("FalseisClick", 0.5f);
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
