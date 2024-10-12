using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionUIControl : MonoBehaviour
{
    [Header("Scale")]
    public Vector3 startScale = new Vector3(350f, 200f, 1f);
    public Vector3 targetScale = new Vector3(19.5f, 11f, 1f);
    public float animationDuration = 0.8f;
    public static  bool isTransitionUIAnim_In = false;
    public static bool isTransitionUIAnim_Out = false;
    float animationTime = 0f;

    void Start()
    {
        //transform.localScale = targetScale;
        //isTransitionUIAnim_Out = true;
    }

    void Update()
    {
        if (isTransitionUIAnim_In)
        {
            animationTime += Time.deltaTime;
            float t = animationTime / animationDuration;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            if (animationTime >= animationDuration)
            {
                isTransitionUIAnim_In = false;
                animationTime = 0f;
            }
        }
        if (isTransitionUIAnim_Out)
        {
            animationTime += Time.deltaTime;
            float t = animationTime / animationDuration;

            transform.localScale = Vector3.Lerp(targetScale, startScale, t);

            if (animationTime >= animationDuration)
            {
                isTransitionUIAnim_Out = false;
                animationTime = 0f;
            }
        }
    }
}
