using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            foreach (FruitBat ding in FindObjectsOfType<FruitBat>())
            {
                ding.ResetBat();
            }

        }
    }
}
