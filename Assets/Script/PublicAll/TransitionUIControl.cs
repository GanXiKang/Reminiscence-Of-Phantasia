using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionUIControl : MonoBehaviour
{
    public Vector3 startScale = new Vector3(350f, 200f, 1f);
    public Vector3 targetScale = new Vector3(19.5f, 11f, 1f);
    public float animationDuration = 1f;
    private bool isAnimating = false;
    private float animationTime = 0f;

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        // �z���Ƿ�ԓ���ӄӮ�
        if (isAnimating)
        {
            // ���ӕr�g���K�_���r�g�������^�Ӯ����m�r�g
            animationTime += Time.deltaTime;
            float t = animationTime / animationDuration;

            // ʹ�� Lerp ������Ŀs�ŏ� startScale �u׃�� targetScale
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            // ���Ӯ��Y���r��ֹͣ�Ӯ�
            if (animationTime >= animationDuration)
            {
                isAnimating = false;
            }
        }
    }

    // �{�ô˷����Ԇ��ӄӮ�
    public void StartScalingAnimation()
    {
        isAnimating = true;
        animationTime = 0f; // ���ÄӮ��r�g
    }
}
