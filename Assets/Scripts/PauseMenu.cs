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
    List<AudioSource> musicSource;
    List<AudioSource> effectsSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuCanvas.activeSelf && Time.timeScale != 0)
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
        Time.timeScale = 1;

        FindSources();

        if (FindObjectOfType<FirebaseManager>())
        {
            musicVolume = FindObjectOfType<FirebaseManager>().MadeUser.MusicVolume;
            effectsVolume = FindObjectOfType<FirebaseManager>().MadeUser.EffectsVolume;

            foreach (var source in musicSource)
            {
                source.volume = musicVolume;
            }

            foreach (var source in effectsSource)
            {
                source.volume = effectsVolume;
            }

            MusicSlider.value = musicVolume;
            EffectsSlider.value = effectsVolume;
        }

    }

    void FindSources()
    {
        effectsSource = new List<AudioSource>();
        musicSource = new List<AudioSource>();
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            if (source.playOnAwake)
                musicSource.Add(source);

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

        StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer());
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
        if (Time.timeScale == 0)
        {
            FindSources();
            foreach (var source in musicSource)
            {
                source.volume = slider.value;
            }
            FindObjectOfType<FirebaseManager>().MadeUser.MusicVolume = slider.value;
        }

    }

    // Called when the user exits the options menu
    public void ChangeVolumeEffects(Slider slider)
    {
        if (Time.timeScale == 0)
        {
            FindSources();
            foreach (var source in effectsSource)
            {
                source.volume = slider.value;
            }
            FindObjectOfType<FirebaseManager>().MadeUser.EffectsVolume = slider.value;
        }

    }
}
