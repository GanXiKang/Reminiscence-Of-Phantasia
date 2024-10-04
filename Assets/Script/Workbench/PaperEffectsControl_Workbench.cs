using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperEffectsControl_Workbench : MonoBehaviour
{
    public GameObject[] paperOut;
    public GameObject[] paperOutEffects;

    void Start()
    {
        Invoke("PaperOut()", 2f);
    }

    void Update()
    {
        PaperOutEffects();
    }

    void PaperOut()
    {
        for (int i = 0; i < paperOutEffects.Length; i++)
        {
            paperOutEffects[i].SetActive(true);
        }
    }
    void PaperOutEffects()
    {
        CheckEffect(paperOut[0], paperOut[1], paperOutEffects[0]);
        CheckEffect(paperOut[1], paperOut[2], paperOutEffects[1]);
        CheckEffect(paperOut[2], paperOut[3], paperOutEffects[2]);
        CheckEffect(paperOut[3], paperOut[0], paperOutEffects[3]);
    }
    void CheckEffect(GameObject obj1, GameObject obj2, GameObject effect)
    {
        if (obj1 == null && obj2 == null)
        {
            Destroy(effect);
        }
    }

}
