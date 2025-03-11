using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryGameControl_Prince : MonoBehaviour
{
    [Header("Texture")]
    public Texture2D mouse1;
    public Texture2D mouse2;
    public Vector2 hotSpot = Vector2.zero;
    bool isClick = false;

    void Start()
    {
        //StartCoroutine(GoToMenu());
    }

    void Update()
    {
        MouseCursor();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StoryTeachControl.isTeachActive = true;
        }
    }

    void MouseCursor()
    {
        if (isClick)
        {
            Cursor.SetCursor(mouse2, hotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(mouse1, hotSpot, CursorMode.Auto);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
            Invoke("FalseisClick", 0.5f);
        }
    }
    void FalseisClick()
    {
        isClick = false;
    }

    //IEnumerator GoToMenu()
    //{
    //    yield return new WaitForSeconds(5f);
    //    TransitionUIControl.isTransitionUIAnim_In = true;
    //    yield return new WaitForSeconds(1f);
    //    SceneManager.LoadScene(0);
    //}
}
