using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int gameDay;
    public int gameStoryNum;
    public int playerCoins;
    public string currentSceneName;
    public bool[] houseBooleans;
    public bool[] girlBooleans;
    public bool[] momotaroBooleans;
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

    public void SaveGame(GameData gameData)
    {
        SaveHouseBooleans();

        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    void SaveHouseBooleans()
    {
        GameData gameData = new GameData();
        gameData.houseBooleans = new bool[]
        {
            InteractableControl_House.isColliderActive[1],
            InteractableControl_House.isColliderActive[2],
            InteractableControl_House.isColliderActive[3],
            InteractableControl_House.isColliderActive[4],
            InteractableControl_House.isColliderActive[5],
        };
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
            return null;
        }
    }

    public bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
}
