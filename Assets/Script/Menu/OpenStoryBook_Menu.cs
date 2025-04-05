using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStoryBook_Menu : MonoBehaviour
{
    public Animator animGirl;
    public Animator animMomo;
    public Animator animPrince;

    public bool isOpenGirl;
    public bool isOpenMomo;
    public bool isOpenPrince;

    void Start()
    {
        isOpenGirl = false;
        isOpenMomo = false;
        isOpenPrince = false;
    }

    void Update()
    {
        animGirl.SetBool("isOpen", isOpenGirl);
        animMomo.SetBool("isOpen", isOpenMomo);
        animPrince.SetBool("isOpen", isOpenPrince);
    }
}
