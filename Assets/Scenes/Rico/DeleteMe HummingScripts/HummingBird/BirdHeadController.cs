using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHeadController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float maxRotationAngle = 45f;
    public Transform birdHead;

    private float currentRotation = 0f;
    private Bird _bird;

    private void Start()
    {
        _bird = FindObjectOfType<Bird>();
    }

    void Update()
    {
        float rotation = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotation = rotationSpeed * Time.deltaTime;

            //if (_bird.isFacingRight)
            //{
            //    rotation = rotationSpeed * Time.deltaTime;
            //}
            //else
            //{
            //    rotation = -rotationSpeed * Time.deltaTime;
            //}
            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotation = -rotationSpeed * Time.deltaTime;

            //if (_bird.isFacingRight)
            //{
            //    rotation = -rotationSpeed * Time.deltaTime;
            //}
            //else
            //{
            //    rotation = rotationSpeed * Time.deltaTime;
            //}
        }

        currentRotation += rotation;

        float clampedRotation = Mathf.Clamp(currentRotation, -maxRotationAngle, maxRotationAngle);

        birdHead.localRotation = Quaternion.Euler(0f, 0f, clampedRotation);
    }
}
