using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{

    public List<string> sceneNames = new List<string>();

    void Update()
    {
        // Kiểm tra các phím từ 1 đến 5 và tải scene tương ứng
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) && i < sceneNames.Count)
            {
                LoadScene(i);
                break; // Ngừng vòng lặp sau khi tải scene
            }
        }
    }


    public void LoadScene(int sceneIndex)
    {
        LoadNextScene(sceneIndex);
    }
    public void LoadNextScene(int sceneIndex)
    {
        if (sceneNames.Count == 0 || sceneIndex >= sceneNames.Count)
        {
            Debug.LogWarning("No scenes to load or invalid index.");
            return;
        }

        SceneManager.LoadScene(sceneNames[sceneIndex]);
    }
}
