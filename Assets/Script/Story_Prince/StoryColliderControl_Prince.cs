using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryColliderControl_Prince : MonoBehaviour
{
    public int _whatCollider;

    bool isLookPastOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_whatCollider)
            {
                case 1:
                    if (isLookPastOnce)
                    {
                        isLookPastOnce = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._textCount = 9;
                    }
                    break;
            }
        }
    }
}
