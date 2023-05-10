using UnityEngine;

public class TongueCurl : MonoBehaviour
{
    public GameObject TriggerHitObject { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        TriggerHitObject = other.transform.gameObject;
    }
}