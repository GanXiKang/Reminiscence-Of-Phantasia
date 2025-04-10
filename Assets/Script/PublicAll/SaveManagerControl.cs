using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int gameDay;
    public int gameStoryNum;
    public int playerCoins;
    public int[] storyBookPaperNum = new int[5];
    public bool[] isSpecialEnd = new bool[4];
    public bool[] isColorUnlock = new bool[11];
}

public class SaveManagerControl : MonoBehaviour
{
    public static SaveManagerControl Instance { get; private set; }
    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Application.persistentDataPath + "/SaveFile.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }

    public void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        string path = Application.persistentDataPath + "/saveData.json";
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    public GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            return null;
        }
    }
}
