using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryColliderControl_Girl : MonoBehaviour
{
    public int _whatCollider;
    bool isOnce = true;

    void Update()
    {
        if (_whatCollider == 2)
        {
            if (StoryInteractableControl_Girl.isNeedHelp)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_whatCollider == 1)
            {
                if (isOnce)
                {
                    isOnce = false;
                    StoryUIControl_Girl.isDialogue = true;
                    StoryDialogueControl_Girl._isAboveWho1 = 1;
                    StoryDialogueControl_Girl._textCount = 30;
                }
            }
            else 
            {
                if (!StoryNpcAnimator_Girl.isShotRunAway && !StoryNpcAnimator_Girl.isAttractWolf)
                {
                    StoryUIControl_Girl.isDialogue = true;
                    StoryDialogueControl_Girl._isAboveWho1 = 1;
                    StoryDialogueControl_Girl._textCount = 31;
                }
            }
        }
    }
}
