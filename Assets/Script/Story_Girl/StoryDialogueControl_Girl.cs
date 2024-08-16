using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("UITransform")]
    public Transform[] target;
    public static int _isAboveWho = 0;
    bool isPlayerTalk;

    [Header("UIComponents")]
    public Transform dialogueUI;
    public Text content;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int index;
    public float textSpend;
    public static int _textCount = 1;
    bool textFinish;

    List<string> textList = new List<string>();

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        GetTextFormFile(textFile[_textCount]);
        StartCoroutine(SetTextLabelIndexUI());
    }

    void Update()
    {
        TextController();

        Vector3 offset = new Vector3(100f, 320f, 0f);
        if (isPlayerTalk)
        {
            Vector3 playerPos = Camera.main.WorldToScreenPoint(player.transform.position);
            dialogueUI.position = playerPos + offset;
        }
        else
        {
            Vector3 targetPos = Camera.main.WorldToScreenPoint(target[_isAboveWho].position);
            dialogueUI.position = targetPos + offset;
        }
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineDate = file.text.Split("\n");

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    void TextController()
    {
        if (textFinish)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                textSpend = 0.1f;
                StartCoroutine(SetTextLabelIndexUI());
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                textSpend = 0f;
            }
        }
    }

    IEnumerator SetTextLabelIndexUI()
    {
        textFinish = false;
        content.text = "";
        switch (textList[index].Trim())
        {
            case "A":
                isPlayerTalk = true;
                index++;
                break;

            case "B":
                isPlayerTalk = false;
                index++;
                break;

            case "Œ¦Ô’½YÊø":
                StoryUIControl_Girl.isDialogue = false;
                break;
        }
        for (int i = 0; i < textList[index].Length; i++)
        {
            content.text += textList[index][i];
            yield return new WaitForSeconds(textSpend);
        }
        textFinish = true;
        index++;
    }
}
