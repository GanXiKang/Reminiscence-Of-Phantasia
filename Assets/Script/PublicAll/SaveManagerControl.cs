using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int playerCoins;
    public string currentSceneName;
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
            saveFilePath = Application.persistentDataPath + "/savefile.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return null;
        }
    }
}
