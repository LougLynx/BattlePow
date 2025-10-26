using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;



public class goldManager : MonoBehaviour
{
    public static goldManager Instance { get; private set; }

    public TextMeshProUGUI goldText;
    public bool canSpend = true;


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
        }
    
    }
    private void Start()
    {

        if (dataManager.Instance == null)
        {
            Debug.LogError("dataManager.Instance bị NULL");
            return;
        }

        // Kiểm tra nếu goldText chưa được gán
        if (goldText == null)
        {
            Debug.LogError(" goldText chưa được gán");
            return;
        }

        UpdateGoldUI();

    }

    public int GetGold()
    {
        return dataManager.Instance.gameData.gold;
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = dataManager.Instance.gameData.gold.ToString();

        }
        else
        {
            Debug.LogError(" goldText bị NULL! Hãy gán TextMeshProUGUI trong Inspector (Data).");
        }
    }

    public void AddGold(int amount)
    {
        dataManager.Instance.gameData.gold += amount;
        dataManager.Instance.SaveGameData();
        UpdateGoldUI();
    }


    public void SpendGold(int amount)
    {
        if (!canSpend) { return; }
        if (dataManager.Instance.gameData.gold >= amount)
        {
            dataManager.Instance.gameData.gold -= amount;
            dataManager.Instance.SaveGameData();
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }
}
