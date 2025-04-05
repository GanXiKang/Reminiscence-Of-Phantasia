using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUIControl_House : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("TransitionUI", 9f);
        Invoke("BackMenu", 10f);
    }

    void TransitionUI()
    {
        TransitionUIControl.isTransitionUIAnim_In = true;
    }
    void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}
