using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuCanvas.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }

        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuCanvas.SetActive(false);
    }
}
