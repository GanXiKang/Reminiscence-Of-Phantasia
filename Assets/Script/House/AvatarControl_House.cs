using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarControl_House : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite normal;
    public Sprite mouth;
    public int _who;
    public static int _whoDialogue;

    void Update()
    {
        if (_who == _whoDialogue) return;

        
    }

}
