using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

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

    public bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }

    public void SaveGame(GameData gameData)
    {
        gameData.houseBooleans = new bool[]
        {
            InteractableControl_House.isColliderActive[1],
            InteractableControl_House.isColliderActive[2],
            InteractableControl_House.isColliderActive[3],
            InteractableControl_House.isColliderActive[4],
            InteractableControl_House.isColliderActive[5],
            InteractableControl_House.isCatSeeWorkbench,
            InteractableControl_House.isCatLeave,
            InteractableControl_House.isBirdDoorBell,
            InteractableControl_House.isBirdEntrust,
            InteractableControl_House.isBirdSeeBed,
            InteractableControl_House.isBirdSeeBookcase,
            InteractableControl_House.isBirdLeave,
            InteractableControl_House.isReadMomLetter,
            InteractableControl_House.isBirdFirstMeet,
            InteractableControl_House.isMomEntrust,
            InteractableControl_House.isBookcasePlotOnce,
        };

        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to " + saveFilePath);
    }

    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            //House
            InteractableControl_House.isColliderActive[1] = gameData.houseBooleans[0];
            InteractableControl_House.isColliderActive[2] = gameData.houseBooleans[1];
            InteractableControl_House.isColliderActive[3] = gameData.houseBooleans[2];
            InteractableControl_House.isColliderActive[4] = gameData.houseBooleans[3];
            InteractableControl_House.isColliderActive[5] = gameData.houseBooleans[4];
            InteractableControl_House.isCatSeeWorkbench = gameData.houseBooleans[5];
            InteractableControl_House.isCatLeave = gameData.houseBooleans[6];
            InteractableControl_House.isBirdDoorBell = gameData.houseBooleans[7];
            InteractableControl_House.isBirdEntrust = gameData.houseBooleans[8];
            InteractableControl_House.isBirdSeeBed = gameData.houseBooleans[9];
            InteractableControl_House.isBirdSeeBookcase = gameData.houseBooleans[10];
            InteractableControl_House.isBirdLeave = gameData.houseBooleans[11];
            InteractableControl_House.isReadMomLetter = gameData.houseBooleans[12];
            InteractableControl_House.isBirdFirstMeet = gameData.houseBooleans[13];
            InteractableControl_House.isMomEntrust = gameData.houseBooleans[14];
            InteractableControl_House.isBookcasePlotOnce = gameData.houseBooleans[15];

            return gameData;
        }
        else
        {
            return null;
        }
    }
}
