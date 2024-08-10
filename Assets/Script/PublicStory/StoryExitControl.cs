using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryExitControl : MonoBehaviour
{
    GameObject player;
    public static int _goToThatScene;

    [Header("ExitUI")]
    public GameObject exitUI;
    public Image bar;
    float _barSpeed = 2f;
    bool isExit = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        exitUI.SetActive(isExit);
        exitUI.transform.position = player.transform.position + new Vector3(0f, 100f, 0f);

        Bar();
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
                        StoryLoadingScene_Girl.isLeftOpen = true;
                        break;

                    case 2:          //forest
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
