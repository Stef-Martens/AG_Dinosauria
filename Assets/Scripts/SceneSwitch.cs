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
        SceneManager.LoadScene(SceneName);
    }

    public void StopGame()
    {
        Application.Quit();
    }
}
