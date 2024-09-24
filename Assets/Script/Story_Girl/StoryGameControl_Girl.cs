using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Girl : MonoBehaviour
{
    [Header("Scene")]
    public GameObject teachWall;
    public static bool isWallActive = true;

    void Update()
    {
        teachWall.SetActive(isWallActive);
    }
}
