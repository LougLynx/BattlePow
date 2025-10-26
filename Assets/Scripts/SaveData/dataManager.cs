using System.IO;
using UnityEngine;

public class dataManager : MonoBehaviour
{

    public static dataManager Instance { get; private set; }

    private string dataFilePath;
    public GameData gameData = new GameData(); 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        dataFilePath = Path.Combine(Application.dataPath, "gameData.json");
        LoadGameData();
    }

    public void SaveGameData()
    {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dataFilePath, json);
        Debug.Log("Game data saved!");
    }

    public void LoadGameData()
    {
        if (File.Exists(dataFilePath))
        {
            string json = File.ReadAllText(dataFilePath);
            gameData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.Log(" Chưa có file gameData.json, tạo mới!");
            SaveGameData();
        }
    }

 
}

[System.Serializable]
public class GameData
{
    public int gold = 10000;
    public float attack = 1;
    public float moveSpeed = 5f;
    public float maxHp = 100f;
    //public float currentHp = 100f;
}

