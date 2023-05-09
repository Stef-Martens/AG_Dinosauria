using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBat : MonoBehaviour
{
    public bool GoAfterPlayer = false;

    Vector3 StartPosition;
    GameObject player;

    public float Speed = 5f;

    public bool IsBee;

    void Start()
    {
        StartPosition = transform.position;
        player = FindObjectOfType<IntroPlayer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GoAfterPlayer)
        {
            Vector3 direction = Vector3.Normalize(player.transform.position - transform.position);
            transform.Translate(direction * Speed * Time.deltaTime);
        }
    }

    public void ResetBat()
    {
        GoAfterPlayer = false;
        transform.position = StartPosition;
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.tag == player.tag)
        {
            ResetBat();
            player.GetComponent<IntroPlayer>().ClearInventory();
        }
    }
}
