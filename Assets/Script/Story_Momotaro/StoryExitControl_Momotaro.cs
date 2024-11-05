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
            if (_goToThatScene != _changeSceneNum) return;

            bar.fillAmount += _barSpeed * Time.deltaTime / 2;
            if (bar.fillAmount == 1)
            {
                isExit = false;
                StoryLoadingScene_Momotaro.isOpen = true;
                switch (_goToThatScene)
                {
                    case 1:
                        isGoScene2 = true;
                        break;

                    case 2:
                        if (isGoScene2)
                            isGoScene2 = false;
                        break;
                }
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
                    sceneName.text = "��߅";
                    break;

                case 2:
                    sceneName.text = "ɭ��";
                    break;

                case 3:
                    sceneName.text = "ɽ��";
                    break;

                case 4:
                    sceneName.text = "�V��";
                    break;
            }
            _changeSceneNum = _goToThatScene;
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
