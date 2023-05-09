using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBack : MonoBehaviour
{
    public GameObject BallPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && FindObjectOfType<IntroPlayer>().inventory.Name == "Ball")
        {
            FindObjectOfType<IntroPlayer>().ClearInventory();
            Vector3 BallPosition = transform.GetChild(0).transform.position;
            Instantiate(BallPrefab, BallPosition, Quaternion.identity);
        }
    }
}
