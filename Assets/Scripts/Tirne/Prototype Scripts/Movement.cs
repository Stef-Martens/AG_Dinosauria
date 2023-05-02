using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float _velocity = 20f;

    [SerializeField]
    private LayerMask _layerMask;

    private float _horizontalInput;
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;  

    public bool _movementLocked = false;

    private bool isFacingRight;
    
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _bc = this.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (!_movementLocked)
        {
            Jump();
            MovementInput();
            transform.Translate(_movement, 0);
        }


        if (_horizontalInput < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (_horizontalInput > 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }



    bool IsGrounded()
    {
        float extraHeight = 0.5f;
        RaycastHit2D raycasthit = Physics2D.Raycast(_bc.bounds.center, Vector2.down, _bc.bounds.extents.y + extraHeight, _layerMask);
        if(raycasthit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.velocity = Vector2.up * _velocity;
            _rb.gravityScale = 2;
        }
    }

    void MovementInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime;
        _movement = new Vector2(_horizontalInput * speed, 0);
    }
}
