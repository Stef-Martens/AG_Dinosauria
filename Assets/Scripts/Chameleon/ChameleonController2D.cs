using UnityEngine;

public class ChameleonController2D : BaseController2D
{
    [SerializeField] private Animator _animator;

    public bool isBaseAnimation;
    public bool isBrownAnimation;
    public bool isGreenAnimation;

    public float animationSpeed;


    protected override void Update()
    {
        _animator.SetFloat("ChameleonBaseSpeed", Mathf.Abs(Movement.x));



        base.Update();
    }

    protected override void CeilingCheck()
    {

    }

    protected override void Jump()
    {

    }
}