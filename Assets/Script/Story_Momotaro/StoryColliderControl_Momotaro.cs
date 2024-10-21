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
                    if (!StoryPlayerAnimator_Momotaro.isRaccoonStone)
                    {
                        Invoke("BlownAwayRenewPos",0.9f);
                        BlackScreenControl.isOpenBlackScreen = true;
                    }
                    break;
            }
        }
    }

    void BlownAwayRenewPos()
    {
        StoryStrongWind_Momotaro.isBlownAway = true;
    }
}
