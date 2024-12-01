using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl_House : MonoBehaviour
{
    Animator anim;

    public static bool isHappy = false;
    public static bool isWave = false;
    public static bool isBye = false;
    public static bool isBag = false;
    public static bool isBag_On = false;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip bye, call;

    [Header("Point")]
    public Transform[] plotPoint;
    public static int _goPointNum = 0;
    int _nowPointNum = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Animation();
        PlotPoint();
    }

    void Animation()
    {
        if (isHappy)
        {
            BGM.PlayOneShot(call);
            anim.SetBool("isHappy", true);
            Invoke("FalseByAnimation", 0.2f);
            isHappy = false;
        }
        if (isWave)
        {
            anim.SetBool("isWave", true);
        }
        else
        {
            anim.SetBool("isWave", false);
        }
        if (isBye)
        {
            BGM.PlayOneShot(bye);
            anim.SetBool("isBye", true);
            Invoke("FalseByAnimation", 0.2f);
            isBye = false;
        }
        if (isBag)
        {
            BGM.PlayOneShot(call);
            anim.SetBool("isBag", true);
            isBag = false;
        }
        if (isBag_On)
        {
            anim.SetBool("isBag_On", true);
            anim.SetBool("isBag", false);
            Invoke("FalseByAnimation", 0.2f);
            isBag_On = false;
        }
    }
    void FalseByAnimation()
    {
        anim.SetBool("isHappy", false);
        anim.SetBool("isBye", false);
        anim.SetBool("isBag_On", false);
    }
    void PlotPoint()
    {
        if (_nowPointNum == _goPointNum) return;

        _nowPointNum = _goPointNum;
        transform.position = plotPoint[_goPointNum].transform.position;
        transform.rotation = plotPoint[_goPointNum].transform.rotation;
    }
}
