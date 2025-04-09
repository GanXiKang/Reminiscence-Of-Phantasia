using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryColliderControl_Girl : MonoBehaviour
{
    public int _whatCollider;
    bool isOnce = true;

    void Update()
    {
        switch (_whatCollider)
        {
            case 2:
                if (StoryInteractableControl_Girl.isNeedHelp)
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_whatCollider)
            {
                case 1:
                    if (isOnce)
                    {
                        isOnce = false;
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = 1;
                        StoryDialogueControl_Girl._textCount = 30;
                    }
                    break;

                case 2:
                    if (!StoryNpcAnimator_Girl.isShotRunAway && !StoryNpcAnimator_Girl.isAttractWolf)
                    {
                        StoryUIControl_Girl.isDialogue = true;
                        StoryDialogueControl_Girl._isAboveWho1 = 1;
                        StoryDialogueControl_Girl._textCount = 31;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                    break;

                case 3:
                    AudioSource sound = GetComponent<AudioSource>();
                    sound.Play();
                    break;
            }
        }
    }
}
