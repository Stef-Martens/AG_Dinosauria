using System.Diagnostics;
using UnityEngine;

public class RaptorBird : MonoBehaviour
{
    protected float Animation = 0f;

    public bool IsStaggered { get; set; }
    public bool IsStaggering { get; private set; } = false;

    public float ParabolaMoveHeight = -2f;
    public float FlySpeed = 1f;
    public float AttackSpeed = 2.5f;
    public float RetreatSpeed = 2.5f;
    public float StaggerTime = 3.0f;

    public GameObject RightBound;
    public GameObject LeftBound;
    public GameObject Chameleon;

    private BodyCamo _bodyCamo;
    private GameObject _attackTarget;
    private Vector3 _chameleonPos;

    private Vector3 _startPosParabola;
    private Vector3 _endPosParabola;
    private bool _isMovingRight = false;
    private bool _isFacingRight = false;

    private bool _startBaseMove = true;
    private bool _hasStartAttacking = false;
    private bool _isAttacking = false;
    private bool _canRetreat = false;

    private Stopwatch _staggerTimer;

    void Start()
    {
        _bodyCamo = Chameleon.transform.GetChild(0).GetChild(1).gameObject.GetComponent<BodyCamo>();
        _attackTarget = Chameleon.transform.GetChild(0).GetChild(2).gameObject;

        _staggerTimer= new Stopwatch();
    }

    void Update()
    {
        FaceChameleon();

        if (!IsStaggering)
        {
            SetStartEndPosParabola();
            BaseMove();
            Attack();
            Retreat();
        }

        Stagger();
    }

    private void FaceChameleon()
    {
        _chameleonPos = Chameleon.transform.GetChild(0).gameObject.transform.position;

        if (this.transform.position.x <= _chameleonPos.x)
        {
            this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, -180, this.transform.localRotation.z);
            _isFacingRight = true;
        }
        if (this.transform.position.x > _chameleonPos.x)
        {
            this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, 0, this.transform.localRotation.z);
            _isFacingRight = false;
        }
    }

    private void SetStartEndPosParabola()
    {
        if (_isFacingRight)
        {
            if (_isMovingRight)
            {
                _startPosParabola = LeftBound.transform.position;
                _endPosParabola = RightBound.transform.position;
            }
            else
            {
                _startPosParabola = RightBound.transform.position;
                _endPosParabola = LeftBound.transform.position;
            }
        }

        else
        {
            if (_isMovingRight)
            {
                _startPosParabola = LeftBound.transform.position;
                _endPosParabola = RightBound.transform.position;
            }
            else
            {
                _startPosParabola = RightBound.transform.position;
                _endPosParabola = LeftBound.transform.position;
            }
        }
    }

    private void ParabolaMovement()
    {
        Animation += Time.deltaTime * FlySpeed;
        Animation = Animation % 5f;this.transform.position = MathParabola.Parabola(_startPosParabola,
                                                                                   _endPosParabola,
                                                                                   ParabolaMoveHeight,
                                                                                   Animation / 5f);
        
        if (Animation / 5 >= .98f)
        {
            _isMovingRight = !_isMovingRight;
            Animation = 0;
        }
    }

    private void BaseMove()
    {
        if (_startBaseMove)
        {
            _startBaseMove = false;
        }

        if (_bodyCamo.IsCamo && !_canRetreat)
        {
            ParabolaMovement();

            _hasStartAttacking = false;
            _isAttacking = false;

        }
    }

    private void Attack()
    {
        if (!_bodyCamo.IsCamo)
        {
            _isAttacking = true;

            if (!_hasStartAttacking)
            {
                _hasStartAttacking = true;
            }
        }

        if (_isAttacking)
        {
            Vector3 moveTowards = Vector3.MoveTowards(this.transform.position,
                                                         _attackTarget.transform.position,
                                                         Time.deltaTime * AttackSpeed);

            this.transform.position = moveTowards;

            _canRetreat = true;
        }
    }

    private void Retreat()
    {
        if (_bodyCamo.IsCamo && _canRetreat)
        {
            _isAttacking = false;
            Vector3 moveTowards = Vector3.zero;

            if (_isFacingRight)
            {
                _isMovingRight = true;

                moveTowards = Vector3.MoveTowards(this.transform.position,
                                                  LeftBound.transform.position,
                                                  Time.deltaTime * RetreatSpeed);
            }
            else
            {
                _isMovingRight = false;

                moveTowards = Vector3.MoveTowards(this.transform.position,
                                                  RightBound.transform.position,
                                                  Time.deltaTime * RetreatSpeed);
            }

            this.transform.position = moveTowards;

            if (Vector3.Distance(this.transform.position, LeftBound.transform.position) < 0.1f
                || Vector3.Distance(this.transform.position, RightBound.transform.position) < 0.1f)
            {
                _startBaseMove = true;
                _canRetreat = false;
                Animation = 0;
            }
        }
    }

    private void Stagger()
    {
        if (IsStaggered)
        {
            IsStaggering = true;
            _staggerTimer.Start();
        }

        if (IsStaggering)
        {
            // Update position based on the elapsed time and desired shake parameters
            float frequency = 10f; // the number of shakes per second
            float amplitude = 0.0025f; // the maximum distance the falcon will move left or right
            Vector3 currentPosition = this.transform.position;
            Vector3 newPosition = new Vector3(currentPosition.x + Mathf.Sin((float)_staggerTimer.Elapsed.TotalSeconds * frequency) * amplitude,
                                              currentPosition.y,
                                              currentPosition.z);
            this.transform.position = newPosition;

            if (_staggerTimer.Elapsed.TotalSeconds >= StaggerTime)
            {
                IsStaggering = false;
                _staggerTimer.Reset();
            }
        }
        else
        {
            _staggerTimer.Stop();
            _staggerTimer.Reset();
        }
    }
}