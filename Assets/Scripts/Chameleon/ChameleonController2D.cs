using UnityEngine;

public class ChameleonController2D : BaseController2D
{
    public float SpeedOffset = 0.1f;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _base;
    [SerializeField] private GameObject _camoBrown;
    [SerializeField] private GameObject _camoGreen;

    private float _animationSpeed;

    protected override void Update()
    {
        _animationSpeed = Inputs.HorizontalMove.x;

        _animator.SetFloat("Speed", Mathf.Abs(_animationSpeed * SpeedOffset));
        _animator.SetBool("isBase", _base.activeSelf);
        _animator.SetBool("isBrown", _camoBrown.activeSelf);
        _animator.SetBool("isGreen", _camoGreen.activeSelf);
        
        base.Update();
    }

    protected override void CeilingCheck()
    {

    }

    protected override void Jump()
    {

    }
}