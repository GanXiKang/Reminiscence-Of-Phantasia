using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryExitControl : MonoBehaviour
{
    GameObject player;
    public int _goToThatScene;

    [Header("ExitUI")]
    public GameObject exitUI;
    public Image bar;
    public Text sceneName;
    float _barSpeed = 2f;
    bool isExit = false;

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
                    case 1:          //Street
                        sceneName.text = "�ֵ�";
                        StoryLoadingScene_Girl.isLeftOpen = true;
                        break;

                    case 2:          //forest
                        sceneName.text = "ɭ��";
                        StoryLoadingScene_Girl.isRightOpen = true;
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
