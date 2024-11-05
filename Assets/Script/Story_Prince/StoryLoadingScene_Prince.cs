using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryLoadingScene_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip nowBGM, pastBGM, futureBGM;

    [Header("Scene")]
    public GameObject now;
    public GameObject past;
    public GameObject future;
    public static bool isNowScene = true;
    public static bool isPastScene = false;
    public static bool isFutureScene = false;
    public static bool isChange = false;

    [Header("LoadingUI")]
    public GameObject loadingUI;
    public Image a;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
