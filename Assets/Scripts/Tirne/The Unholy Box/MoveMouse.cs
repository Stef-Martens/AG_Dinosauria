using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouse : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _movementSpeed = 4f;

    [SerializeField]
    private Vector2 _resetPosition;

    private bool _canMove = false;

    public void StartMovement()
    {
        _canMove = true;
    }

    public void StopMovement()
    {
        _canMove = false;
    }

    public void ResetMouse()
    {
        gameObject.transform.position = _resetPosition;
    }

    private void FixedUpdate()
    {
        if(_canMove)
        {
            _rb.velocity = new Vector2(Vector2.one.x, -0.5f) * _movementSpeed;
        }
        else
        {
            _rb.velocity = new Vector2(0, 0);
        }
    }


}
