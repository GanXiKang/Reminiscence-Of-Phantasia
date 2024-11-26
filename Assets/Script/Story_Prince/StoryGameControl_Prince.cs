using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryGameControl_Prince : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        Invoke("GoToMenu", 10f);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
