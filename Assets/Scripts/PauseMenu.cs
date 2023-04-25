using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject OptionsPanel;
    public GameObject ButtonsMain;
    public Slider MusicSlider;
    public Slider EffectsSlider;

    // Define variables for the master volume and effects volume
    float musicVolume = 1.0f;
    float effectsVolume = 1.0f;
    AudioSource musicSource;
    List<AudioSource> effectsSource;

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

    void Start()
    {
        effectsSource = new List<AudioSource>();
        foreach (var source in FindObjectsOfType<AudioSource>())
        {
            if (source.playOnAwake)
                musicSource = source;

            else
                effectsSource.Add(source);
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

    public void BackToPauseMenu()
    {
        ButtonsMain.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        ButtonsMain.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        FindObjectOfType<SceneSwitch>().ChangeScene("Selectionscreen");
    }


    public void ChangeVolumeMusic(Slider slider)
    {
        musicSource.volume = slider.value;
    }

    // Called when the user exits the options menu
    public void ChangeVolumeEffects(Slider slider)
    {
        foreach (var source in effectsSource)
        {
            source.volume = slider.value;
        }
    }
}
