using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGameControl_Momotaro : MonoBehaviour
{
    GameObject player;

    [Header("StrongWind")]
    public BoxCollider inCollider;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        StrongWind();
    }

    void StrongWind()
    {
        inCollider.isTrigger = StoryPlayerAnimator_Momotaro.isRaccoon;
    }
}
