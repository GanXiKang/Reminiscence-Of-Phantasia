using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryColliderControl_Momotaro : MonoBehaviour
{
    public int _whatCollider;

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
                    break;
            }
        }
    }
}
