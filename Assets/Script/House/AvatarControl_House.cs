using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarControl_House : MonoBehaviour
{
    Image avatar;

    [Header("Sprite")]
    public Sprite normal;
    public Sprite mouth;
    public int _who;
    bool isAnim = false;

    void Start()
    {
        avatar = GetComponent<Image>();
    }

    void Update()
    {
        if (!UIControl_House.isDialogue) return;

        if (_who == DialogueControl_House._whoDia && !isAnim)
        {
            StartCoroutine(SwitchAvatar());
        }
    }

    IEnumerator SwitchAvatar()
    {
        isAnim = true;

        while (UIControl_House.isDialogue)
        {
            avatar.sprite = mouth;
            yield return new WaitForSeconds(0.5f);

            avatar.sprite = normal;
            yield return new WaitForSeconds(0.5f);
        }

        isAnim = false;
    }
}
