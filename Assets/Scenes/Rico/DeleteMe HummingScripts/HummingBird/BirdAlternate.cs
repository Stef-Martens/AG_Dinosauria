using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdAlternate : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float _rotationSpeed = 720f;

    private bool _isFrozen = false;
    private Vector3 _frozenPosition;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the gameobject forward
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        if(_isFrozen)
        {
            transform.position = _frozenPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flower flower = collision.gameObject.GetComponent<Flower>();
        if (flower != null)
        {
            if(!_isFrozen)
            {
                StartCoroutine(FreezePosition());
            }
        }
    }

    IEnumerator FreezePosition()
    {
        _isFrozen= true;
        _frozenPosition = transform.position;
        yield return new WaitForSeconds(1f);
        _isFrozen= false;
    }
}

