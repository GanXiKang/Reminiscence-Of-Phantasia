using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    [Header("Scene")]
    public GameObject wall_NotSee;
    public static bool isWallActive = true;

    void Update()
    {
        wall_NotSee.SetActive(isWallActive);
    }
}
