using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryControl_House : MonoBehaviour
{
    static DontDestoryControl_House instance;

    void Start()
    {
        print("YOUYOUYOU");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }
}
