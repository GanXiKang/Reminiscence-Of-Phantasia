using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryGameControl_Prince : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GoToMenu());
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(5f);
        TransitionUIControl.isTransitionUIAnim_In = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
