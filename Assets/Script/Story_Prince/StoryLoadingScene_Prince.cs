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
    public static bool isLoading = false;
    public static bool isOpen = false;
    public static bool isClose = false;
    float _loadingSpeed = 1.5f;

    void Update()
    {
        SceneActive();
    }

    void SceneActive()
    {
        loadingUI.SetActive(isLoading);

        now.SetActive(isNowScene);
        past.SetActive(isPastScene);
        future.SetActive(isFutureScene);
    }
    void BarValue(Image bar, bool isAdd)
    {
        if (isAdd)
        {
            if (bar.fillAmount < 1)
            {
                bar.fillAmount += _loadingSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (bar.fillAmount > 0)
            {
                bar.fillAmount -= _loadingSpeed * Time.deltaTime;
            }
        }
    }
}
