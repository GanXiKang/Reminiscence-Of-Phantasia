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

    [Header("UIChoose")]
    public Transform chooseUI;
    public GameObject buttonUI;
    public Text contentA;
    public Text contentB;
    bool isChoose = false;
    int _chooseNum;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int _index;
    public float _textSpend;
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
        TextPosition();
        ChooseUI();
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        _index = 0;

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
                _textSpend = 0.1f;
                StartCoroutine(SetTextLabelIndexUI());
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _textSpend = 0f;
            }
        }
    }
    void TextPosition()
    {
        Vector3 offset = new Vector3(-120f, 300f, 0f);
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
    void ChooseUI()
    {
        if (!isChoose) return;

        Vector3 chooseUITarget = new Vector3(350f, 0f, 0f);
        chooseUI.position = Vector3.Lerp(chooseUI.position, chooseUITarget, 2f * Time.deltaTime);
    }

    IEnumerator SetTextLabelIndexUI()
    {
        textFinish = false;
        content.text = "";
        switch (textList[_index].Trim())
        {
            case "Player":
                isPlayerTalk = true;
                _index++;
                break;

            case "Target":
                isPlayerTalk = false;
                _index++;
                break;

            case "End":
                StoryUIControl_Girl.isDialogue = false;
                break;

            case "Choose":
                isChoose = true;
                break;
        }
        for (int i = 0; i < textList[_index].Length; i++)
        {
            content.text += textList[_index][i];
            yield return new WaitForSeconds(_textSpend);
        }
        textFinish = true;
        _index++;
    }
}
