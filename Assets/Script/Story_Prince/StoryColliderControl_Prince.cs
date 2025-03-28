using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryColliderControl_Prince : MonoBehaviour
{
    public int _whatCollider;

    bool isLookPastOnce = true;
    bool isLookBaseOnce = true;
    bool isPrinceAppearOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_whatCollider)
            {
                case 1:
                    if (isLookPastOnce && StoryInteractableControl_Prince._helpChildQian == 0)
                    {
                        isLookPastOnce = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._textCount = 9;
                    }
                    break;

                case 2:
                    StoryUIControl_Prince.isDialogue = true;
                    StoryDialogueControl_Prince._textCount = 89;
                    break;
                     
                case 3:
                    if (isLookBaseOnce)
                    {
                        isLookBaseOnce = false;
                        StoryNpcAnimator_Prince.isWet = false;
                        StoryInteractableControl_Prince.isPrinceInNow = true;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = 2;
                        StoryDialogueControl_Prince._isAboveWho2 = 4;
                        StoryDialogueControl_Prince._textCount = 32;
                    }
                    break;

                case 4:
                    AudioSource sound = GetComponent<AudioSource>();
                    sound.Play();
                    break;

                case 5:
                    if (StoryGhostControl_Prince.isAscend && isPrinceAppearOnce)
                    {
                        isPrinceAppearOnce = false;
                        StoryUIControl_Prince.isDialogue = true;
                        StoryDialogueControl_Prince._isAboveWho1 = 10;
                        StoryDialogueControl_Prince._textCount = 67;
                    }
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_whatCollider)
            {
                case 4:
                    AudioSource sound = GetComponent<AudioSource>();
                    sound.Stop();
                    break;
            }
        }
    }
}
