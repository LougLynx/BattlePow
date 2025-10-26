using UnityEngine;
using System.IO;


[System.Serializable]
public class PlayerData
{
    public float x, y, z;
    public float currentHp =100f;
}
public class playerData : MonoBehaviour
{
    public Transform player; // Kéo Player vào đây trong Unity Inspector
    private string path;
    

    public static playerData Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        path = Application.dataPath + "/playerData.json";
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    SavePlayer();
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    LoadPlayer();
        //}
    }
    public void SavePlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player chưa được gán vào Data (script Player Data)!");
            return;
        }
        Player playerScript = player.GetComponent<Player>();
        PlayerData data = new PlayerData
        {
            x = player.position.x,
            y = player.position.y,
            z = player.position.z,
            currentHp = playerScript.currentHp
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log(" Đã lưu tọa độ Player: " + json);
    }

    public void LoadPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player chưa được gán vào PlayerSave!");
            return;
        }

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Debug log để kiểm tra dữ liệu đọc được từ file
            Debug.Log("Dữ liệu đọc được từ file JSON: " + json);

            player.position = new Vector3(data.x, data.y, data.z);
            Player playerScript = player.GetComponent<Player>();

            // Debug log để kiểm tra giá trị currentHp
            Debug.Log("currentHp trước khi gán: " + playerScript.currentHp);

            playerScript.currentHp = data.currentHp;

            // Debug log để kiểm tra giá trị currentHp sau khi gán
            Debug.Log("currentHp sau khi gán: " + playerScript.currentHp);

            Debug.Log("Đã load vị trí Player và currentHp từ JSON");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy file JSON để load!");
        }
    }

    public void ResetPlayer()
    {
      
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        // 2. Đặt lại vị trí Player về mặc định
        if (player != null)
        {
            player.position = new Vector3(0, 0, 0);
            Player playerScript = player.GetComponent<Player>();

            // Reset currentHp to maxHp
            playerScript.currentHp = playerScript.maxHp;
            playerScript.updateHpBar();
            Debug.Log("Đã đặt lại vị trí Player về mặc định!");
        }
    }
}
