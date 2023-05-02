using System;
using UnityEngine;

public class Nectar : MonoBehaviour
{
    public static event Action OnCollected;
    public static int total;
    private void Awake() => total++;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ParentObject"))
        {
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
