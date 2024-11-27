using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl_House : MonoBehaviour
{
    Animator anim;

    public static bool isIdle = false;
    public static bool isDeliver = false;
    public static bool isDeliver_Close = false;
    public static bool isHappy = false;
    public static bool isBye = false;

    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip bye, call;

    [Header("Effects")]
    public GameObject letterEffects;

    [Header("Point")]
    public Transform[] plotPoint;
    public static int _goPointNum = 0;
    int _nowPointNum = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        letterEffects.SetActive(false);
    }
    
    void Update()
    {
        Animation();
        PlotPoint();
    }

    void Animation()
    {
        if (isIdle)
        {
            anim.SetBool("isIdle", true);
            isIdle = false;
        }
        if (isDeliver)
        {
            letterEffects.SetActive(true);
            anim.SetBool("isDeliver", true);
            anim.SetBool("isDeliver_Close", false);
            isDeliver = false;
        }
        if (isDeliver_Close)
        {
            letterEffects.SetActive(false);
            anim.SetBool("isDeliver_Close", true);
            anim.SetBool("isDeliver", false);
            isDeliver_Close = false;
        }
        if (isHappy)
        {
            BGM.PlayOneShot(call);
            anim.SetBool("isHappy", true);
            Invoke("FalseByAnimationisHappy", 0.2f);
            isHappy = false;
        }
        if (isBye)
        {
            BGM.PlayOneShot(bye);
            letterEffects.SetActive(false);
            anim.SetBool("isBye", true);
            anim.SetBool("isIdle", false);
            Invoke("FalseByAnimationisBye", 1f);
            isBye = false;
        }
    }
    void FalseByAnimationisHappy()
    {
        anim.SetBool("isHappy", false);
        isDeliver = true;
    }
    void FalseByAnimationisBye()
    {
        anim.SetBool("isBye", false);
        anim.SetBool("isDeliver", false);
    }
    void PlotPoint()
    {
        if (_nowPointNum == _goPointNum) return;

        _nowPointNum = _goPointNum;
        transform.position = plotPoint[_goPointNum].transform.position;
        transform.rotation = plotPoint[_goPointNum].transform.rotation;
    }
}
    
