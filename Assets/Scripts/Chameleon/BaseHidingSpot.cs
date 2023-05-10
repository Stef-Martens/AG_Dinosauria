using UnityEngine;

public abstract class BaseHidingSpot : MonoBehaviour
{
    public GameObject TriggerHitObject { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HidingSpotCollider")
            TriggerHitObject = this.transform.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HidingSpotCollider")
            TriggerHitObject = null;
    }
}