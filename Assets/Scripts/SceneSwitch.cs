using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void ChangeScene(string SceneName)
    {
        // Check if there is more than one scene loaded
        if (SceneManager.sceneCount > 1)
        {
            // Unload the current scene
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        // Load the new scene
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);

    }

    public void StopGame()
    {
        Application.Quit();
    }
}
