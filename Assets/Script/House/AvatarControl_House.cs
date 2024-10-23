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
    public static int _whoDialogue;
    bool isSwitching = false;
    bool isSwitchActive = false;

    void Start()
    {
        avatar = GetComponent<Image>();
    }

    void Update()
    {
        if (_who == _whoDialogue && isSwitchActive && !isSwitching)
        {
            StartCoroutine(SwitchAvatar());
        }
    }

    IEnumerator SwitchAvatar()
    {
        isSwitching = true;

        while (isSwitchActive)
        {
            avatar.sprite = mouth;
            yield return new WaitForSeconds(0.5f); // �ГQ��mouth�K�ȴ�0.5��

            avatar.sprite = normal;
            yield return new WaitForSeconds(0.5f); // �ГQ��normal�K�ȴ�0.5��
        }

        isSwitching = false;
    }
}
