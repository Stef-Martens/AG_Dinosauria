using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class RaptorBird : MonoBehaviour
{
    protected float Animation = 0f;

    public float Height = -2f;
    public float FlySpeed = 1f;
    public float AttackSpeed = 2.5f;
    public float RetreatSpeed = 2.5f;

    public GameObject RightBound;
    public GameObject LeftBound;
    public GameObject Chameleon;

    private Vector3 _start;
    private Vector3 _end;
    private bool _isMovingRight = false;

    //private ChameleonController2D _chameleonController2D;
    private Vector3 _chameleonPos;
    private BodyCamo _bodyCamo;







    private bool _isAttacking;



    private Vector3 _startAttackPos;

    private bool _canStartAttacking = true;

   
    
    private bool _doOnce = true;

    void Start()
    {
        _bodyCamo = Chameleon.transform.GetChild(0).GetChild(1).gameObject.GetComponent<BodyCamo>();
    }

    void Update()
    {
        FaceChameleon();
        ParabolaMovement();
    }

    private void FaceChameleon()
    {
        _chameleonPos = Chameleon.transform.GetChild(0).gameObject.transform.position;

        if (this.transform.position.x <= _chameleonPos.x)
            this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, -180, this.transform.localRotation.z);
        if(this.transform.position.x > _chameleonPos.x)
            this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, 0, this.transform.localRotation.z);
    }

    private void ParabolaMovement()
    {
        _start = LeftBound.transform.position;
        _end = RightBound.transform.position;

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
}