using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionUIControl : MonoBehaviour
{
    [Header("Scale")]
    public Vector3 bigScale = new Vector3(1200f, 600f, 1f);
    public Vector3 smallScale = new Vector3(20f, 12f, 1f);
    public float animationDuration = 1f;
    public static  bool isTransitionUIAnim_In;
    public static bool isTransitionUIAnim_Out;
    float animationTime = 0f;

    [Header("UI")]
    public Sprite book;
    public Sprite house;
    public static bool isHouse;

    void Start()
    {
        transform.localScale = smallScale;
        isTransitionUIAnim_In = false;
        isTransitionUIAnim_Out = true;
        isHouse = true;
    }

    void OnEnable()
    {
        if (isHouse)
            gameObject.GetComponent<Image>().sprite = house;
        else
            gameObject.GetComponent<Image>().sprite = book;
    }

    void Update()
    {
        if (isTransitionUIAnim_In)
        {
            animationTime += Time.deltaTime;
            float t = animationTime / animationDuration;

            transform.localScale = Vector3.Lerp(bigScale, smallScale, t);

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

            transform.localScale = Vector3.Lerp(smallScale, bigScale, t);

            if (animationTime >= animationDuration)
            {
                isTransitionUIAnim_Out = false;
                animationTime = 0f;
            }
        }
    }
}
