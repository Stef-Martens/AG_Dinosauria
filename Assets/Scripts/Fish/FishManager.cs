using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    // LIFES AND PROGRESS
    public int Lifes = 3;
    public int AlgaeCount = 0;
    public int WormCount = 0;
    public int SnailCount = 0;
    public Text PickupText;
    public GameObject[] Pickups;
    public float spawnYPickup = 6.5f;
    public float spawnIntervalPickup = 1f;
    public float spawnRangePickup = 9f;

    public Text AlgaeText;
    public Text SnailText;
    public Text WormsText;

    public Text LifesText;

    public GameObject gameoverObject;


    void Start()
    {
        InvokeRepeating("SpawnPickup", 0f, spawnIntervalPickup);
        EditPickupsText();
    }

    void Update()
    {
        LifesText.text = Lifes.ToString();
        if (Lifes <= 0)
        {
            gameoverObject.SetActive(true);
            gameoverObject.GetComponent<GameOver>().StartGameOver("You lost all 3 of your lifes.");
        }
    }

    void SpawnPickup()
    {
        // Randomly generate an X position within the spawn range
        float spawnX = UnityEngine.Random.Range(-spawnRangePickup, spawnRangePickup);

        int randomIndex = UnityEngine.Random.Range(0, Pickups.Length);
        GameObject randomPickup = Pickups[randomIndex];

        Instantiate(randomPickup, new Vector3(spawnX, spawnYPickup, 0f), Quaternion.identity);
    }

    public void EditPickupsText()
    {
        AlgaeText.text = AlgaeCount.ToString();
        SnailText.text = SnailCount.ToString();
        WormsText.text = WormCount.ToString();
        if (AlgaeCount >= 5 && WormCount >= 5 && SnailCount >= 5)
        {
            FindObjectOfType<SceneSwitch>().ChangeScene("QuizFish");
        }
    }

}
