using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryObjectScale_Prince : MonoBehaviour
{
    public Vector3 oriScale;
    public Vector3 tarScale;
    public float _speed;
    bool isZoom = false;
    float t;

    void OnEnable()
    {
        isZoom = true;
        t = 0f;
        transform.localScale = oriScale;
    }

    void Update()
    {
        if (!isZoom) return;

        t += 3f * Time.deltaTime;
        transform.localScale = Vector3.Lerp(oriScale, tarScale, Mathf.Clamp01(t));

        if (t >= 1f)
            isZoom = false;
    }

    void OnDisable()
    {
        isZoom = false;
        transform.localScale = oriScale;
    }
}
