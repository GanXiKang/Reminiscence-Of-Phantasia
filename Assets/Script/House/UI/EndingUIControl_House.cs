using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUIControl_House : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("BackMenu", 10f);
    }

    void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}
