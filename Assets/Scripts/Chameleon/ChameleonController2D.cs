using UnityEngine;

public class ChameleonController2D : BaseController2D
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _base;
    [SerializeField] private GameObject _camoBrown;
    [SerializeField] private GameObject _camoGreen;

    public bool isBaseAnimation;
    public bool isBrownAnimation;
    public bool isGreenAnimation;

    public float animationSpeed;
    protected override void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(Movement.x));
        _animator.SetBool("IsBase", _base.activeSelf);
        _animator.SetBool("IsBrown", _camoBrown.activeSelf);
        _animator.SetBool("IsGreen", _camoGreen.activeSelf);

        base.Update();
    }

    protected override void CeilingCheck()
    {

    }

    protected override void Jump()
    {

    }
}