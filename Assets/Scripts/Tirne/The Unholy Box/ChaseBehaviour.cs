using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : MonoBehaviour
{
    [SerializeField]
    float _movementSpeed = 5f;

    Rigidbody2D _rb;

    [SerializeField]
    Transform _target;

    Vector3 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_target)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if(_target)
        {
            _rb.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _movementSpeed;
        }
    }



}
