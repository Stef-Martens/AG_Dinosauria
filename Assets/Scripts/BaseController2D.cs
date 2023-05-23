using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseController2D : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 4.0f;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.1f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool IsGrounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    [Header("Player HitCeiling")]
    [Tooltip("If the character has hit ceiling or not")]
    public bool HasHitCeiling = false;
    [Tooltip("The radius of the hit ceiling check.")]
    public float HitCeilingRadius = 0.1f;
    [Tooltip("What layers the character uses as ceiling")]
    public LayerMask CeilingLayers;

    // player
    private float _speed;
    private float _terminalVelocity = 53.0f;
    private bool _hasHitCeilingBefore = false;

    private float _fallTimeoutDelta;

    private PlayerInput _playerInput;
    private CharacterController _controller;
    protected Vector3 Movement;

    // Properties
    public bool IsFacingRight { get; private set; } = true;
    public float VerticalVelocity { get; set; }
    public float JumpTimeoutDelta { get; set; }
    public Inputs Inputs { get; private set; }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Inputs = GetComponent<Inputs>();
        _playerInput = GetComponent<PlayerInput>();

        // reset our timeouts on start
        JumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    protected virtual void Update()
    {
        GroundedCheck();
        CeilingCheck();
        Move();
        SetFacingRight();
        SetRotation();
        ApplyGravity();
        Jump();
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        IsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    protected virtual void CeilingCheck()
    {
        // set sphere position, with controller height
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + _controller.height, transform.position.z);
        HasHitCeiling = Physics.CheckSphere(spherePosition, HitCeilingRadius, CeilingLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = MoveSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (Inputs.HorizontalMove == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = Inputs.AnalogMovement ? Inputs.HorizontalMove.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(Inputs.HorizontalMove.x, 0.0f, Inputs.HorizontalMove.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (Inputs.HorizontalMove != Vector2.zero)
        {
            // move
            if (IsFacingRight)
                inputDirection = transform.right * Inputs.HorizontalMove.x;
            else
                inputDirection = -transform.right * Inputs.HorizontalMove.x;
        }

        // move the player
        Movement = inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, VerticalVelocity, 0.0f) * Time.deltaTime;
        _controller.Move(Movement);
    }

    private void SetFacingRight()
    {
        if (Inputs.HorizontalMove.x > 0.0f)
            IsFacingRight = true;
        if (Inputs.HorizontalMove.x < 0.0f)
            IsFacingRight = false;
        if (Inputs.HorizontalMove.x == 0)
        {
            if (IsFacingRight)
                IsFacingRight = true;
            else
                IsFacingRight = false;
        }
    }

    private void SetRotation()
    {
        if (IsFacingRight)
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void ApplyGravity()
    {
        if (IsGrounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (VerticalVelocity < 0.0f)
            {
                VerticalVelocity = -2f;
            }
        }
        else
        {
            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (VerticalVelocity < _terminalVelocity)
        {
            VerticalVelocity += Gravity * Time.deltaTime;
        }

        // reset vertical velocity if hit ceiling
        if (HasHitCeiling)
        {
            if (VerticalVelocity >= 0.0f && !_hasHitCeilingBefore)
            {
                VerticalVelocity = 0.0f;
                _hasHitCeilingBefore = true;
            }
        }

        if (_hasHitCeilingBefore)
            _hasHitCeilingBefore = false;
    }

    protected virtual void Jump()
    {
        if (IsGrounded)
        {
            // Jump
            if (Inputs.Action && JumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                VerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            // jump timeout
            if (JumpTimeoutDelta >= 0.0f)
            {
                JumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            JumpTimeoutDelta = JumpTimeout;

            // if we are not grounded, do not jump
            Inputs.Action = false;
        }
    }
}