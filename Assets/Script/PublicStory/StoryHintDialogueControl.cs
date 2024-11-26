using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryHintDialogueControl : MonoBehaviour
{
    Coroutine switchCoroutine;

    public Image hint;
    public Sprite spriteA, spriteB;
    float _switchInterval = 1f;
    bool isSwitching = false;

    void Update()
    {
        if (isDialogue())
        {
            if (!isSwitching)
            {
                isSwitching = true;
                switchCoroutine = StartCoroutine(SwitchSprite());
            }
        }
        else
        {
            isSwitching = false;
            if (switchCoroutine != null)
            {
                StopCoroutine(switchCoroutine);
                switchCoroutine = null;
            }
        }
    }

    bool isDialogue()
    {
        return StoryUIControl_Girl.isDialogue ||
               StoryUIControl_Momotaro.isDialogue ||
               StoryUIControl_Prince.isDialogue;
    }

    private IEnumerator SwitchSprite()
    {
        while (isSwitching)
        {
           hint.sprite = (hint.sprite == spriteA) ? spriteB : spriteA;
            yield return new WaitForSeconds(_switchInterval);
        }
    }
}
