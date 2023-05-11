using UnityEngine;

public abstract class MoveWithCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.x = Camera.main.transform.position.x + (transform.position.x - Camera.main.transform.position.x) / Camera.main.aspect;
        transform.position = newPos;
    }
}