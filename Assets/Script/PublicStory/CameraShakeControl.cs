using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeControl : MonoBehaviour
{
    public Transform cameraTransform; // �R�^��Transform
    public float shakeDuration = 0.5f; // ���ӳ��m�r�g
    public float shakeAmount = 0.1f; // ���ӏ���
    public float decreaseFactor = 1.0f; // ����˥�p�ٶ�

    private Vector3 originalPos;
    private float currentShakeDuration = 0f;

    void Start()
    {
        originalPos = cameraTransform.localPosition; // ӛ�ԭʼλ��
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            // �S�C����һ��ƫ�������ӔzӰ�C
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            // ��u�p�����ӕr�g
            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            // �֏͵�ԭʼλ��
            currentShakeDuration = 0f;
            cameraTransform.localPosition = originalPos;
        }
    }

    public void TriggerShake(float duration)
    {
        // ��������
        currentShakeDuration = duration;
    }
}
