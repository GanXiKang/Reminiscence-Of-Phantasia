using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpression_House : MonoBehaviour
{
    SkinnedMeshRenderer smr;

    [Header("Material")]
    public Material openEyes;
    public Material closeEyes;
    bool isBlink = true;

    void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (isBlink)
        {
            int r = Random.Range(1, 3);
            StartCoroutine(Blink(r));
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
