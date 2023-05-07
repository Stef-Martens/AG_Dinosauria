using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text ReasonText;
    public void StartGameOver(string reason)
    {
        Time.timeScale = 0f;
        ReasonText.text = reason;

        // ui tonen enzowe
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Restart()
    {
        FindObjectOfType<SceneSwitch>().ChangeScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
    }
}
