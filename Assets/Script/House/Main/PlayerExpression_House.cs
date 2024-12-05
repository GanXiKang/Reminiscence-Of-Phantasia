using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpression_House : MonoBehaviour
{
    SkinnedMeshRenderer smr;
    Coroutine blinkCoroutine;

    [Header("Material")]
    public Material openEyes;
    public Material closeEyes;
    public Material happy;
    public Material Sad;
    bool isBlink;
    bool isHappyExp;
    bool isSleepExp;

    void OnEnable()
    {
        isBlink = true;
        isHappyExp = false;
        isSleepExp = false;
    }

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
        if (PlayerControl_House.isHappy) return;
        if (PlayerControl_House.isSleep) return;

        if (isBlink)
        {
            int r = Random.Range(1, 3);
            blinkCoroutine = StartCoroutine(Blink(r));
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
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                }
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
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                }
                smr.material = closeEyes;
            }
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
        blinkCoroutine = null;
    }
}
