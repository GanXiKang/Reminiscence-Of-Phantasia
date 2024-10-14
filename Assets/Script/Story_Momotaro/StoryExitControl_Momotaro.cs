using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryExitControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("Value")]
    public int _goToThatScene;
    public static int _changeSceneNum;
    bool isGoScene2 = true;

    [Header("ExitUI")]
    public GameObject exitUI;
    public Image bar;
    public Text sceneName;
    public static bool isExit = false;
    float _barSpeed = 2f;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ExitUI();
        Bar();
    }

    void ExitUI()
    {
        exitUI.SetActive(isExit);

        Vector3 worldPos = player.transform.position + new Vector3(0f, 10f, 0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        exitUI.transform.position = screenPos;
    }
    void Bar()
    {
        if (isExit)
        {
            bar.fillAmount += _barSpeed * Time.deltaTime / 2;
            if (bar.fillAmount == 1)
            {
                isExit = false;
                switch (_goToThatScene)
                {
                    case 1:
                        isGoScene2 = true;
                        StoryLoadingScene_Momotaro.isLeftOpen = true;
                        //StoryGameControl_Girl.isInStreet = true;
                        break;

                    case 2:
                        //if (isGoScene2)
                        //{
                        //    isGoScene2 = false;
                            StoryLoadingScene_Girl.isRightOpen = true;
                        //}
                        //else
                        //{
                        //    StoryLoadingScene_Momotaro.isLeftOpen = true;
                        //}
                        break;

                    case 3:
                        StoryLoadingScene_Girl.isRightOpen = true;
                        break;

                    case 4:
                        StoryLoadingScene_Girl.isRightOpen = true;
                        break;
                }
                _changeSceneNum = _goToThatScene;
            }
        }
        else
        {
            bar.fillAmount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isExit = true;
            switch (_goToThatScene)
            {
                case 1:
                    sceneName.text = "∫”ﬂÖ";
                    break;

                case 2:
                    sceneName.text = "…≠¡÷";
                    break;

                case 3:
                    sceneName.text = "…ΩΩ≈";
                    break;

                case 4:
                    sceneName.text = "èVàˆ";
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isExit = false;
        }
    }
}
