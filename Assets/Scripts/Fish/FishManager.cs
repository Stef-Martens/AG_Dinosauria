using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    // HOOKS SPAWNING
    public GameObject Hook;
    public float RepeatTime = 5f;
    public float minX = -37f;
    public float maxX = 37f;

    // LIFES AND PROGRESS
    public int Lifes = 3;

    void Start()
    {
        InvokeRepeating("SpawnHooks", 0f, RepeatTime);
    }

    void SpawnHooks()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, 10, 0);
        GameObject newObject = Instantiate(Hook, spawnPosition, Quaternion.identity);
    }

    public void PlayerHit()
    {
        Lifes--;
        /*if (Lifes == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);*/
    }

}
