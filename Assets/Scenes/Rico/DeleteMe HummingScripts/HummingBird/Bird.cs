using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float velocity = 1f;
    public float speed = 2f;
    public float rotationSpeed = 100f;
    public float characterDirection;

    private Rigidbody2D _rb;

    private float _horizontalInput;
    private float _verticalInput;
    private bool _jump;
    private bool _isFrozen;
    public bool isFacingRight = true;

    private float _currentRotation = 0f;
    private float _defaultRotation = 0f;

    private Vector3 _frozenPosition;
    private Vector2 _movement;

    public bool insideFlower;
    [SerializeField] private Animator _animator;
    private bool _isFlying;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime;
        //_verticalInput = Input.GetAxis("Vertical") * Time.deltaTime;
        _movement = new Vector2(_horizontalInput * speed, _verticalInput * speed);

        if (_movement != Vector2.zero)
        {
            _rb.gravityScale = 2;
            _isFrozen = false;
            insideFlower = false;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _rb.velocity = Vector2.up * velocity;
        //    _rb.gravityScale = 2;
        //    _animator.SetBool("Fly", true);
        //}

        // Check for Space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle flying state
            _isFlying = !_isFlying;
            _rb.velocity = Vector2.up * velocity;

            _rb.gravityScale = 2;

            // Toggle the animator bool
            _animator.SetBool("Fly", _isFlying);
        }

        float rotationDelta = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
            _currentRotation += rotationDelta;
            _currentRotation = Mathf.Clamp(_currentRotation, -90f, 90f);
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, _currentRotation);
        if(isFacingRight)
        {
            transform.rotation = rotation;
            _rb.MoveRotation(rotation);
        }

        if(!isFacingRight)
        {
            Quaternion invertedRotation = Quaternion.Euler(0.0f, 0.0f, -_currentRotation);
            transform.rotation = invertedRotation;
            _rb.MoveRotation(invertedRotation);
        }
            

        transform.Translate(_movement,0);

        if (_isFrozen)
        {
            transform.position = _frozenPosition;
        }

        if (_horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }

        else if (_horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Flower flower = collision.gameObject.GetComponent<Flower>();
        if (flower != null)
        {
            insideFlower = true;
            if (!_isFrozen)
            {
                StartCoroutine(FreezePosition());
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Flower flower = collision.gameObject.GetComponent<Flower>();
        SceneSwitch sceneSwitch = collision.gameObject.GetComponent<SceneSwitch>();

        if (flower != null)
        {
            insideFlower = true;
            if (!_isFrozen)
            {
                StartCoroutine(FreezePosition());
            }
        }

        if(sceneSwitch!= null)
        {
            if (!_isFrozen)
            {
                StartCoroutine(FreezePosition());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Flower flower = collision.gameObject.GetComponent<Flower>();
        if (flower != null)
        {
            insideFlower = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneSwitch sceneSwitch = collision.GetComponent<SceneSwitch>();
        if(sceneSwitch != null)
        {
            gameObject.SetActive(false);
        }
    }


    IEnumerator FreezePosition()
    {
        _isFrozen = true;
        _frozenPosition = transform.position;
        yield return _frozenPosition;
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        
        _currentRotation = _defaultRotation;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, _currentRotation);
        _rb.MoveRotation(transform.rotation);

        isFacingRight = !isFacingRight;
    }


}
