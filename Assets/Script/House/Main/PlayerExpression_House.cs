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
    public static bool isHappy = false;
    bool isBlink = true;

    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (isHappy)
        {
            isHappy = false;
            isBlink = false;
            StopCoroutine(Blink(0));
            smr.material = happy;
            Invoke("HappyFinish", 3.5f);
        }
        if (isBlink)
        {
            int r = Random.Range(1, 3);
            StartCoroutine(Blink(r));
        }
    }

    void HappyFinish()
    {
        isBlink = true;
        smr.material = openEyes;
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
