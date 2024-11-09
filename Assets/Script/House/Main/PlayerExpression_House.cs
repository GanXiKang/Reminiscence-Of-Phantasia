using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpression_House : MonoBehaviour
{
    SkinnedMeshRenderer smr;

    [Header("Material")]
    public Material openEyes;
    public Material closeEyes;
    public Material happy;
    public Material Sad;
    bool isBlink = false;
    bool isHappyExp = false;
    bool isSleepExp = false;

    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        Blink();
        Happy();
        Sleep();
    }

    void Blink()
    {
        if (isBlink)
        {
            int r = Random.Range(1, 3);
            StartCoroutine(Blink(r));
        }
    }
    void Happy()
    {
        if (PlayerControl_House.isHappy)
        {
            if (!isHappyExp)
            {
                isHappyExp = true;
                isBlink = false;
                StopCoroutine(Blink(0));
                smr.material = happy;
            }
        }
        else
        {
            if (isHappyExp)
            {
                isHappyExp = false;
                isBlink = true;
                smr.material = openEyes;
            }

        }
    }
    void Sleep()
    {
        if (PlayerControl_House.isSleep)
        {
            if (!isSleepExp)
            {
                isSleepExp = true;
                isBlink = false;
                StopCoroutine(Blink(0));
                smr.material = closeEyes;
            }
        }
        else
        {
            //if (isSleepExp)
            //{
            //    isSleepExp = false;
            //    isBlink = true;
            //    smr.material = openEyes;
            //}
        }
    }

    IEnumerator Blink(int wait)
    {
        isBlink = false;
        smr.material = closeEyes;
        yield return new WaitForSeconds(0.1f);
        smr.material = openEyes;
        yield return new WaitForSeconds(wait);
        isBlink = true;
    }
}
