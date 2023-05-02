using UnityEngine;

public class FalconBehaviour : MonoBehaviour
{
    protected float Animation = 0f;
    private Vector3 _start;
    private Vector3 _end;

    public float Height = -2f;
    public float FlySpeed = 1f;

    private bool _isMovingRight = true;


    public ChameleonMove ChameleonMove;
    public GameObject Chameleon;

    public float AttackSpeed = 2.5f;

    private bool _isAttacking;

    public float RetreatSpeed = 2.5f;

    private Vector3 _startAttackPos;

    private bool _canStartAttacking = true;

    private bool _doOnce = true;

    void Start()
    {
        _start = new Vector3(-6.95f, 4.73f, 0);
        _end = new Vector3(6.95f, 4.73f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (ChameleonMove.IsHiding)
        {
            Vector3 moveTowards = Vector3.MoveTowards(this.transform.position,
                                               _startAttackPos,
                                               Time.deltaTime * RetreatSpeed);

            this.transform.position = moveTowards;

            if (this.transform.position == _startAttackPos)
                _doOnce= false;

            if(!_doOnce)
            {
                ParabolaMovement(); 
                _isAttacking = false;
                _canStartAttacking = true;
            }
        }
           
        if (!ChameleonMove.IsHiding)
        {
            Attack();
            _isAttacking = true;
        }
        
        if (_canStartAttacking)
        {
            _startAttackPos = this.transform.position;
           _canStartAttacking= false;
            _doOnce= true;
        }
    }

    private void ParabolaMovement()
    {
        Animation += Time.deltaTime * FlySpeed;
        Animation = Animation % 5f;

        if (Animation / 5 <= .98f && _isMovingRight)
            this.transform.position = MathParabola.Parabola(_start, _end, Height, Animation / 5f);

        if (!_isMovingRight)
            this.transform.position = MathParabola.Parabola(_end, _start, Height, Animation / 5f);

        if (Animation / 5 >= .98f && _isMovingRight)
        {
            _isMovingRight = false;
            Animation = 0;
        }

        if (Animation / 5 >= .98f && !_isMovingRight)
        {
            _isMovingRight = true;
            Animation = 0;
        }
    }

    private void Attack()
    {
        Vector3 moveTowards = Vector3.MoveTowards(this.transform.position,
                                                       Chameleon.transform.position,
                                                       Time.deltaTime * AttackSpeed);

       this.transform.position = moveTowards;
    }
}
