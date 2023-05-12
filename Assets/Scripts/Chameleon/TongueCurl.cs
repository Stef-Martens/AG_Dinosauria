using UnityEngine;

public class TongueCurl : MonoBehaviour
{
    public LayerMask AnimalsLayer;

    public GameObject TriggerHitObject { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & AnimalsLayer.value) != 0)
        {
            TriggerHitObject = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & AnimalsLayer.value) != 0)
        {
            TriggerHitObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & AnimalsLayer.value) != 0)
        {
            TriggerHitObject = null;
        }
    }
}