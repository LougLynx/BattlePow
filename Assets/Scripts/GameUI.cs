using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public void StartGame()
    {
        gameManager.NewGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContiueGame()
    {
        gameManager.ResumeGame();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
