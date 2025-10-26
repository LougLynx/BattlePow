using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private int currentEnergy;
    [SerializeField] private int energyHold = 3;
    [SerializeField] private GameObject boss;
    private bool callBoss = false;
    [SerializeField] private GameObject enemySpam;
    [SerializeField] private Image energyBar;
    [SerializeField] private GameObject[] panels;


    [SerializeField] GameObject mana;
    [SerializeField] private GameObject gold;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float powerUpInterval = 30f;
    private float nextPowerUpTime = 0f;

    void Start()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        MainMenu();
        Home();
		audioManager.StopAudioGame();
	}

    void Update()
    {
        if (Time.time >= nextPowerUpTime)
        {
            nextPowerUpTime = Time.time + powerUpInterval;
            PowerUpPlayer();
            PowerUpEnemies();
        }
    }

    private void PowerUpPlayer()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.maxHp += 10;  
            player.attack += 2;   
            player.moveSpeed *= 1.05f; 
        }
    }

    public void PowerUpEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.maxHp *= 1.2f; 
            enemy.enterDamege *= 1.15f;
            enemy.stayDamage *= 1.15f; 
        }
    }



    public void addE()
    {
        if (callBoss)
        {
            return;
        };
        currentEnergy += 1;
        UpdateEnergyBar();
        if (currentEnergy == energyHold)
        {
            CallBoss();
        }
    }
    public void CallBoss()
    {
        callBoss = true;
        boss.SetActive(true);
        //enemySpam.SetActive(false);

        //hidden energyBar
        mana.SetActive(false);
		audioManager.PlayBossAudio();

	}
    private void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            float fill = Mathf.Clamp01((float) currentEnergy / (float)energyHold);
            energyBar.fillAmount = fill;
        }

    }


    public void MainMenu()
    {
        //mainMenu.SetActive(true);
        //pauseMenu.SetActive(false);
        //overMenu.SetActive(false);

        ShowPanelByName("MainMenu");
        Time.timeScale = 0f;
    }
    public void OverMenu()
    {
        ShowPanelByName("GameOver");
        Time.timeScale = 0f;

    }
    public void PauseMenu()
    {
        ShowPanelByName("GamePause");
        Time.timeScale = 0f;
		audioManager.StopAudioGame();
	}
    public void NewGame()
    {
        playerData.Instance.ResetPlayer();
        enemyManager.Instance.ResetEnemies();
        ShowPanelByName();
        mana.SetActive(true);
        Time.timeScale = 1f;
		audioManager.PlayDefaultAudio();
	}
    public void ResumeGame()
    {
        ShowPanelByName();
        Time.timeScale = 1f;
    }  

    public void Shop()
    {
    
        ShowPanelByName("Shop");
        gold.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Home()
    {
        ShowPanelByName("Home");
        Time.timeScale = 0f;
	}
    public void UpGrade()
    {
     
        ShowPanelByName("UpGrade");
        gold.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Save_Quit()
    {
        enemyManager.Instance.SaveEnemies();
        playerData.Instance.SavePlayer();
        ShowPanelByName("Home");
        Time.timeScale = 0f;
    } 
    public void Continue()
    {
        enemyManager.Instance.LoadEnemies();
        playerData.Instance.LoadPlayer();
        mana.SetActive(true);
        ShowPanelByName();
        Time.timeScale = 1f;
		audioManager.PlayDefaultAudio();
	}
	public void ContinuePauseMenu()
    {

        mana.SetActive(true);
        ShowPanelByName();
        Time.timeScale = 1f;
		audioManager.PlayDefaultAudio();
	}
    public void Quit()
    {

        ShowPanelByName("Home");
        Time.timeScale = 0f;
    }

    public void ChooseScene()
    {
        ShowPanelByName("ChooseLevel");
        Time.timeScale = 0f;
    }
    //public void RestartGame()
    //{

    //    playerData.Instance.ResetPlayer();
    //    enemyManager.Instance.ResetEnemies();

      

    //    mana.SetActive(true);
    //    //mana.SetActive(false);
    //    //ShowPanelByName();
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    Time.timeScale = 1f;

    //}


    public void ShowPanelByName(string panelName)
    {
        // Ẩn tất cả panel
        foreach (GameObject panel in panels)
        {
            //panel.SetActive(false);
            panel.SetActive(panel.name == panelName);
        }

        mana.SetActive(true);
   
    }   
    public void ShowPanelByName()
    {
        // Ẩn tất cả panel
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);

        }

  

    }

}
