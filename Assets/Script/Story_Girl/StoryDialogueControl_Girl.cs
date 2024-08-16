using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogueControl_Girl : MonoBehaviour
{
    GameObject player;

    [Header("UITransform")]
    public Transform[] target;
    public static int _isAboveWho;

    [Header("UIComponents")]
    public Transform dialogueUI;
    public Text content;

    [Header("TextFile")]
    public TextAsset[] textFile;
    public int index;
    public float textSpend;
    public static int textCount = 1;
    bool textFinish;

    List<string> textList = new List<string>();

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnEnable()
    {
        GetTextFormFile(textFile[textCount]);
        StartCoroutine(SetTextLabelIndexUI());
    }

    void Update()
    {
        
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

    IEnumerator SetTextLabelIndexUI()
    {
        textFinish = false;
        content.text = "";
        switch (textList[index].Trim())
        {
            case "A":

                index++;
                break;

            case "B":
                index++;
                break;

            case "Œ¦Ô’½YÊø":
                textFinish = true;
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
