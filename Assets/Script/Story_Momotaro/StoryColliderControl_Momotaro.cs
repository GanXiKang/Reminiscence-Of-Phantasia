using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryColliderControl_Momotaro : MonoBehaviour
{
    public int _whatCollider;

    //Once
    bool isParrotOnce = true;
    bool isStoneSuccess = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_whatCollider)
            {
                case 1:
                case 2:
                case 3:
                    if (!StoryPlayerAnimator_Momotaro.isStone)
                    {
                        StoryStrongWind_Momotaro._respawnNum = _whatCollider;
                        StoryStrongWind_Momotaro.isBlownAway = true;
                        BlackScreenControl.isOpenBlackScreen = true;
                    }
                    else
                    {
                        if (isStoneSuccess)
                        {
                            isStoneSuccess = false;
                            StoryUIControl_Momotaro.isDialogue = true;
                            StoryDialogueControl_Momotaro._textCount = 8;
                        }
                    }
                    break;

                case 4:
                    if (!StoryGameControl_Momotaro.isParrotActive && isParrotOnce)
                    {
                        isParrotOnce = false;
                        StoryUIControl_Momotaro.isDialogue = true;
                        StoryDialogueControl_Momotaro._isAboveWho1 = 10;
                        StoryDialogueControl_Momotaro._textCount = 73;
                    }
                    break;
            }
        }
    }
}
