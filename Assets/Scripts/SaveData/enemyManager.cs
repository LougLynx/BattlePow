using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class EnemyData
{
    public string enemyType; 
    public float x, y, z , currentHpEnemy;
  
}

[System.Serializable]
public class EnemyDataList // Lưu danh sách quái
{
    public List<EnemyData> enemies = new List<EnemyData>();
}

public class enemyManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // Danh sách Prefabs của quái
    private string path;

    public static enemyManager Instance { get; private set; }

  
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        path = Path.Combine(Application.dataPath, "enemyData.json");
      
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    SaveEnemies();
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    LoadEnemies();
        //}
    }

    public void SaveEnemies()
    {
        EnemyDataList saveData = new EnemyDataList();
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                EnemyData data = new EnemyData
                {
                    enemyType = enemy.name.Replace("(Clone)", "").Trim(), // Loại quái
                    x = enemy.transform.position.x,
                    y = enemy.transform.position.y,
                    z = enemy.transform.position.z,
                    currentHpEnemy = enemyScript.currentHp,
                };
                saveData.enemies.Add(data);
            }
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
        Debug.Log("Đã lưu vị trí của tất cả quái vào enemyData.json");
    }

    public void LoadEnemies()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning("Không tìm thấy file enemyData.json để load quái!");
            return;
        }

        string json = File.ReadAllText(path);
        EnemyDataList saveData = JsonUtility.FromJson<EnemyDataList>(json);

        // Xóa tất cả quái cũ trước khi load lại
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Destroy(enemy);
        }

        foreach (EnemyData data in saveData.enemies)
        {
            GameObject prefab = enemyPrefabs.Find(p => p.name == data.enemyType);
            if (prefab != null)
            {
                GameObject enemyInstance = Instantiate(prefab, new Vector3(data.x, data.y, data.z), Quaternion.identity);
                Enemy enemyScript = enemyInstance.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.SetCurrentHp(data.currentHpEnemy); // Gọi phương thức SetCurrentHp để khôi phục HP

                }
               
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Prefab cho loại quái: " + data.enemyType);
            }
        }

        Debug.Log("Đã load tất cả quái từ enemyData.json.");
    }
    public void ResetEnemies()
    {
        if (File.Exists(path))
        {
            File.Delete(path);  
        }

      
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Destroy(enemy);
        }


    }



}
