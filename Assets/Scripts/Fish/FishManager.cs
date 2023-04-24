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
    public int AlgaeCount = 0;
    public GameObject Algae;
    public float spawnYAlgae = 6.5f;
    public float spawnIntervalAlgae = 1f;
    public float spawnRangeAlgae = 9f;


    void Start()
    {
        InvokeRepeating("SpawnHooks", 0f, RepeatTime);
        InvokeRepeating("SpawnAlgae", 0f, spawnIntervalAlgae);
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

    void SpawnAlgae()
    {
        // Randomly generate an X position within the spawn range
        float spawnX = Random.Range(-spawnRangeAlgae, spawnRangeAlgae);
        // Spawn the object at the specified Y and X positions
        Instantiate(Algae, new Vector3(spawnX, spawnYAlgae, 0f), Quaternion.identity);
    }

}
