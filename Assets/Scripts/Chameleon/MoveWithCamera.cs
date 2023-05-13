using UnityEngine;

public abstract class MoveWithCamera : MonoBehaviour
{
    public bool IsMoveWithLeftSide { get; set; }

    private void LateUpdate()
    {
        Vector3 newPos = transform.position;

        float aspectRatio = (float)Screen.width / (float)Screen.height;

        if (Camera.main.orthographic)
        {
            Camera.main.aspect = aspectRatio;
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Atan(Mathf.Tan(Camera.main.fieldOfView * Mathf.Deg2Rad * 0.5f) * aspectRatio) * 2f * Mathf.Rad2Deg;
        }

        float distance = transform.position.z - Camera.main.transform.position.z;
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * aspectRatio;
        
        float leftBound = Camera.main.transform.position.x - halfWidth;
        float rightBound = Camera.main.transform.position.x + halfWidth;

        if(IsMoveWithLeftSide) newPos.x = leftBound;
        else newPos.x = rightBound;

        newPos.y = transform.position.y;
        newPos.z = Camera.main.transform.position.z + distance;

        transform.position = newPos;
    }
}